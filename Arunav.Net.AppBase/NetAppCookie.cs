namespace Arunav.Net.AppBase
{
    public partial class AppCookie
    {
        private class NetAppCookie : IAppCookie
        {
            private readonly System.Net.Cookie _internalCookie;

            public string Name { get { return _internalCookie.Name; } set { _internalCookie.Name = value; } }
            public string Value { get { return _internalCookie.Value; } set { _internalCookie.Value = value; } }
            public string Path { get { return _internalCookie.Path; } set { _internalCookie.Path = value; } }
            public System.DateTime Expires { get { return _internalCookie.Expires; } set { _internalCookie.Expires = value; } }

            public NetAppCookie(System.Net.Cookie cookie)
            {
                _internalCookie = cookie;
            }

            public NetAppCookie(string name, string value)
            {
                _internalCookie = new System.Net.Cookie(name, value);
            }

            public System.Web.HttpCookie GetWebCookie()
            {
                throw new System.TypeAccessException("Cannot Cast System.Net.Cookie to System.Web.HttpCookie");
            }

            public System.Net.Cookie GetNetCookie()
            {
                return _internalCookie;
            }
        }
    }
}
