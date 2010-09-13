using System;
using System.Collections.Generic;
using TinyBlog.Objects;

namespace TinyBlog.Interface
{
    public interface IPostRepository
    {
        IList<Post> GetRecentPosts(int numberOfPosts, DateTime maxDateTime);
        IList<Post> GetPostsPage(int pageNumber, int pageSize, DateTime maxDateTime);
        IList<Post> GetPostsWithCategory(string category, int pageNumber, int pageSize, DateTime maxDateTime);
        IList<Post> GetPages();
        void SavePost(Post post);
        Post GetPost(int id);
        Post GetPostWithSlug(string slug);
        bool DeletePost(int postid);

        int GetPostCount();
        int GetPostCount(string tag);
        bool PostTitleExists(string title);
    }
}
