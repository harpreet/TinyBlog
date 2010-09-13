using FluentNHibernate.Mapping;
using TinyBlog.Objects;

namespace TinyBlog.Data.Mappings
{
    public sealed class CommentMap : ClassMap<Comment>
    {
        public CommentMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.AuthorEmail);
            Map(x => x.AuthorIP);
            Map(x => x.AuthorName);
            Map(x => x.AuthorUrl);
            Map(x => x.Body).Length(10000);
            Map(x => x.Created);
            Map(x => x.IsApproved).Index("comment_isapproved_idx");
            References(x => x.ParentPost).Not.Nullable().LazyLoad().Index("comment_post_idx");
        }
    }
}
