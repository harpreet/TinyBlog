using System.Web.Mvc;
using System.Web.Routing;
using MvcContrib.PortableAreas;
using TinyBlog.Interface;

namespace Links.Links
{
    //public class LinksRegistration : IPluginRegistration
    //{
    //    public string AreaName
    //    {
    //        get { return "Links"; }
    //    }

    //    public void RegisterPlugin(RouteCollection context)
    //    {
    //        // RegisterTheViewsInTheEmbeddedViewEngine(GetType());
    //        //context.MapRoute("links", "links/{controller}/{action}", new {controller = "links", action = "index"});
    //    }
    //}

    public class LinksRegistration : PortableAreaRegistration
    {



        public override string AreaName
        {
            get { return "Links"; }
        }

        public override void RegisterArea(AreaRegistrationContext context, IApplicationBus bus)
        {
            RegisterAreaEmbeddedResources();
            context.MapRoute("links", "Links/{action}", new { controller = "Links", action = "index" });


        }
    }



}
