using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Web;
using TinyBlog.Objects;

namespace TinyBlog.Interface
{
    public interface IContentService
    {
        int GetPageSize();
        int GetPostCount();
        int GetPostCount(string tag);
        IList<Post> GetRecentPublishedPosts(int numberOfRecentPosts);
        IList<Post> GetPostsPage(int pageNumber);
        void SavePost(Post post);
        void UpdatePost(int postid, Post post);
        bool PostTitleExists(string title);
        IList<Comment> GetApprovedCommentsForPost(Post post);
        IList<Comment> GetApprovedCommentsForPost(int parentPostId);
        OperationStatus SaveComment(Comment comment);
        Post GetPost(int id);
        Post GetPostWithSlug(string slug);
        IList<Post> GetPages();
        Post CreateNewPost();
        IEnumerable<Tag> GetAllTags();
        SyndicationFeed GetFeed();

        bool DeletePost(int postid);
        string SaveFileReturnUrl(HttpContextBase context, byte[] file, string fileName);
        IList<Comment> GetUnapprovedComments();
        OperationStatus ApproveComment(int id);
        OperationStatus DeleteComment(int id);
        IList<Post> GetPostsWithCategory(string category, int pageNumber);
    }
}