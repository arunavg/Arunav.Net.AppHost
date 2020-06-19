using Arunav.Net.AppBase;
using System;
using System.Web;

namespace Arunav.Net.TestApp
{
    public class MainModule : IHttpModule, IAppRouteModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
        }

        private void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication httpApp = (HttpApplication)sender;
            AppContext context = new WebAppContext(httpApp.Context);
            RouteRequest(context);
        }

        public bool RouteRequest(AppContext context)
        {
            string fileNamePart = context.Request.Url.Segments[context.Request.Url.Segments.Length - 1];
            if (fileNamePart.IndexOf('.') < 0)
            {
                if (fileNamePart[fileNamePart.Length - 1] == '/')
                    context.RewritePath(context.Request.QueryString.Count > 0 ? ("Default.ashx?" + context.Request.QueryString.ToString()) : "Default.ashx");
                else
                    context.Response.RedirectPermanent(context.Request.QueryString.Count > 0 ? (fileNamePart + "/?" + context.Request.QueryString.ToString()) : fileNamePart + '/');
            }
            return true;
        }

        public void Dispose()
        {
            //Not required for this stage
        }
    }
}
