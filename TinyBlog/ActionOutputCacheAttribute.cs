using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace TinyBlog
{
    /// <summary>
    /// From http://blog.stevensanderson.com/2008/10/15/partial-output-caching-in-aspnet-mvc/
    /// </summary>
    public class ActionOutputCacheAttribute : ActionFilterAttribute
    {
        public ActionOutputCacheAttribute(int cacheDuration)
        {
            CacheDuration = cacheDuration;
        }

        // This hack is optional; I'll explain it later in the blog post
        private static readonly MethodInfo _switchWriterMethod = typeof(HttpResponse).GetMethod("SwitchWriter", BindingFlags.Instance | BindingFlags.NonPublic);

        public int CacheDuration { get; set; }

        private TextWriter _originalWriter;
        private string _cacheKey;


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated) return; // avoid caching views that are credential based

            _cacheKey = ComputeCacheKey(filterContext);
            string cachedOutput = (string)filterContext.HttpContext.Cache[_cacheKey];
            if (cachedOutput != null)
                filterContext.Result = new ContentResult { Content = cachedOutput };
            else
                _originalWriter = (TextWriter)_switchWriterMethod.Invoke(HttpContext.Current.Response, new object[] { new HtmlTextWriter(new StringWriter()) });
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated) return; // avoid caching views that are credential based

            if (_originalWriter != null) // Must complete the caching
            {
                HtmlTextWriter cacheWriter = (HtmlTextWriter)_switchWriterMethod.Invoke(HttpContext.Current.Response, new object[] { _originalWriter });
                string textWritten = cacheWriter.InnerWriter.ToString();
                filterContext.HttpContext.Response.Write(textWritten);

                filterContext.HttpContext.Cache.Add(_cacheKey, textWritten, null, DateTime.Now.AddSeconds(CacheDuration), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }
        }

        private string ComputeCacheKey(ActionExecutingContext filterContext)
        {
            var keyBuilder = new StringBuilder();
            foreach (var pair in filterContext.RouteData.Values)
                keyBuilder.AppendFormat("rd{0}_{1}_", pair.Key.GetHashCode(), pair.Value.GetHashCode());
            foreach (var pair in filterContext.ActionParameters)
                keyBuilder.AppendFormat("ap{0}_{1}_", pair.Key.GetHashCode(), pair.Value.GetHashCode());
            return keyBuilder.ToString();
        }
    }
}