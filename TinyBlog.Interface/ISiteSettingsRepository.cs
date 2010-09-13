using TinyBlog.Objects;

namespace TinyBlog.Interface
{
    public interface ISiteSettingsRepository
    {
        void SaveSettings(Site site);
        Site GetSiteSettings();
    }
}
