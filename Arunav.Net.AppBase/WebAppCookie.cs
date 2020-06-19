namespace Arunav.Net.AppBase
{
    public partial class AppCookie
    {
        private class WebAppCookie : IAppCookie
        {
            private readonly System.Web.HttpCookie _internalCookie;

            public string Name { get { return _internalCookie.Name; } set { _internalCookie.Name = value; } }
            public string Value { get { return _internalCookie.Value; } set { _internalCookie.Value = value; } }
            public string Path { get { return _internalCookie.Path; } set { _internalCookie.Path = value; } }
            public System.DateTime Expires { get { return _internalCookie.Expires; } set { _internalCookie.Expires = value; } }

            public WebAppCookie(System.Web.HttpCookie cookie)
            {
                _internalCookie = cookie;
            }

            public WebAppCookie(string name, string value)
            {
                _internalCookie = new System.Web.HttpCookie(name, value);
            }

            public System.Web.HttpCookie GetWebCookie()
            {
                return _internalCookie;
            }

            public System.Net.Cookie GetNetCookie()
            {
                throw new System.TypeAccessException("Cannot Cast System.Web.HttpCookie to System.Net.Cookie");
            }
        }
    }
}
