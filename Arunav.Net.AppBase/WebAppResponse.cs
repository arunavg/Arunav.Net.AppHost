namespace Arunav.Net.AppBase
{
    public class WebAppResponse : IAppResponse
    {
        private readonly System.Web.HttpResponse _response;
        private readonly AppCookieCollection _cookies;

        public WebAppResponse(System.Web.HttpResponse response)
        {
            _response = response;
            _cookies = new WebAppCookieCollection(response.Cookies);
        }

        public bool IsClientConnected { get { return _response.IsClientConnected; } }

        public bool BufferOutput { set { _response.BufferOutput = value; } }

        public string ContentType { set { _response.ContentType = value; } get { return _response.ContentType; } }

        public long ContentLength { set { _response.AppendHeader("Content-Length", value.ToString("G")); } get { return System.Convert.ToInt64(_response.Headers["Content-Length"]); } }

        public AppCookieCollection Cookies { get { return _cookies; } }

        public System.IO.TextWriter Output { get { return _response.Output; } }

        public System.IO.Stream OutputStream { get { return _response.OutputStream; } }

        public int StatusCode { set { _response.StatusCode = value; } }

        public void AppendHeader(string name, string value)
        {
            _response.AppendHeader(name, value);
        }

        public void Write(string s)
        {
            _response.Output.Write(s);
        }

        public void Write(string s, object arg0)
        {
            _response.Output.Write(s, arg0);
        }

        public void Write(string s, object arg0, object arg1)
        {
            _response.Output.Write(s, arg0, arg1);
        }

        public void Write(string s, object arg0, object arg1, object arg2)
        {
            _response.Output.Write(s, arg0, arg1, arg2);
        }

        public void Write(string s, params object[] arg)
        {
            _response.Output.Write(s, arg);
        }

        public void Clear()
        {
            _response.Clear();
        }

        public void Redirect(string url)
        {
            _response.Redirect(url);
        }

        public void RedirectPermanent(string url)
        {
            _response.RedirectPermanent(url);
        }

        public void TransmitFile(string filename)
        {
            _response.TransmitFile(filename);
        }

        public void TransmitFile(string filename, int offset, int length)
        {
            _response.TransmitFile(filename, offset, length);
        }
    }
}
