using System.Collections.Generic;
using System.Linq;
using TinyBlog.Objects;

namespace TinyBlog.Models
{
    public class PostListViewModel
    {
        public PostListViewModel()
        {
            PageSize = 1;
        }

        public virtual IEnumerable<Post> Posts { get; set; }
        public virtual int CurrentCount {
            get
            {
                return Posts == null ? 0 : Posts.Count();
            }
        }
        public virtual Post FirstPost
        {
            get
            {
                if (CurrentCount == 0) return null;
                return Posts.First();
            }
        }
        public virtual bool IsForSinglePost
        {
            get
            {
                return CurrentCount == 1 && PageNumber <= 1;
            }
        }

        public virtual bool IsForTag { get { return !string.IsNullOrEmpty(Tag); } }
        public virtual string Tag { get; set; }
        public virtual int TotalCount { get; set; }
        public virtual int PageNumber { get; set; }
        public virtual int PageSize { get; set; }
        public virtual bool HasNextPage { get { return ((decimal)TotalCount/PageSize) > PageNumber; }}
        public virtual bool HasPreviousPage { get { return PageNumber > 1; } }

        public string NextPageLink { get; set; }
        public string PreviousPageLink { get; set; }
    }
}