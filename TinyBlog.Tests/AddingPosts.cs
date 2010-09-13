using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using StructureMap;
using TinyBlog.Data;
using TinyBlog.Objects;
using TinyBlog.Interface;
using System.Web.Security;

namespace TinyBlog.Tests
{
    [TestFixture]
    public class AddingPosts : IntegrationTestBase
    {


        [Test]
        public void AddPostWithTags()
        {
            var p = new Post();
            string postTitle = "Some Title";
            p.Slug = postTitle;
            p.Title = postTitle;
            p.Body = "Some body";
            p.Tags = new List<Tag> {new Tag{Name = "A"}, new Tag{Name = "B"}};
            p.Published = new DateTime(2005, 12, 12);

            var contentService = ObjectFactory.GetInstance<IContentService>();
            contentService.SavePost(p);

            Post retrievedPost = contentService.GetPostWithSlug(postTitle);

            Assert.IsTrue(retrievedPost.Tags.Count == 2);

        }

        [Test]
        public void AddTwoPostsWithOverlappingTags_ShouldShowDistinctTags()
        {
            var p = new Post();
            string postTitle = "Some Title";
            p.Slug = postTitle;
            p.Title = postTitle;
            p.Body = "Some body";
            p.Tags = new List<Tag> { new Tag { Name = "A" }, new Tag { Name = "B" } };
            p.Published = new DateTime(2005, 12, 12);

            var p2 = new Post();
            string postTitle2 = "Some Title2";
            p2.Slug = postTitle2;
            p2.Title = postTitle2;
            p2.Body = "Some body2";
            p2.Tags = new List<Tag> { new Tag { Name = "B" }, new Tag { Name = "A" }, new Tag { Name = "D" }, new Tag { Name = "C" } };
            p2.Published = new DateTime(2006, 12, 12);

            IContentService contentService = ObjectFactory.GetInstance<IContentService>();
            contentService.SavePost(p2);
            contentService.SavePost(p);

            IEnumerable<Tag> tags = contentService.GetAllTags();

            Assert.IsTrue(tags.Count() == 4);
        }
    }
}
