namespace Arunav.Net.AppBase
{
    public interface IAppServiceModule
    {
        void Start(string hostUrl);
        void Stop();
    }
}
