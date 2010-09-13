using System.Web.Mvc;
using TinyBlog.Objects;
using TinyBlog.Interface;

namespace TinyBlog.Controllers
{
    [ActionOutputCache(360)]
    public class FooterController : Controller
    {
        private readonly ISiteSettingsService _siteSettingsService;

        public FooterController(ISiteSettingsService siteSettingsService)
        {
            _siteSettingsService = siteSettingsService;
        }

        public virtual ActionResult Index()
        {
            Site settings = _siteSettingsService.GetSiteSettings();
            return View(settings);
        }
    }
}
