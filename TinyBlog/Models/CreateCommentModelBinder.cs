using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TinyBlog.Controllers;
using TinyBlog.Objects;

namespace TinyBlog.Models
{
    public class CreateCommentModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var form = controllerContext.HttpContext.Request.Form;

            var result = new CreateCommentModel{Comment = new Comment(), ParentPostId = Convert.ToInt32(form["ParentPostId"])};

            result.Comment.AuthorName = form["Comment.AuthorName"];
            result.Comment.AuthorEmail = form["Comment.AuthorEmail"];
            result.Comment.AuthorUrl = form["Comment.AuthorUrl"];
            result.Comment.Body = form["Comment"];

            return result;
        }
    }
}