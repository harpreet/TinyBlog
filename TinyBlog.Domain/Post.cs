using System;
using System.Collections.Generic;

namespace TinyBlog.Objects
{
    public class Post : EntityBase
    {
        protected string _title;
        public virtual string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                SetSlug();
            }
        }

        public virtual string Slug { get; set; }
        public virtual string Body { get; set; }
        public virtual DateTime Published { get; set; }

        protected DateTime _modified;
        public virtual DateTime Modified
        {
            get 
            {
                return _modified == DateTime.MinValue ? Published : _modified;
            }
            set { _modified = value; }
        }

        public virtual bool CommentsOpen { get; set; }
        public virtual bool IsPage { get; set; }

        public virtual IList<Tag> Tags { get; set; }
        public virtual IList<Comment> Comments { get; set; }
        public virtual User Author { get; set; }

        protected virtual void SetSlug()
        {
            if (!string.IsNullOrEmpty(Slug) || string.IsNullOrEmpty(_title)) return;

            string slug = _title.Replace(" ", string.Empty);
            Slug = slug;
        }
    }
}
