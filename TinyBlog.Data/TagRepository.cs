using System.Collections.Generic;
using System.Linq;
using NHibernate;
using TinyBlog.Objects;
using TinyBlog.Interface;

namespace TinyBlog.Data
{
    public class TagRepository : RepositoryBase, ITagRepository
    {
        public TagRepository(IQueryExecutor queryExecutor) : base(queryExecutor) { }


        public IEnumerable<Tag> GetAllTags()
        {

            Query<IEnumerable<Tag>> query = session =>
            {
                ICriteria crit = session.CreateCriteria(typeof(Tag));
                return crit.List<Tag>();
            };

            return QueryExecutor.ExecuteQuery(query);
        }

        public IEnumerable<Tag> SearchForTags(IEnumerable<Tag> tagsToFind)
        {
            if (!tagsToFind.HasData()) return new List<Tag>();

            object[] tagNames = (from t in tagsToFind where t != null && t.Name != null select t.Name).Cast<object>().ToArray();

            Query<IEnumerable<Tag>> query = session =>
                                                {
                                                    IQuery q = session.CreateQuery("select t from Tag t where t.Name in (:tags)")
                                                                                    .SetParameterList("tags", tagNames);

                                                    return q.List<Tag>();
                                                };

            return QueryExecutor.ExecuteQuery(query);
        }

    }
}
