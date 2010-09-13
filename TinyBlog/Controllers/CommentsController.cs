using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Microsoft.Security.Application;
using MvcContrib.Filters;
using TinyBlog.Objects;
using TinyBlog.Interface;

namespace TinyBlog.Controllers
{
    [PassParametersDuringRedirect]
    public class CommentsController : ContentControllerBase
    {

        public CommentsController(IContentService contentService) : base(contentService){}

        public virtual ActionResult Index(int postId)
        {
            IEnumerable<Comment> comments = ContentService.GetApprovedCommentsForPost(postId);
            return View(comments);
        }

        public virtual ActionResult Create(int postId)
        {
            return View(new CreateCommentModel { Comment = new Comment(), ParentPostId = postId });
        } 

        [CaptchaValidator]
        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult Create(CreateCommentModel comment, bool captchaValid)
        {
            if (!captchaValid)
            {
                ModelState.AddModelError("_FORM", "You did not type the verification word correctly. Please try again.");
                if (Request.IsAjaxRequest())
                {
                    Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
                    return new EmptyResult();
                }
                return View("Create", comment);
            }

            comment.Comment.Body = AntiXss.HtmlEncode(comment.Comment.Body);
            comment.Comment.AuthorName = AntiXss.GetSafeHtmlFragment(comment.Comment.AuthorName);
            comment.Comment.AuthorUrl = AntiXss.GetSafeHtmlFragment(comment.Comment.AuthorUrl);
            comment.Comment.AuthorEmail = AntiXss.GetSafeHtmlFragment(comment.Comment.AuthorEmail);

            comment.Comment.ParentPost = new Post {Id = comment.ParentPostId};
            
            var status = ContentService.SaveComment(comment.Comment);
            if (status is Failure)
            {
                Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                if (Request.IsAjaxRequest())
                {
                    return Json(new { responseXML = "Error" });
                }

                return View("Comment", comment.Comment);
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("Comment", comment.Comment);
                }

                var post = ContentService.GetPost(comment.ParentPostId);
                return RedirectToRoute(RouteNames.Post, new { postSlug = post.Slug });
            }



        }

    }

    public class CreateCommentModel
    {
        public Comment Comment { get; set; }
        public int ParentPostId { get; set; }
    }
}
