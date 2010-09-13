using FluentNHibernate.Mapping;
using TinyBlog.Objects;

namespace TinyBlog.Data.Mappings
{
    public sealed class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.UserId).Not.Nullable();
            Map(x => x.Role);
            Map(x => x.FullName);
            Map(x => x.EmailAddress);
            Map(x => x.Password);
            Map(x => x.PasswordIsHashed);

        }
    }
}
