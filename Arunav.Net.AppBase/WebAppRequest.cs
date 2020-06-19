namespace Arunav.Net.AppBase
{
    public class WebAppRequest : IAppRequest
    {
        private readonly System.Web.HttpRequest _request;
        private readonly AppCookieCollection _cookies;

        public string RawUrl { get { return _request.RawUrl; } }
        public System.Uri Url { get { return _request.Url; } }
        public string HttpMethod { get { return _request.HttpMethod; } }
        public System.Collections.Specialized.NameValueCollection QueryString { get { return _request.QueryString; } }
        public System.Collections.Specialized.NameValueCollection Form { get { return _request.Form; } }
        public System.Collections.Specialized.NameValueCollection Headers { get { return _request.Headers; } }
        public long ContentLength { get { return _request.ContentLength; } }
        public string ContentType { get { return _request.ContentType; } }
        public System.IO.Stream InputStream { get { return _request.InputStream; } }
        public string RemoteIPAddress { get { return _request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? _request.ServerVariables["REMOTE_ADDR"]; } }
        public AppCookieCollection Cookies { get { return _cookies; } }

        public WebAppRequest(System.Web.HttpRequest request)
        {
            _request = request;
            _cookies = new WebAppCookieCollection(request.Cookies);
        }
    }
}
