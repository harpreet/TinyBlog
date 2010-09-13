using System.Data;
using FluentNHibernate.Cfg;
namespace TinyBlog.Data
{
    public interface ISessionBuilder
    {
        NHibernate.ISession OpenSession();
        FluentConfiguration GetDb();
        IDbConnection GetConnection();
    }
}
