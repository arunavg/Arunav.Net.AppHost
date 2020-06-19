namespace Arunav.Net.AppBase
{
    public class NetAppRequest : IAppRequest
    {
        private readonly System.Net.HttpListenerRequest _request;
        private readonly AppCookieCollection _cookies;
        private string _requestRawUrl;
        private System.Uri _requestUrl;
        private System.Collections.Specialized.NameValueCollection _requestQueryString;

        private readonly System.Uri _baseUrl;

        private readonly System.Collections.Specialized.NameValueCollection _formValues;

        public string RawUrl { get { return _requestRawUrl; } }
        public System.Uri Url { get { return _requestUrl; } }
        public string HttpMethod { get { return _request.HttpMethod; } }
        public System.Collections.Specialized.NameValueCollection QueryString { get { return _requestQueryString; } }
        public System.Collections.Specialized.NameValueCollection Form { get { return _formValues; } }
        public System.Collections.Specialized.NameValueCollection Headers { get { return _request.Headers; } }
        public long ContentLength { get { return _request.ContentLength64; } }
        public string ContentType { get { return _request.ContentType; } }
        public System.IO.Stream InputStream { get { return _request.InputStream; } }
        public string RemoteIPAddress { get { return _request.RemoteEndPoint.Address.ToString(); } }
        public AppCookieCollection Cookies { get { return _cookies; } }

        public NetAppRequest(System.Net.HttpListenerRequest request)
        {
            _baseUrl = new System.Uri(request.Url.GetLeftPart(System.UriPartial.Authority));
            _request = request;
            _requestUrl = request.Url;
            _requestRawUrl = request.RawUrl;
            _requestQueryString = request.QueryString;
            _cookies = new NetAppCookieCollection(request.Cookies);

            _formValues = new System.Collections.Specialized.NameValueCollection();

            if ("POST".Equals(request.HttpMethod, System.StringComparison.Ordinal) && request.HasEntityBody)
            {
                string formDataValue;
                // here we have data
                using (System.IO.StreamReader reader = new System.IO.StreamReader(request.InputStream, request.ContentEncoding))
                    formDataValue = reader.ReadToEnd();

                if (!string.IsNullOrEmpty(formDataValue))
                {
                    string[] pairs = formDataValue.Split('&');
                    foreach (string pair in pairs)
                    {
                        string[] nameValue = pair.Split('=');
                        if (nameValue.Length > 1)
                            _formValues.Add(System.Web.HttpUtility.UrlDecode(nameValue[0]), System.Web.HttpUtility.UrlDecode(nameValue[1]));
                        else
                            _formValues.Add(System.Web.HttpUtility.UrlDecode(nameValue[0]), "");
                    }
                }
            }
        }

        internal void RewriteUrl(string relativeUrl)
        {
            _requestRawUrl = relativeUrl;
            _requestUrl = new System.Uri(_baseUrl, relativeUrl);
            _requestQueryString = System.Web.HttpUtility.ParseQueryString(_requestUrl.Query);
        }
    }
}
