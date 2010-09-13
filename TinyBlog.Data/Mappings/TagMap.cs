using FluentNHibernate.Mapping;
using TinyBlog.Objects;

namespace TinyBlog.Data.Mappings
{
    public sealed class TagMap : ClassMap<Tag>
    {
        public TagMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Not.Nullable().Unique();
            HasManyToMany(x => x.Posts).Table("TagToPost").ParentKeyColumn("Tag_id").ChildKeyColumn("Post_id").Inverse();
        }
    }
}
 