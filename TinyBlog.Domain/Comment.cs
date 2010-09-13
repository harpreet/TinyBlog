using System;

namespace TinyBlog.Objects
{
    public class Comment : EntityBase
    {
        public virtual Post ParentPost { get; set; }
        public virtual string Body { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual string AuthorName { get; set; }
        public virtual string AuthorEmail { get; set; }
        public virtual string AuthorUrl { get; set; }
        public virtual string AuthorIP { get; set; }
        public virtual bool IsApproved { get; set; }
    }
}
