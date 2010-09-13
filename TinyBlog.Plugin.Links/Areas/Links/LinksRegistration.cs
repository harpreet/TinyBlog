using System.Web.Mvc;
using MvcContrib.PortableAreas;

namespace Links.Areas.Links
{

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
