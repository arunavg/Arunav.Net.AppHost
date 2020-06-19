using Arunav.Net.AppBase;
using System;
using System.Web;

namespace Arunav.Net.TestApp
{
    public class DefaultHandler : IHttpHandler
    {
        public bool IsReusable
        {
            //for AppHost framework the Handler must be reusable
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //Here is the trick we do not use the HttpContext rather use the AppContext.Current
            ShowPage(AppContext.Current);
        }

        private static void ShowPage(AppContext context)
        {
            bool isPostBack = "POST".Equals(context.Request.HttpMethod, StringComparison.Ordinal);
            string message = isPostBack ? string.Concat("Hello ", context.Request.Form["txtFullName"], "!") : "Hello World!";

            context.Response.Write(HtmlTags.HeadStartFormat, "Test Handler");
            context.Response.Write(HtmlTags.HeadMetaTags);
            context.Response.Write(HtmlTags.PageStyleTags);
            context.Response.Write(HtmlTags.BodyStartOnloadFormat, "Test Handler - AppHost Server!");
            context.Response.Write(HtmlTags.DivMessageLabel);
            context.Response.Write(HtmlTags.UserFormFields);
            context.Response.Write(HtmlTags.GetMessageScript(message));
            context.Response.Write(HtmlTags.DivFormBodyHtmlClose);
        }
    }
}
