
namespace Arunav.Net.AppBase
{
    public class WebAppContext : AppContext
    {
        private readonly System.Web.HttpContext _context;
        private readonly IAppRequest _request;
        private readonly IAppResponse _response;

        public override System.Collections.IDictionary Items { get { return _context.Items; } }

        public override System.Web.Caching.Cache Cache { get { return _context.Cache; } }

        public override IAppRequest Request { get { return _request; } }

        public override IAppResponse Response { get { return _response; } }

        public WebAppContext(System.Web.HttpContext context) : base(AppContextType.Web)
        {
            _context = context;
            _request = new WebAppRequest(context.Request);
            _response = new WebAppResponse(context.Response);
        }

        public override void RewritePath(string path)
        {
            _context.RewritePath(path);
        }
    }
}
