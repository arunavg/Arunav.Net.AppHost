
namespace Arunav.Net.AppBase
{
    public abstract class AppCookieCollection
    {
        private readonly System.Collections.Generic.List<AppCookie> _appCookies;

        public AppCookie this[int index] { get { return _appCookies[index]; } }

        public AppCookie this[string name] { get { return _appCookies.Find(c => name.Equals(c.Name, System.StringComparison.Ordinal)); } }

        public int Count { get { return _appCookies.Count; } }

        public AppCookieCollection(System.Web.HttpCookieCollection cookies)
        {
            _appCookies = new System.Collections.Generic.List<AppCookie>();
            for (int i = 0; i < cookies.Count; i++)
            {
                _appCookies.Add(new AppCookie(cookies[i]));
            }
        }

        public AppCookieCollection(System.Net.CookieCollection cookies)
        {
            _appCookies = new System.Collections.Generic.List<AppCookie>();
            for (int i = 0; i < cookies.Count; i++)
            {
                _appCookies.Add(new AppCookie(cookies[i]));
            }
        }

        protected void AddBaseCookie(AppCookie cookie)
        {
            _appCookies.Add(cookie);
        }

        public abstract void Add(AppCookie cookie);
    }
}