using FluentNHibernate.Mapping;
using TinyBlog.Objects;

namespace TinyBlog.Data.Mappings
{
    public sealed class SiteMap : ClassMap<Site>
    {
        public SiteMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.CommentingEnabled);
            Map(x => x.Description);
            Map(x => x.Name);
            Map(x => x.TimeZoneOffset);
            Map(x => x.PageFooterScript);
            Map(x => x.PostFooterScript);
            Map(x => x.AlternateFeedUrl);
            Map(x => x.ModerateComments);

        }
    }
}
