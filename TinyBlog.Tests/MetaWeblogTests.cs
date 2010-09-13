using System;
using System.Collections.Generic;
using NUnit.Framework;
using StructureMap;
using TinyBlog.MetaWeblog;

namespace TinyBlog.Tests
{
    [TestFixture]
    public class MetaWeblogTests : IntegrationTestBase
    {
        public IMetaWeblog Metaweblog
        {
            get { return ObjectFactory.GetInstance<IMetaWeblog>(); }
        }

        [Test]
        public void AddPostToPublish()
        {
            Post p = GetTestPost();

            string postid = Metaweblog.AddPost(string.Empty, "some", "pass", p, true);
            Post post = Metaweblog.GetPost(postid, string.Empty, string.Empty);
            Assert.IsTrue(post.title == p.title);
        }

        private Post GetTestPost()
        {
            return new Post
                       {
                           title = "Title",
                           wp_slug = "title_slug",
                           categories = new[] { "a", "b" },
                           description = "description",
                           permalink = "permalink"
                       };
        }

        [Test]
        public void AfterAPostIsAdded_ItsTagsShouldBeRetrievable()
        {
            var post = GetTestPost();

            Metaweblog.AddPost(string.Empty, string.Empty, string.Empty, post, true);

            var tags = new List<CategoryInfo>(Metaweblog.GetCategories(string.Empty, string.Empty, string.Empty));


            foreach (string c in post.categories)
            {
                Assert.IsTrue(tags.Exists(t => t.title == c));
            }
        }


        [Test]
        public void UpdatePost_ShouldHaveNewDetails()
        {
            var p = new Post();
            const string postTitle = "Some Title To Be Edited";
            p.title = postTitle;
            p.wp_slug = postTitle;
            p.description = "Post that will be edited";
            p.categories = new[] { "A", "B" };
            p.dateCreated = new DateTime(2005, 12, 12);

            string postid = Metaweblog.AddPost(string.Empty, "some", "pass", p, true);

            var updatedPost = new Post();
            const string updatedTitle = "UpdatedTitle";
            const string updatedBody = "Updated body";
            updatedPost.wp_slug = updatedTitle;
            updatedPost.title = updatedTitle;

            updatedPost.description = updatedBody;
            updatedPost.categories = new[] { "updated tag" };
            updatedPost.dateCreated = new DateTime(2006, 1, 1);

            Metaweblog.UpdatePost(postid, string.Empty, string.Empty, updatedPost, true);

            Post retrievedUpdatedPost = Metaweblog.GetPost(postid, string.Empty, string.Empty);

            Assert.IsTrue(retrievedUpdatedPost.title == updatedTitle);
            Assert.IsTrue(retrievedUpdatedPost.categories.Length == 1);
            Assert.IsTrue(retrievedUpdatedPost.description == updatedBody);
        }


        [Test]
        public void CreateAndThenDeletePost_ShouldNotBeFound()
        {
            Post p = GetTestPost();

            int initialCount = Metaweblog.GetRecentPosts(string.Empty, string.Empty, string.Empty, 2).Length;
            Assert.IsTrue(initialCount == 0);

            string postid = Metaweblog.AddPost(string.Empty, "some", "pass", p, true);
            int count = Metaweblog.GetRecentPosts(string.Empty, string.Empty, string.Empty, 2).Length;

            Assert.IsTrue(count == 1, string.Format("Count is {0} but should be 1", count));
            bool result = Metaweblog.DeletePost(null, postid, string.Empty, string.Empty, false);
            Post deletedPost = Metaweblog.GetPost(postid, string.Empty, string.Empty);
            Assert.IsTrue(Metaweblog.GetRecentPosts(string.Empty, string.Empty, string.Empty, 2).Length == 0, "Post found but should have been deleted");
            Assert.IsTrue(result);

        }
    }
}
