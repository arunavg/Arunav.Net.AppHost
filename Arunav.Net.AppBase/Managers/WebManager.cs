using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Web;

namespace Arunav.Net.AppBase.Managers
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "The HttpListener in question is disposed in the Stop method. This is done to align interface with Service Model.")]
    internal class WebManager : IAppManager
    {
        private IAppRouteModule _mainModule;
        private HttpListener _listener;
        private bool _stopListener;

        private NameValueCollection _mimeTypes;
        private Dictionary<string, IHttpHandler> _handlers;

        public void Initialize(object appModule, Assembly appAssembly)
        {
            _mainModule = (IAppRouteModule)appModule;

            //SectionInformation.GetRawXml Returns null in Mono (bug #13599) so using XmlDocument instead of ConfigurationSection to read the config for handlers

            //System.Configuration.Configuration configuration = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            //System.Configuration.ConfigurationSection webServerSection = configuration.GetSection("system.webServer");

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            //doc.LoadXml(webServerSection.SectionInformation.GetRawXml());

            doc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            System.Xml.XmlNode webServerNode = doc.SelectSingleNode("/configuration/system.webServer");
            System.Xml.XmlNodeList handlerNodes = webServerNode.SelectNodes("handlers/add");

            _handlers = new Dictionary<string, IHttpHandler>(StringComparer.OrdinalIgnoreCase);

            foreach (System.Xml.XmlNode handlerNode in handlerNodes)
            {
                string pathVal = handlerNode.Attributes["path"].Value;
                int startPos = pathVal.LastIndexOf('/') + 1;
                pathVal = pathVal.Substring(startPos, pathVal.LastIndexOf('.') - startPos); //remove slash and extension
                string typeName = handlerNode.Attributes["type"].Value;
                typeName = typeName.Substring(0, typeName.IndexOf(','));
                Type handlerType = appAssembly.GetType(typeName);
                IHttpHandler handler = (IHttpHandler)Activator.CreateInstance(handlerType);
                _handlers.Add(pathVal, handler);
            }
        }

        public void Start(string hostUrl, NameValueCollection mimeTypes)
        {
            _mimeTypes = mimeTypes;
            _listener = new HttpListener();
            _listener.Prefixes.Add(hostUrl);
            _listener.Start();

            Thread mainListenerThread = new Thread(new ThreadStart(StartListening));
            mainListenerThread.Start();
        }

        public void Stop()
        {
            _stopListener = true;
            _listener.Stop();
            _listener.Close();
        }

        private void StartListening()
        {
            try
            {
                while (!_stopListener)
                {
                    // Note: The GetContext method blocks while waiting for a request. 
                    HttpListenerContext context = _listener.GetContext();
                    if (_stopListener)
                        break;
                    Thread listenerThread = new Thread(new ParameterizedThreadStart(ProcessRequest));
                    listenerThread.Start(context);
                }
            }
            catch (HttpListenerException ex)
            {
                if (ex.ErrorCode != 995) //when listener is stopped explicity no need for exception
                    throw;
            }
        }

        private void ProcessRequest(object objContext)
        {
            HttpListenerContext listenerContext = (HttpListenerContext)objContext;
            AppContext context = new NetAppContext(listenerContext);

            if (!_mainModule.RouteRequest(context))
            {
                listenerContext.Response.Close();
                return;
            }

            string fileNamePart = context.Request.Url.Segments[context.Request.Url.Segments.Length - 1];
            if (IsHandlerRequest(ref fileNamePart))
            {
                //for handlers
                IHttpHandler handler = _handlers[fileNamePart];

                handler.ProcessRequest(null);

                StreamWriter sw = (StreamWriter)context.Response.Output;
                sw.Flush();
                MemoryStream ms = (MemoryStream)sw.BaseStream;
                byte[] data = ms.ToArray();
                if (data.Length > 0)
                {
                    listenerContext.Response.ContentLength64 = data.Length;
                    listenerContext.Response.OutputStream.Write(data, 0, data.Length);
                }
            }
            else
            {
                //for static files
                string filePath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, context.Request.Url.AbsolutePath.Substring(1));

                if (File.Exists(filePath))
                    WriteStaticFile(filePath, listenerContext.Response);
                else
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }

            listenerContext.Response.Close();
        }

        private static bool IsHandlerRequest(ref string fileNamePart)
        {
            if (fileNamePart.EndsWith(".aspx", StringComparison.OrdinalIgnoreCase)
                || fileNamePart.EndsWith(".ashx", StringComparison.OrdinalIgnoreCase))
            {
                fileNamePart = fileNamePart.Substring(0, fileNamePart.Length - 5).ToLowerInvariant(); // remove ext
                return true;
            }
            return false;
        }

        private void WriteStaticFile(string filename, HttpListenerResponse response)
        {
            try
            {
                using (Stream input = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    //Adding permanent http response headers
                    response.ContentType = _mimeTypes[Path.GetExtension(filename)] ?? "application/octet-stream";

                    response.ContentLength64 = input.Length;
                    response.AddHeader("Date", DateTime.UtcNow.ToString("r"));
                    response.AddHeader("Last-Modified", File.GetLastWriteTime(filename).ToString("r"));

                    //Should set the status header first before flushing out the content
                    response.StatusCode = (int)HttpStatusCode.OK;

                    byte[] buffer = new byte[1024 * 32];
                    int nbytes;
                    while ((nbytes = input.Read(buffer, 0, buffer.Length)) > 0)
                        response.OutputStream.Write(buffer, 0, nbytes);
                    response.OutputStream.Flush();
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.StatusDescription = ex.Message;
            }
        }
    }
}
