using Arunav.Net.AppBase.Managers;
using System;
using System.IO;

namespace Arunav.Net.AppBase
{
    public class AppInstance : MarshalByRefObject
    {
        private IAppManager _appManager;

        public void Initialize(string appType, string moduleTypeName)
        {
            int commaPos = moduleTypeName.LastIndexOf(',');
            string assemblyFilePath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, Path.DirectorySeparatorChar, moduleTypeName.Substring(commaPos + 1).Trim(), ".dll");
            System.Reflection.Assembly externalAssembly = System.Reflection.Assembly.LoadFrom(assemblyFilePath);
            Type externalType = externalAssembly.GetType(moduleTypeName.Substring(0, commaPos));
            if (externalType == null)
                throw new ArgumentException("Type '{0}' Not Found!", moduleTypeName.Substring(0, commaPos));
            object appModule = Activator.CreateInstance(externalType);
            _appManager = AppManagerFactory.GetManager(appType);
            _appManager.Initialize(appModule, externalAssembly);
        }

        public void Start(string hostUrl, System.Collections.Specialized.NameValueCollection mimeTypes)
        {
            _appManager.Start(hostUrl, mimeTypes);
        }

        public void Stop()
        {
            _appManager.Stop();
        }
    }
}
