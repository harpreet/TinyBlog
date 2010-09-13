using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cache;
using NHibernate.Impl;
using NHibernate.Tool.hbm2ddl;
using TinyBlog.Data.Mappings;
using Configuration = NHibernate.Cfg.Configuration;

namespace TinyBlog.Data
{
    public class SessionBuilder : ISessionBuilder
    {
        private static ISessionFactory _sessionFactory;
        private static readonly object _sessionFactoryLockObj = new object();

        protected static Configuration _config;

        //TODO:  There must be a better way to do this.  Look it up / Fix
        private readonly Dictionary<string, IPersistenceConfigurer> _configs =
            new Dictionary<string, IPersistenceConfigurer>
                {
                    { "mssql2008", MsSqlConfiguration.MsSql2008.ShowSql().ConnectionString(ConnectionString).Cache(c => c.ProviderClass(typeof (HashtableCacheProvider).AssemblyQualifiedName).UseQueryCache())},
                    { "mysql", MySQLConfiguration.Standard.ShowSql().ConnectionString(ConnectionString).ShowSql().Cache(c => c.ProviderClass(typeof (HashtableCacheProvider).AssemblyQualifiedName).UseQueryCache())},
                    { "sqlite", SQLiteConfiguration.Standard.ShowSql().ConnectionString(ConnectionString)},
                };

        public SessionBuilder()
        {
            Console.SetOut(new ToDebugWriter());
            GetFactory();
        }


        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["Db"].ToString(); }
        }

        #region ISessionBuilder Members

        public virtual ISession OpenSession()
        {
            return GetFactory().OpenSession();
        }

        public virtual IDbConnection GetConnection()
        {
            return ((SessionFactoryImpl) _sessionFactory).ConnectionProvider.GetConnection();
        }

        public virtual FluentConfiguration GetDb()
        {
            return Fluently.Configure().Database(_configs[DbTypeFromConfig()]);
        }

        public string DbTypeFromConfig()
        {
            return ConfigurationManager.AppSettings["DBType"].ToLower();
        }

        #endregion

        public Configuration CreateConfig()
        {
            FluentConfiguration db = GetDb();
            FluentConfiguration mappings = db.Mappings(m => m.FluentMappings.AddFromAssemblyOf<PostMap>());
            return mappings.BuildConfiguration();
        }

        protected ISessionFactory GetFactory()
        {
            if (_sessionFactory == null)
            {
                lock (_sessionFactoryLockObj)
                {
                    if (_sessionFactory == null)
                    {
                        _config = CreateConfig();
                        UpdateSchema(_config);
                        _sessionFactory = _config.BuildSessionFactory();
                    }
                }
            }
            return _sessionFactory;
        }

        protected virtual void UpdateSchema(Configuration cfg)
        {
            new SchemaUpdate(cfg).Execute(true, true);
        }

        //for testing purposes only
        public void Reset()
        {
            _sessionFactory = null;
        }
    }


    public class ToDebugWriter : StringWriter
    {
        public override void WriteLine(string value)
        {
            Debug.WriteLine(value);
            base.WriteLine(value);
        }
    }
}