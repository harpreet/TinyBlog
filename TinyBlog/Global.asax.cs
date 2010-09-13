using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcContrib.PortableAreas;
using MvcContrib.UI.InputBuilder.ViewEngine;
using StructureMap;
using TinyBlog.Controllers;
using TinyBlog.Core;
using TinyBlog.Objects;
using TinyBlog.Interface;
using System.Web.Security;
using TinyBlog.Models;

namespace TinyBlog
{
    public class MetaWeblogRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new MetaWeblog.MetaWeblog();
        }
    }

    public class MvcApplication : HttpApplication
    {
        private static string _routePrefix;
        public static string RoutePrefix
        {
            get { return _routePrefix ?? (_routePrefix = ConfigurationManager.AppSettings["RouteUrlPrefix"]); }
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("Content/{*pathInfo}");
            routes.IgnoreRoute("Scripts/{*pathInfo}");
            routes.IgnoreRoute("ckeditor/{*pathInfo}");
            routes.IgnoreRoute("ClientBin/{*pathInfo}");

            // not yet ready
            //routes.Add(new Route("api", new MetaWeblogRouteHandler()));

            routes.MapRoute(
                RouteNames.LogOn,
                "logon",
                new {controller = "Account", action = "LogOn"}
                );

            routes.MapRoute(
                RouteNames.LogOff,
                "logoff",
                new {controller = "Account", action = "LogOff"}
                );

            routes.MapRoute(
                RouteNames.FeedUrl,
                "feedurl",
                new {controller = "Feed", action = "FeedUrl"}
                );

            routes.MapRoute(
                RouteNames.Header,
                "header",
                new {controller = "Header", action = "Header"}
                );

            routes.MapRoute(
                RouteNames.Author,
                "author",
                new {controller = "Posts", action= "Create"}
                );

            routes.MapRoute(
                RouteNames.Pages,
                "pages",
                new {controller = "Pages", action = "Index"}
                );

            routes.MapRoute(
                RouteNames.Footer,
                "footer",
                new { controller = "Footer", action = "Index" }
                );
            
            routes.MapRoute(
                RouteNames.RSSFeed,
                "rss",
                new { controller = "Feed", action = "GetFeed", atomOrRss = "rss" }
                );

            routes.MapRoute(
                RouteNames.Admin,
                "admin",
                new { controller = "Admin", action = "Index" }
                );

            routes.MapRoute(
                RouteNames.Settings,
                "settings",
                new { controller = "Admin", action = "SiteSettings"}
                );

            routes.MapRoute(
                RouteNames.ModerateComments,
                "moderate",
                new { controller = "Admin", action = "ModerateComments" }
                );

            routes.MapRoute(
                RouteNames.Category,
                "category/{categoryName}",
                new { controller = "Posts", action = "ShowPostsInCategory", categoryName = string.Empty, pageNumber = 1 });

            routes.MapRoute(
                RouteNames.CategoryWithPage,
                "category/{categoryName}/page/{pageNumber}",
                new { controller = "Posts", action = "ShowPostsInCategory", categoryName = string.Empty, pageNumber = 1 });

            routes.MapRoute(
                RouteNames.PostsWithPage,
                "page/{pageNumber}",
                new { controller = "Posts", action = "Index", pageNumber = 1 }
            );

            routes.MapRoute(
                RouteNames.EditPost,
                "{postSlug}/edit",
                new { controller = "Posts", action = "Edit", postSlug = string.Empty }
                );

            routes.MapRoute(
                RouteNames.Post,
                "{postSlug}",
                new { controller = "Posts", action = "ShowPost", id = string.Empty }
            );

            routes.MapRoute(
                RouteNames.Posts,
                "",
                new {controller = "Posts", action = "Index", id = string.Empty, pageNumber = 1}
                );

            routes.MapRoute(
                "testtest",
                "{controller}/{action}",
                new { controller = "Posts", action = "Index", pageNumber = 1 }
            );

            routes.MapRoute(
                RouteNames.Default,                                               
                "{controller}/{action}/{id}",                           
                new { controller = "Posts", action = "Index", id = string.Empty, pageNumber = 1 }  
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
            ConfigureContainer();

            var customControllerFactory = new CustomControllerFactory();

            customControllerFactory.AddControllers(ObjectFactory.GetAllInstances<PluginController>().Select(x => x.GetType()));

            ControllerBuilder.Current.SetControllerFactory(customControllerFactory);

            System.Web.Hosting.HostingEnvironment.RegisterVirtualPathProvider(new AssemblyResourceProvider());

            ModelBinders.Binders.Add(typeof(Post), new PostBinder());
            ModelBinders.Binders.Add(typeof(CreateCommentModel), new CreateCommentModelBinder());

            MakeSureApplicationIsInitialized();

        }

        public static bool AppIsSetup { get; private set; }

        private void MakeSureApplicationIsInitialized()
        {
            var membershipService = ObjectFactory.GetInstance<IMembershipService>();
            if (membershipService.ValidUserExists())
            {
                AppIsSetup = true;
            }
        }

        private bool _goneToSetup;
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (!AppIsSetup && !_goneToSetup)
            {
                _goneToSetup = true;                var redirectTargetDictionary = new RouteValueDictionary {{"action", "Setup"}, {"controller", "Setup"}};                Response.RedirectToRoute(redirectTargetDictionary);
                return;
            }

            string sitePrefix = string.Empty;

            if (!Request.Url.Authority.StartsWith("localhost") && !Request.Url.Authority.StartsWith("www.")) sitePrefix = "www.";

            var pathAndQuery = string.IsNullOrEmpty(Request.Url.PathAndQuery) ? "/" : Request.Url.PathAndQuery;

            if (!pathAndQuery.EndsWith("/") && pathAndQuery.Contains("/category"))
            {
                pathAndQuery += "/";
            }

            var url = string.Format("{0}://{1}{2}{3}", Request.Url.Scheme, sitePrefix, Request.Url.Authority.ToLower(), pathAndQuery);
            var originalUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority.ToLower(), Request.Url.PathAndQuery);

            if (url != originalUrl) Response.RedirectPermanent(url, true);
        }

        protected void Application_EndRequest()
        {
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }


        protected void ConfigureContainer()
        {
            ObjectFactory.Initialize(s =>
                                         {
                                             s.Scan(x =>
                                                        {
                                                            x.AssembliesFromPath(HttpRuntime.BinDirectory,
                                                                                 assembly =>
                                                                                 (!assembly.GetName().Name.Contains(
                                                                                     "NHibernate") &&
                                                                                  !assembly.GetName().Name.Contains(
                                                                                      "MvcContrib") &&
                                                                                  !assembly.GetName().Name.Contains(
                                                                                      "Castle")

                                                                                 ));
                                                            x.WithDefaultConventions();
                                                            x.AddAllTypesOf<IMembershipService>();
                                                            x.AddAllTypesOf<IPluginRegistration>();
                                                            x.AddAllTypesOf<PluginController>();
                                                            x.AddAllTypesOf<PortableAreaRegistration>();
                                                            x.LookForRegistries();
                                                        });
                                             
                                         });

        }

    }


}