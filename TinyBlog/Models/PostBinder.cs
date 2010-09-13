using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TinyBlog.Objects;

namespace TinyBlog.Models
{
    public class PostBinder : DefaultModelBinder
    {

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var form = controllerContext.HttpContext.Request.Form;

            var post = new Post();

            post.Title = form["Title"];
            post.Body = form["Body"];
            post.IsPage = Convert.ToBoolean(form.GetValues("IsPage")[0]);
            post.Published = Convert.ToDateTime(form["Published"]);
            if (form["Id"] != null && !form["Id"].StartsWith("0000000"))
            {
                post.Id = Convert.ToInt32(form["Id"]);
            }

            if (form["Tags"] != null)
            {
                post.Tags = new List<Tag>();
                string []tags = form["Tags"].Split(new char[] {',', ' '}, StringSplitOptions.RemoveEmptyEntries);
                if (tags.HasData())
                {
                    foreach (var t in tags)
                        post.Tags.Add(new Tag{Name = t});
                }
            }

            return post;

        }
    }
}