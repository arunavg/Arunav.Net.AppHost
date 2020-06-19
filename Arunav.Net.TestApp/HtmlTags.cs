
namespace Arunav.Net.TestApp
{
    internal static class HtmlTags
    {
        public const string HeadStartFormat = "<!DOCTYPE html>\n<html>\n<head>\n\t<title>{0}</title>";
        public const string HeadMetaTags = "\n\t<meta http-equiv='content-type' content='text/html; charset=utf-8' />\n\t<meta name='viewport' content='initial-scale=1.0, width=device-width' />\n\t<meta name='description' content='This is a test handler page...' />";
        public const string PageStyleTags = "\n\t<link href='Assets/Dialog.css' rel='stylesheet' type='text/css' />\n\t<script src='Assets/Dialog.js' type='text/javascript'></script>";
        public const string BodyStartOnloadFormat = "\n</head>\n<body onload='showDialog(\"divUserDialog\", \"txtFullName\");'>\n\t<form id='mainForm' method='post'>\n\t\t<div>\n\t\t\t<div id='divUserDialog' style='visibility: hidden;'>\n\t\t\t\t<div id='divHeader' class='header'>\n\t\t\t\t\t<span id='rowTitle'>{0}</span>\n\t\t\t\t</div>";
        public const string DivMessageLabel = "\n\t\t\t\t<div style='text-align: center; height: 28px;'>\n\t\t\t\t\t<span id='lblMessage' style='color:Red;'></span>\n\t\t\t\t</div>\n\t\t\t\t<div style='font-size: 11px; padding: 8px;'>Arunav.Net.AppHost is a lightweight web server for hosting asp.net web apps that contain http module and handlers. It supports GET and POST methods.<br/>Use this form to test FORM POST.</div>";
        public const string UserFormFields = "\n\t\t\t\t<label>Full Name</label>\n\t\t\t\t<input name='txtFullName' type='text' id='txtFullName' class='txtbox' />\n\t\t\t\t<div class='tdSubmit'>\n\t\t\t\t\t<input type='submit' name='btnSubmit' value='Submit' onclick='return validateFunction(event);' id='btnSubmit' />\n\t\t\t\t</div>";
        public const string DivFormBodyHtmlClose = "\n\t\t\t</div>\n\t\t</div>\n\t</form>\n</body>\n</html>";
        public const string LineTab4 = "\n\t\t\t\t";
        public const string ScriptOpenTag = "<script type=\"text/javascript\">";
        public const string ScriptCloseTag = "</script>";

        public static string GetMessageScript(string message)
        {
            System.Text.StringBuilder scriptBuilder = new System.Text.StringBuilder();
            scriptBuilder.Append(message).Replace("\\", "\\\\").Replace("'", "\\'");
            string safeValue = scriptBuilder.ToString();
            scriptBuilder.Clear();
            scriptBuilder.Append(LineTab4);
            scriptBuilder.Append(ScriptOpenTag);
            scriptBuilder.Append(LineTab4);
            scriptBuilder.AppendFormat("\tdocument.getElementById('lblMessage').innerHTML = '{0}';", safeValue);
            scriptBuilder.Append(LineTab4);
            scriptBuilder.Append(ScriptCloseTag);
            return scriptBuilder.ToString();
        }
    }
}
