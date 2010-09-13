using System.Collections.Generic;
using NHibernate;
using TinyBlog.Objects;
using TinyBlog.Interface;

namespace TinyBlog.Data
{
    public class SiteSettingsRepository : RepositoryBase, ISiteSettingsRepository
    {
        public SiteSettingsRepository(IQueryExecutor queryExecutor) : base(queryExecutor) { }

        public void SaveSettings(Site site)
        {
            CUDQuery query = session =>
                                 {
                                     session.SaveOrUpdate(site);
                                 };

            QueryExecutor.UpdateDelete(query);
        }

        public Site GetSiteSettings()
        {
            Query<Site> query = session =>
                                    {
                                        ICriteria crit = session.CreateCriteria(typeof (Site)).SetMaxResults(1);

                                        if (crit != null && crit.List<Site>().HasData())
                                        {
                                            return crit.List<Site>()[0];
                                        }
                                        return new Site();
                                    };

            return QueryExecutor.ExecuteQuery(query);
        }
    }
}
