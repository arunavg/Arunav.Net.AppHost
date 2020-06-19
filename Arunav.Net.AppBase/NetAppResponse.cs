namespace Arunav.Net.AppBase
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "MemoryStreams do not require disposing. https://stackoverflow.com/questions/4274590/memorystream-close-or-memorystream-dispose")]
    public class NetAppResponse : IAppResponse
    {
        private readonly System.Net.HttpListenerResponse _response;
        private readonly System.IO.Stream _responseOutputStream;
        private readonly System.IO.TextWriter _responseOutput;
        private readonly AppCookieCollection _cookies;

        public NetAppResponse(System.Net.HttpListenerResponse response)
        {
            _response = response;
            //_responseOutput = new System.IO.StreamWriter(response.OutputStream);
            _responseOutputStream = new System.IO.MemoryStream();
            _responseOutput = new System.IO.StreamWriter(_responseOutputStream);
            _cookies = new NetAppCookieCollection(response.Cookies);
        }

        public bool IsClientConnected { get { return true; } } //Not Supported

        public bool BufferOutput { set { } } //Not Supported

        public string ContentType { set { _response.ContentType = value; } get { return _response.ContentType; } }

        public long ContentLength { set { _response.ContentLength64 = value; } get { return _response.ContentLength64; } }

        public AppCookieCollection Cookies { get { return _cookies; } }

        public System.IO.Stream OutputStream { get { return _responseOutputStream; } }

        public System.IO.TextWriter Output { get { return _responseOutput; } }

        public int StatusCode { set { _response.StatusCode = value; } }

        public void AppendHeader(string name, string value)
        {
            _response.AppendHeader(name, value);
        }

        public void Write(string s)
        {
            _responseOutput.Write(s);
        }

        public void Write(string s, object arg0)
        {
            _responseOutput.Write(s, arg0);
        }

        public void Write(string s, object arg0, object arg1)
        {
            _responseOutput.Write(s, arg0, arg1);
        }

        public void Write(string s, object arg0, object arg1, object arg2)
        {
            _responseOutput.Write(s, arg0, arg1, arg2);
        }

        public void Write(string s, params object[] arg)
        {
            _responseOutput.Write(s, arg);
        }

        public void Clear()
        {
            _response.ContentLength64 = 0;
            //_response.OutputStream.SetLength(0);
        }

        public void Redirect(string url)
        {
            _response.StatusDescription = "302 Temporary Redirect";
            _response.StatusCode = 302;
            _response.RedirectLocation = url;
        }

        public void RedirectPermanent(string url)
        {
            _response.StatusDescription = "301 Permanent Redirect";
            _response.StatusCode = 301;
            _response.RedirectLocation = url;
        }

        public void TransmitFile(string filename)
        {
            using (System.IO.FileStream fileStream = System.IO.File.OpenRead(filename))
            {
                _response.ContentLength64 = fileStream.Length;
                fileStream.CopyTo(_response.OutputStream);
            }
        }

        public void TransmitFile(string filename, int offset, int length)
        {
            using (System.IO.FileStream fileStream = System.IO.File.OpenRead(filename))
            {
                byte[] data = new byte[length];
                int readLength = fileStream.Read(data, offset, length);
                _response.ContentLength64 = readLength;
                _response.OutputStream.Write(data, 0, readLength);
            }
        }
    }
}
