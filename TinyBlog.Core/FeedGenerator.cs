using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using TinyBlog.Objects;

namespace TinyBlog.Core
{
    public class FeedGenerator
    {

        private readonly string _siteRoot;

        public FeedGenerator(HttpContextBase context)
        {
            _siteRoot = SiteRoot(context, true);
        }

        public SyndicationFeed Generate(IList<Post> recentPosts)
        {
            var feed = new SyndicationFeed("blog title", "blog description", new Uri(_siteRoot))
                           {
                               Items = ConvertPostsToSyndicationItems(recentPosts)
                           };

            return feed;
        }

        public List<SyndicationItem> ConvertPostsToSyndicationItems(IList<Post> posts)
        {
            if (posts == null) return new List<SyndicationItem>();

            var result = posts.Select(p => ConvertPostToSyndicationItem(p)).ToList();

            return result;
        }

        public SyndicationItem ConvertPostToSyndicationItem(Post post)
        {
            if (post == null) return null;


            SyndicationItem syndicationItem = new SyndicationItem(post.Title, post.Body, new Uri(_siteRoot + post.Slug))
                                                  {
                                                      PublishDate = post.Published,
                                                      Title = new TextSyndicationContent(post.Title ?? string.Empty)
                                                  };

            return syndicationItem;
        }


        public static string SiteRoot(HttpContextBase context, bool usePort)
        {
            var result = string.Format("{0}://{1}/", context.Request.Url.Scheme, context.Request.Url.Authority);
            return result;
        }
    }
}