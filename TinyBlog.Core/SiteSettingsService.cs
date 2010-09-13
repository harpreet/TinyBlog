using System.Web;
using TinyBlog.Objects;
using TinyBlog.Interface;

namespace TinyBlog.Core
{
    public class SiteSettingsService : ISiteSettingsService
    {
        private readonly ISiteSettingsRepository _siteSettingsRepository;
        private readonly HttpContextBase _context;

        public bool CommentingEnabled()
        {
            return GetSiteSettings().CommentingEnabled;
        }


        public string GetFooterScript()
        {
            return GetSiteSettings().PageFooterScript ?? string.Empty;
        }

        public SiteSettingsService(ISiteSettingsRepository siteSettingsRepository, HttpContextBase context)
        {
            _siteSettingsRepository = siteSettingsRepository;
            _context = context;
        }

        public void SaveSiteSettings(Site site)
        {
            _siteSettingsRepository.SaveSettings(site);
        }
        
        public Site GetSiteSettings()
        {
            var settings = _siteSettingsRepository.GetSiteSettings() ?? new Site();

            settings.SiteHomeUrl = string.Format("{0}://{1}/", _context.Request.Url.Scheme,
                                                 _context.Request.Url.Authority);

            return settings;
        }
    }
}
