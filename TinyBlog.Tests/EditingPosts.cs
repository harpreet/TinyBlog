using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using StructureMap;
using TinyBlog.Objects;
using TinyBlog.Interface;
using TinyBlog.MetaWeblog;
using Post = TinyBlog.Objects.Post;

namespace TinyBlog.Tests
{
    [TestFixture]
    public class EditingPosts : IntegrationTestBase
    {
        [Test]
        public void EditPost_ShouldHaveNewDetails()
        {
            var p = new Post();
            string postTitle = "Some Title To Be Edited";
            p.Slug = postTitle;
            p.Title = postTitle;
            p.Body = "Post that will be edited";
            p.Tags = new List<Tag> { new Tag { Name = "A" }, new Tag { Name = "B" } };
            p.Published = new DateTime(2005, 12, 12);

            var contentService = ObjectFactory.GetInstance<IContentService>();
            contentService.SavePost(p);

            Post retrievedPost = contentService.GetPostWithSlug(postTitle);

            int postid = retrievedPost.Id;

            var updatedPost = new Post();
            string updatedTitle = "UpdatedTitle";
            updatedPost.Slug = updatedTitle;
            updatedPost.Title = updatedTitle;
            updatedPost.Body = "Updated body";
            updatedPost.Tags = new List<Tag> { new Tag { Name = "updated tag" } };
            updatedPost.Published = new DateTime(2006, 1, 1);

            contentService.UpdatePost(postid, updatedPost);

            Post retrievedUpdatedPost = contentService.GetPostWithSlug(updatedTitle);
            Post originalPost = contentService.GetPostWithSlug(postTitle);

            Assert.IsNull(originalPost);
            Assert.IsNotNull(retrievedUpdatedPost);

        }
    }
}
