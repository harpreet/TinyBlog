using System.Web.Mvc;
using System.Web.Routing;

namespace TinyBlog.Interface
{
    public interface IPluginRegistration
    {
        string AreaName { get; }
        void RegisterPlugin(RouteCollection context);
    }
}