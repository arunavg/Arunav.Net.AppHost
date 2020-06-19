
namespace Arunav.Net.AppBase
{
    public class WebAppCookieCollection : AppCookieCollection
    {
        private readonly System.Web.HttpCookieCollection _internalCookies;

        public WebAppCookieCollection(System.Web.HttpCookieCollection cookies) : base(cookies)
        {
            _internalCookies = cookies;
        }

        public override void Add(AppCookie cookie)
        {
            _internalCookies.Add((System.Web.HttpCookie)cookie);
            AddBaseCookie(cookie);
        }
    }
}