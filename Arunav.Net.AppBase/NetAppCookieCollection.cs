
namespace Arunav.Net.AppBase
{
    public class NetAppCookieCollection : AppCookieCollection
    {
        private readonly System.Net.CookieCollection _internalCookies;

        public NetAppCookieCollection(System.Net.CookieCollection cookies) : base(cookies)
        {
            _internalCookies = cookies;
        }

        public override void Add(AppCookie cookie)
        {
            _internalCookies.Add((System.Net.Cookie)cookie);
            AddBaseCookie(cookie);
        }
    }
}