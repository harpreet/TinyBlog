using System.ComponentModel.DataAnnotations;

namespace TinyBlog.Objects
{
    public class Site : EntityBase
    {
        public virtual string Name { get; set; }
        public virtual string SiteHomeUrl { get; set; }
        public virtual string Description { get; set; }
        public virtual int TimeZoneOffset { get; set; }
        public virtual bool CommentingEnabled { get; set; }
        public virtual string AlternateFeedUrl { get; set; }
        public virtual string PostFooterScript { get; set; }
        public virtual string PageFooterScript { get; set; }
        public virtual string Disclaimer { get; set; }
        public virtual bool ModerateComments { get; set; }
    }
}
