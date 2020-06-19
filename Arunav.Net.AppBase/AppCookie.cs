namespace Arunav.Net.AppBase
{
    public partial class AppCookie
    {
        private IAppCookie _internalCookie;

        public string Name { get { return _internalCookie.Name; } set { _internalCookie.Name = value; } }
        public string Value { get { return _internalCookie.Value; } set { _internalCookie.Value = value; } }
        public string Path { get { return _internalCookie.Path; } set { _internalCookie.Path = value; } }
        public System.DateTime Expires { get { return _internalCookie.Expires; } set { _internalCookie.Expires = value; } }

        public AppCookie(string name, string value)
        {
            switch (AppContext.ContextType)
            {
                case AppContextType.Net:
                    _internalCookie = new NetAppCookie(name, value);
                    break;
                case AppContextType.Web:
                    _internalCookie = new WebAppCookie(name, value);
                    break;
            }
        }

        public AppCookie(System.Net.Cookie cookie)
        {
            _internalCookie = new NetAppCookie(cookie);
        }

        public AppCookie(System.Web.HttpCookie cookie)
        {
            _internalCookie = new WebAppCookie(cookie);
        }

        protected void SetBaseCookie(IAppCookie cookie)
        {
            _internalCookie = cookie;
        }

        public static explicit operator System.Web.HttpCookie(AppCookie appCookie)
        {
            return appCookie._internalCookie.GetWebCookie();
        }

        public static explicit operator System.Net.Cookie(AppCookie appCookie)
        {
            return appCookie._internalCookie.GetNetCookie();
        }

        protected interface IAppCookie
        {
            string Name { get; set; }
            string Value { get; set; }
            string Path { get; set; }
            System.DateTime Expires { get; set; }
            System.Web.HttpCookie GetWebCookie();
            System.Net.Cookie GetNetCookie();
        }
    }
}
