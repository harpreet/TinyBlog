using System.Collections.Generic;
using TinyBlog.Objects;

namespace TinyBlog.Interface
{
    public interface ICommentRepository
    {
        
        void SaveComment(Comment comment);

        IList<Comment> GetApprovedCommentsForPost(Post post);
        IList<Comment> GetApprovedCommentsForPost(int parentPostId);
        IList<Comment> GetUnapprovedComments();
        void ApproveComment(int id);
        void DeleteComment(int id);
    }
}