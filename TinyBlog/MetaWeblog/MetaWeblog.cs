using System;
using System.Linq;
using System.Web;
using CookComputing.XmlRpc;
using System.Collections.Generic;
using StructureMap;
using TinyBlog.Objects;
using TinyBlog.Interface;

/*
 * Comes from Keyvan Nayyeri (nayyeri.net blog)
 */
namespace TinyBlog.MetaWeblog
{
    public class MetaWeblog : XmlRpcService, IMetaWeblog
    {
        
        public MetaWeblog (): this(ObjectFactory.GetInstance<IContentService>(), ObjectFactory.GetInstance<IUserValidator>())
        {
            
        }

        public MetaWeblog(IContentService contentService, IUserValidator userValidator)
        {
            ContentService = contentService;
            UserValidator = userValidator;
        }

        public IUserValidator UserValidator { get; set;}

        public IContentService ContentService { get; set; }

        #region IMetaWeblog Members

        string IMetaWeblog.AddPost(string blogid, string username, string password,
            Post post, bool publish)
        {
            ValidateUser(username, password);
            Objects.Post newPost = ConvertMetaweblogPostToDomainPost(post);
            ContentService.SavePost(newPost);
            return newPost.Id.ToString();
        }



        bool IMetaWeblog.UpdatePost(string postid, string username, string password,
            Post post, bool publish)
        {
            ValidateUser(username, password);
            ContentService.UpdatePost(Convert.ToInt32(postid), ConvertMetaweblogPostToDomainPost(post));
            return true;
        }

        Post IMetaWeblog.GetPost(string postid, string username, string password)
        {
            ValidateUser(username, password);
            var p = this.ContentService.GetPost(Convert.ToInt32(postid));
            Post post = ConvertDomainPostToMetaweblogPost(p);
            return post;
        }

        private Post ConvertDomainPostToMetaweblogPost(Objects.Post post)
        {
            if (post == null) return default(Post);

            var newMetaWeblogPost = new Post
                                        {
                                            wp_slug = post.Slug,
                                            title = post.Title,
                                            description = post.Body,
                                            categories = (from t in post.Tags select t.Name).ToArray(),
                                            dateCreated = post.Published,
                                            postid = post.Id,
                                        };

            return newMetaWeblogPost;
        }

        private static Objects.Post ConvertMetaweblogPostToDomainPost(Post post)
        {
            var newPost = new Objects.Post
            {
                Slug = post.wp_slug,
                Title = post.title,
                Body = post.description,
                Tags = (from c in post.categories
                        select new Tag { Name = c }).ToList()
            };

            return newPost;
        }

        CategoryInfo[] IMetaWeblog.GetCategories(string blogid, string username, string password)
        {
            ValidateUser(username, password);

            IEnumerable<Tag> tags = ContentService.GetAllTags();

            if (tags != null && tags.Count() != 0)
            {
                return (from t in tags
                        select new CategoryInfo
                                   {
                                       categoryid = t.Id.ToString(),
                                       title = t.Name,
                                       description = t.Name,
                                       htmlUrl = string.Empty,
                                       rssUrl = string.Empty
                                   }).ToArray();
            }

            return new CategoryInfo[0];
        }

        Post[] IMetaWeblog.GetRecentPosts(string blogid, string username, string password, int numberOfPosts)
        {
            ValidateUser(username, password);

            var posts = ContentService.GetRecentPublishedPosts(numberOfPosts);

            if (posts == null) return new List<Post>().ToArray();

            var metaWebLogPosts = from p in posts
                                  select ConvertDomainPostToMetaweblogPost(p);

            return metaWebLogPosts.ToArray();

        }

        MediaObjectInfo IMetaWeblog.NewMediaObject(string blogid, string username, string password, MediaObject mediaObject)
        {
            ValidateUser(username, password);
            var objectInfo = new MediaObjectInfo();

            var w = new HttpContextWrapper(Context);
            ContentService.SaveFileReturnUrl(w, new byte[0], "");

            return objectInfo;
        }

        bool IMetaWeblog.DeletePost(string key, string postid, string username, string password, bool publish)
        {
            ValidateUser(username, password);

            bool result = ContentService.DeletePost(Convert.ToInt32(postid));

            return result;
        }

        BlogInfo[] IMetaWeblog.GetUsersBlogs(string key, string username, string password)
        {
            ValidateUser(username, password);
            var infoList = new List<BlogInfo>();

            // TODO: Implement your own logic to get blog info objects and set the infoList

            return infoList.ToArray();
        }

        UserInfo IMetaWeblog.GetUserInfo(string key, string username, string password)
        {
            ValidateUser(username, password);
            var info = new UserInfo();

            // TODO: Implement your own logic to get user info objects and set the info

            return info;
        }

        #endregion

        #region Private Methods

        protected virtual void ValidateUser(string username, string password)
        {
            if (!UserValidator.ValidateUser(username, password))
            {
                throw new XmlRpcFaultException(0, "User is not valid!");
            }
        }

        #endregion
    }
}
