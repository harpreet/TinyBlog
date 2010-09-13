using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TinyBlog
{
    public static class Extensions
    {
        public static void RenderPlugin(this HtmlHelper html, string pluginName)
        {
            string actionName = "Index";
            string controllerName = pluginName;
            ControllerContext context = html.ViewContext;
            RouteData rd = new RouteData(context.RouteData.Route, context.RouteData.RouteHandler);

            rd.Values.Add("action", actionName);
            rd.Values.Add("controller", controllerName);

            IHttpHandler handler = new MvcHandler(new RequestContext(context.HttpContext, rd));
            handler.ProcessRequest(System.Web.HttpContext.Current);

        }


        public static string ToCommaDelimitedString<T>(this IEnumerable<T> enumerable)
        {
            StringBuilder result = new StringBuilder();
            if (enumerable == null) return result.ToString();

            return string.Join(", ", enumerable.ToArray());

            
        }
    }
}