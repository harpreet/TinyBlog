using NHibernate;

namespace TinyBlog.Data
{
    public delegate T Query<out T>(ISession s);

    public delegate void CUDQuery(ISession s);
}