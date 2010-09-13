using System;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;
using TinyBlog.Interface;

namespace TinyBlog.Controllers
{
    [ActionOutputCache(60)]
    public class FeedController : Controller
    {
        private readonly IContentService _contentService;
        private readonly ISiteSettingsService _siteSettingsService;

        public FeedController(IContentService contentService, ISiteSettingsService siteSettingsService)
        {
            _contentService = contentService;
            _siteSettingsService = siteSettingsService;
        }

        [ChildActionOnly]
        public virtual ActionResult FeedUrl()
        {
            var settings = _siteSettingsService.GetSiteSettings();

            if (settings != null && !string.IsNullOrEmpty(settings.AlternateFeedUrl))
            {
                return View(new Uri(settings.AlternateFeedUrl));
            }
            
            var feedUri = new Uri(string.Format("{0}://{1}/{2}", Request.Url.Scheme,  Request.Url.Authority.ToLower(), "rss"));

            return View(feedUri);
        }

        public virtual FeedActionResult GetFeed(string atomOrRss)
        {
            if (atomOrRss == null) atomOrRss = FeedType.Rss.ToString();

            if (atomOrRss.Equals(FeedType.Atom.ToString(), StringComparison.InvariantCultureIgnoreCase))
                return Atom();
            else 
                return RSS();
        }

        public virtual FeedActionResult RSS()
        {
            var feed = _contentService.GetFeed();
            return new FeedActionResult(FeedType.Rss, feed);
        }

        public virtual FeedActionResult Atom()
        {
            var feed = _contentService.GetFeed();
            return new FeedActionResult(FeedType.Atom, feed);
        }
    }



    public class FeedActionResult : ActionResult
    {
        private readonly FeedType _feedType;
        private readonly SyndicationFeed _syndicationFeed;

        public FeedActionResult(FeedType feedType, SyndicationFeed syndicationFeed)
        {
            _feedType = feedType;
            _syndicationFeed = syndicationFeed;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            SyndicationFeedFormatter formatter;
            string contentType;

            switch (_feedType)
            {
                case FeedType.Atom:
                    formatter = new Atom10FeedFormatter(_syndicationFeed);
                    contentType = "application/atom+xml";
                    break;
                case FeedType.Rss:
                    formatter = new Rss20FeedFormatter(_syndicationFeed);
                    contentType = "application/rss+xml";
                    break;
                default:
                    throw new NotImplementedException("Feed type not accounted for");
            }

            context.HttpContext.Response.ContentType = contentType;

            using (var writer = XmlWriter.Create(context.HttpContext.Response.Output))
            {
                formatter.WriteTo(writer);
            }
        }

    }

    public enum FeedType
    {
        Atom,
        Rss
    }

}
