using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using TinyBlog.Core;
using TinyBlog.Models;
using TinyBlog.Objects;
using TinyBlog.Interface;

namespace TinyBlog.Controllers
{
    public class PostsController : ContentControllerBase
    {
        public PostsController(IContentService contentService) : base(contentService){}

        [ActionOutputCache(180)]
        public virtual ActionResult Index(int pageNumber)
        {
            var postList = GetPostList(null, pageNumber);
            return View(postList);
        }

        [ActionOutputCache(180)]
        public virtual ActionResult ShowPostsInCategory(string categoryName, int pageNumber)
        {
            var postList = GetPostList(categoryName, pageNumber);
            return View("Index", postList);
        }

        protected PostListViewModel GetPostList(string category, int pageNum)
        {
            PostListViewModel viewModel = new PostListViewModel();
            viewModel.Posts = string.IsNullOrEmpty(category)
                                  ? ContentService.GetPostsPage(pageNum)
                                  : ContentService.GetPostsWithCategory(category, pageNum);
            viewModel.TotalCount = ContentService.GetPostCount(category);
            viewModel.PageNumber = pageNum;
            viewModel.PageSize = ContentService.GetPageSize();
            viewModel.Tag = string.Empty;
            if (viewModel.HasNextPage)
            {
                if (!string.IsNullOrEmpty(category))
                    viewModel.NextPageLink = Url.RouteUrl(RouteNames.CategoryWithPage, new { categoryName = category, pageNumber = pageNum + 1 });
                else
                    viewModel.NextPageLink = Url.RouteUrl(RouteNames.PostsWithPage, new { pageNumber = pageNum + 1 });
            }
            if (viewModel.HasPreviousPage)
            {
                if (!string.IsNullOrEmpty(category))
                    viewModel.PreviousPageLink = Url.RouteUrl(RouteNames.CategoryWithPage, new { categoryName = category, pageNumber = pageNum - 1 });
                else
                    viewModel.PreviousPageLink = Url.RouteUrl(RouteNames.PostsWithPage, new { pageNumber = pageNum - 1 });
            }
            return viewModel;
        }

        [ActionOutputCache(180)]
        public virtual ActionResult ShowPost(string postSlug)
        {
            if (postSlug.IsEmptyOrNull())
            {
                return RedirectToRoute(RouteNames.Posts);
            }
            
            Post p = ContentService.GetPostWithSlug(postSlug);

            if (p == null) return RedirectToRoute(RouteNames.Posts);
            
            if (p.Slug != postSlug || Request.Url.PathAndQuery.EndsWith("/"))
            {
                var url = string.Format("{0}://{1}/{2}", Request.Url.Scheme,  Request.Url.Authority.ToLower(), p.Slug);
                Response.RedirectPermanent(url);
            }

            if (p.IsPage)
            {
                return View("Page", p);
            }

            return View("Index", new PostListViewModel {Posts = new List<Post>{p}});
        }


        [RequiresRole(UserRole.User | UserRole.Admin)]
        public virtual ActionResult Create()
        {
            Post p = ContentService.CreateNewPost();
            p.Tags = new List<Tag>();
            return CreateEditView(p);
        }

        [RequiresRole(UserRole.User | UserRole.Admin)]
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Save(Post p)
        {
            ContentService.SavePost(p);
            return RedirectToRoute(RouteNames.Posts);
        }

        [RequiresRole(UserRole.User | UserRole.Admin)]
        public virtual ActionResult Edit(string postSlug)
        {
            Post p = ContentService.GetPostWithSlug(postSlug);
            if (p.Tags == null) p.Tags = new List<Tag>();
            return CreateEditView(p);
        }

        private ActionResult CreateEditView(Post p)
        {
            return View("Create", p);
        }

        public virtual JsonResult CheckPostTitleExists(string title)
        {
            if (ContentService.PostTitleExists(title))
            {
                return new JsonResult{Data = true};
            }
            return new JsonResult {Data = false};
        }




    }
}
