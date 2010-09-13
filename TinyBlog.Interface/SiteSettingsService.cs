using System;
using System.Web.Mvc;
using TinyBlog.Objects;

namespace TinyBlog.Interface
{
    public interface ISiteSettingsService
    {
        void SaveSiteSettings(Site site);
        Site GetSiteSettings();
        bool CommentingEnabled();
    }
}
