
namespace Arunav.Net.AppBase.Managers
{
    internal static class AppManagerFactory
    {
        public static IAppManager GetManager(string appType)
        {
            //AppType can be a asp.net web (module and handlers) or a wcf service
            //Default to Web when appType not specified
            if (string.IsNullOrWhiteSpace(appType))
                return new WebManager();

            appType = appType.ToUpperInvariant();

            switch (appType)
            {
                case "WEB":
                    return new WebManager();
                case "SERVICE":
                    return new ServiceManager();
                default:
                    return new WebManager();
            }
        }
    }
}
