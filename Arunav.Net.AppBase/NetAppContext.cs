
namespace Arunav.Net.AppBase
{
    public class NetAppContext : AppContext
    {
        //private readonly System.Net.HttpListenerContext _context;
        private readonly NetAppRequest _request;
        private readonly NetAppResponse _response;

        private readonly System.Collections.IDictionary _items;

        private static readonly System.Web.Caching.Cache _cache = new System.Web.Caching.Cache();

        public override System.Collections.IDictionary Items { get { return _items; } }

        public override System.Web.Caching.Cache Cache { get { return _cache; } }

        public override IAppRequest Request { get { return _request; } }

        public override IAppResponse Response { get { return _response; } }

        public NetAppContext(System.Net.HttpListenerContext context) : base(AppContextType.Net)
        {
            //_context = context;
            _request = new NetAppRequest(context.Request);
            _response = new NetAppResponse(context.Response);
            _items = new System.Collections.Generic.Dictionary<string, object>();
        }

        public override void RewritePath(string path)
        {
            _request.RewriteUrl(path);
        }
    }
}
