namespace Arunav.Net.AppBase
{
    public interface IAppRequest
    {
        string RawUrl { get; }
        System.Uri Url { get; }
        string HttpMethod { get; }
        System.Collections.Specialized.NameValueCollection QueryString { get; }
        System.Collections.Specialized.NameValueCollection Form { get; }
        System.Collections.Specialized.NameValueCollection Headers { get; }
        long ContentLength { get; }
        string ContentType { get; }
        System.IO.Stream InputStream { get; }
        string RemoteIPAddress { get; }
        AppCookieCollection Cookies { get; }
    }
}
