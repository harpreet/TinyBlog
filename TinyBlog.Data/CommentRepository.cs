using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using TinyBlog.Interface;
using TinyBlog.Objects;

namespace TinyBlog.Data
{
    public class CommentRepository : RepositoryBase, ICommentRepository
    {
        public CommentRepository(IQueryExecutor queryExecutor) : base(queryExecutor)
        {
        }

        #region ICommentRepository Members

        public IList<Comment> GetApprovedCommentsForPost(Post post)
        {
            return GetApprovedCommentsForPost(post.Id);
        }

        public void SaveComment(Comment comment)
        {
            CUDQuery query = session => session.Save(comment);

            QueryExecutor.UpdateDelete(query);
        }

        public IList<Comment> GetApprovedCommentsForPost(int parentPostId)
        {
            Query<IList<Comment>> query = session =>
                                              {
                                                  ICriteria crit = session.CreateCriteria<Comment>()
                                                      .CreateAlias("ParentPost", "parent")
                                                      .Add(Restrictions.Eq("parent.Id", parentPostId))
                                                      .Add(Restrictions.Eq("IsApproved", true));


                                                  return crit.List<Comment>();
                                              };

            return QueryExecutor.ExecuteQuery(query);
        }

        public IList<Comment> GetUnapprovedComments()
        {
            Query<IList<Comment>> query = session =>
                                              {
                                                  ICriteria crit = session.CreateCriteria<Comment>()
                                                      .Add(Restrictions.Eq("IsApproved", false));

                                                  return crit.List<Comment>();
                                              };

            return QueryExecutor.ExecuteQuery(query);
        }

        public void ApproveComment(int id)
        {
            CUDQuery query =
                session =>
                session.CreateQuery("update Comment c set c.IsApproved = True where c.Id = :id").SetInt32("id", id).
                    ExecuteUpdate();

            QueryExecutor.UpdateDelete(query);
        }

        public void DeleteComment(int id)
        {
            CUDQuery query =
                session =>
                session.CreateQuery("delete Comment c where c.Id = :id").SetInt32("id", id).ExecuteUpdate();

            QueryExecutor.UpdateDelete(query);
        }

        #endregion
    }
}