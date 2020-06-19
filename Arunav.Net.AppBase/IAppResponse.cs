namespace Arunav.Net.AppBase
{
    public interface IAppResponse
    {
        int StatusCode { set; }

        bool IsClientConnected { get; }

        bool BufferOutput { set; }

        string ContentType { set; get; }

        long ContentLength { set; get; }

        AppCookieCollection Cookies { get; }

        System.IO.TextWriter Output { get; }

        System.IO.Stream OutputStream { get; }

        void AppendHeader(string name, string value);

        void Write(string s);

        void Write(string s, object arg0);

        void Write(string s, object arg0, object arg1);

        void Write(string s, object arg0, object arg1, object arg2);

        void Write(string s, params object[] arg);

        void Clear();

        void Redirect(string url);

        void RedirectPermanent(string url);

        void TransmitFile(string filename);

        void TransmitFile(string filename, int offset, int length);
    }
}
