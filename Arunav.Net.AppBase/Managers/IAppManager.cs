
namespace Arunav.Net.AppBase.Managers
{
    internal interface IAppManager
    {
        void Initialize(object appModule, System.Reflection.Assembly appAssembly);
        void Start(string hostUrl, System.Collections.Specialized.NameValueCollection mimeTypes);
        void Stop();
    }
}
