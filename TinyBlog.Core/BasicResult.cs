using System.Web.Mvc;

namespace TinyBlog.Core
{
    public class BasicResult : ActionResult
    {

        public override void ExecuteResult(ControllerContext context)
        {

            context.HttpContext.Response.ContentType = "text/html";

            context.HttpContext.Response.Output.WriteLine("hello from plugin");
            context.HttpContext.Response.Output.Flush();
        }

    }
}
