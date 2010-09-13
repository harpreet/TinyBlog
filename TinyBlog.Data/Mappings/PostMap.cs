using FluentNHibernate.Mapping;
using TinyBlog.Objects;

namespace TinyBlog.Data.Mappings
{
    public sealed class PostMap : ClassMap<Post>
    {
        public PostMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Title).Not.Nullable();
            Map(x => x.Slug).Not.Nullable().Index("post_slug_idx");
            Map(x => x.Body).Not.Nullable().Length(100000);
            Map(x => x.CommentsOpen).Not.Nullable();
            Map(x => x.Modified).Not.Nullable();
            Map(x => x.Published).Index("post_published_idx");
            Map(x => x.IsPage).Not.Nullable().Default("False");
            HasManyToMany(x => x.Tags).AsBag().Table("TagToPost").ParentKeyColumn("Post_id").ChildKeyColumn("Tag_id").Cascade.All().Not.LazyLoad();
            HasMany<Comment>(x => x.Comments).KeyColumn("ParentPost_id").AsBag().Inverse().Not.LazyLoad();
            LazyLoad();
            
        }
    }
}
