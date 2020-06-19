using Arunav.Net.AppBase;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Security.Policy;

namespace Arunav.Net.AppHost
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("HttpListener is not available with your current operating system!");
                Console.WriteLine("It is required for AppHost Web Server for self hosting.");
                Console.WriteLine("Host the App on a Web Server for this environment.");
                return;
            }

            Start();
            Console.WriteLine("Press Escape to Stop...");
            while (Console.ReadKey().Key != ConsoleKey.Escape) ;
            Stop();
        }

        private static AppInstance[] _appInstances = new AppInstance[0];

        internal static void Start()
        {
            NameValueCollection settings = ReadSettings("AppSettings.ini");
            NameValueCollection mimeTypes = ReadSettings("MimeTypes.ini");

            Type appInstanceType = typeof(AppInstance);

            string[] appNames = settings["AppNames"].Split(',');
            _appInstances = new AppInstance[appNames.Length];

            for (int i = 0; i < appNames.Length; i++)
            {
                string appName = appNames[i];
                string appType = settings[appName + "_Type"];
                string hostUrl = settings[appName + "_HostUrl"];
                string rootPath = settings[appName + "_RootPath"];
                string moduleTypeName = settings[appName + "_MainModule"];

                rootPath = (rootPath[rootPath.Length - 1] != Path.DirectorySeparatorChar)
                            ? Path.GetFullPath(rootPath + Path.DirectorySeparatorChar) : Path.GetFullPath(rootPath);

                AppDomainSetup domainInfo = new AppDomainSetup
                {
                    ApplicationBase = rootPath,
                    ConfigurationFile = rootPath + "Web.config",
                    PrivateBinPath = "bin"
                };

                Evidence evidence = new Evidence(AppDomain.CurrentDomain.Evidence);
                AppDomain domain = AppDomain.CreateDomain(appName, evidence, domainInfo);

                AppInstance app = (AppInstance)domain.CreateInstanceAndUnwrap(appInstanceType.Assembly.FullName, appInstanceType.FullName);

                //Calling AppInstance initialize and starting the app (server)
                app.Initialize(appType, moduleTypeName);
                app.Start(hostUrl, mimeTypes);

                _appInstances[i] = app;

                Console.WriteLine("App Host {0} Started:", appName);
                Console.WriteLine(hostUrl);
            }
        }

        internal static void Stop()
        {
            for (int i = 0; i < _appInstances.Length; i++)
            {
                try
                {
                    _appInstances[i].Stop();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static NameValueCollection ReadSettings(string filePath)
        {
            NameValueCollection settings = new NameValueCollection();

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    line = line.TrimStart();

                    if (line.Length > 0 && line.IndexOf(';') != 0)
                    {
                        int eqPos = line.IndexOf('=');
                        if (eqPos < 0)
                            settings.Add(line, "");
                        else
                            settings.Add(line.Substring(0, eqPos), line.Substring(eqPos + 1));
                    }

                    line = sr.ReadLine();
                }
            }

            return settings;
        }
    }
}
