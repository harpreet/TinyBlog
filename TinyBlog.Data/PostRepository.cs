using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using TinyBlog.Interface;
using TinyBlog.Objects;

namespace TinyBlog.Data
{
    public class PostRepository : RepositoryBase, IPostRepository
    {
        public PostRepository(IQueryExecutor queryExecutor) : base(queryExecutor)
        {
        }

        #region IPostRepository Members

        public IList<Post> GetRecentPosts(int numberOfPosts, DateTime maxDateTime)
        {
            Query<IList<Post>> query = 
                session =>
                    {
                        IQueryable<Post> result = (from p in session.Linq<Post>()
                                                   where (!p.IsPage && p.Published < maxDateTime)
                                                   orderby p.Published
                                                   select p).Take(numberOfPosts);

                        return result.ToList();
                    };

            return QueryExecutor.ExecuteQuery(query);
        }

        public IList<Post> GetPostsPage(int pageNumber, int pageSize, DateTime maxDateTime)
        {
            Query<IList<Post>> query = session =>
                                           {
                                               IQueryable<Post> result = (from p in session.Linq<Post>()
                                                                          where (!p.IsPage && p.Published < maxDateTime)
                                                                          orderby p.Published
                                                                          select p).Skip((pageNumber - 1)*pageSize).Take
                                                   (pageSize);

                                               return result.ToList();
                                           };

            return QueryExecutor.ExecuteQuery(query);
        }

        public IList<Post> GetPostsWithCategory(string category, int pageNumber, int pageSize, DateTime maxDateTime)
        {
            Query<IList<Post>> query = session =>
                                           {
                                               IQuery q = session.CreateQuery("select p from Post p join p.Tags t where t.Name in (:tag) and p.IsPage = false and p.Published < :maxDateTime order by p.Published desc")
                                                                        .SetString("tag", category).SetDateTime("maxDateTime", maxDateTime);
                                                   //.SetDateTime("maxDateTime", maxDateTime);
                                               IEnumerable<Post> result =
                                                   q.List<Post>().Skip((pageNumber - 1)*pageSize).Take(pageSize);
                                               return result.ToList();
                                           };

            return QueryExecutor.ExecuteQuery(query);
        }

        public IList<Post> GetPages()
        {
            Query<IList<Post>> query = session =>
                                           {
                                               IQueryable<Post> result = from p in session.Linq<Post>()
                                                                         where p.IsPage
                                                                         select p;
                                               return result.ToList();
                                           };

            return QueryExecutor.ExecuteQuery(query);
        }

        public void SavePost(Post post)
        {
            CUDQuery query = session => session.SaveOrUpdate(post);
            QueryExecutor.UpdateDelete(query);
        }

        public Post GetPost(int id)
        {
            Query<Post> query = session =>
                                    {
                                        IQueryable<Post> result = from p in session.Linq<Post>()
                                                                  where p.Id == id
                                                                  select p;
                                        return result.FirstOrDefault();
                                    };
            return QueryExecutor.ExecuteQuery(query);
        }

        public Post GetPostWithSlug(string slug)
        {
            Query<Post> query = session =>
                                    {
                                        IQueryable<Post> result = from p in session.Linq<Post>()
                                                                  where p.Slug == slug
                                                                  select p;
                                        return result.FirstOrDefault();
                                    };
            return QueryExecutor.ExecuteQuery(query);
        }

        public bool DeletePost(int id)
        {
            Post p = GetPost(id);
            return DeletePost(p);
        }

        public int GetPostCount()
        {
            Query<int> query = session =>
                                   {
                                       int q = (from p in session.Linq<Post>() select p).Count();
                                       return q;
                                   };

            return QueryExecutor.ExecuteQuery(query);
        }

        public int GetPostCount(string tag)
        {
            Query<int> query = session =>
                                   {
                                       IQuery q = session.CreateQuery("select count(*) from Post p join p.Tags t where t.Name in (:tag)")
                                                                        .SetString("tag", tag);
                                       return Convert.ToInt32(q.List()[0]);
                                   };

            return QueryExecutor.ExecuteQuery(query);
        }

        public bool PostTitleExists(string title)
        {
            Query<bool> query = session =>
                                    {
                                        IQueryable<Post> result = from p in session.Linq<Post>()
                                                                  where p.Title == title
                                                                  select p;
                                        return result.Count() > 0;
                                    };

            return QueryExecutor.ExecuteQuery(query);
        }

        #endregion

        public bool DeletePost(Post p)
        {
            CUDQuery query = session => session.Delete(p);
            return QueryExecutor.UpdateDelete(query);
        }
    }
}