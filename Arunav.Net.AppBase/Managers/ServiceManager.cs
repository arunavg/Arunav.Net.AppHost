
namespace Arunav.Net.AppBase.Managers
{
    internal class ServiceManager : IAppManager
    {
        private IAppServiceModule _mainModule;

        public void Initialize(object appModule, System.Reflection.Assembly appAssembly)
        {
            _mainModule = (IAppServiceModule)appModule;
        }

        public void Start(string hostUrl, System.Collections.Specialized.NameValueCollection mimeTypes)
        {
            _mainModule.Start(hostUrl);
        }

        public void Stop()
        {
            _mainModule.Stop();
        }
    }
}
