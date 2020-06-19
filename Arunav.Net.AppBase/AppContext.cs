
namespace Arunav.Net.AppBase
{
    public abstract class AppContext
    {
        [System.ThreadStatic]
        private static AppContextType _contextType;

        [System.ThreadStatic]
        private static AppContext _currentContext;

        public static AppContextType ContextType
        {
            get { return _contextType; }
        }

        public static AppContext Current
        {
            get { return _currentContext; }
        }

        public abstract System.Collections.IDictionary Items { get; }
        public abstract System.Web.Caching.Cache Cache { get; }
        public abstract IAppRequest Request { get; }
        public abstract IAppResponse Response { get; }

        public AppContext(AppContextType contextType)
        {
            _contextType = contextType;
            _currentContext = this;
        }

        public abstract void RewritePath(string path);
    }

    public enum AppContextType
    {
        Web,
        Net
    }
}
