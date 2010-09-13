using System.Collections.Generic;

namespace TinyBlog.Objects
{
    public class Tag : EntityBase
    {
        public virtual string Name { get; set; }
        public virtual IList<Post> Posts { get; set; }
        public override string ToString()
        {
            return Name ?? string.Empty;
        }
    }
}
