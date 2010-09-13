using System;
using System.IO;
using System.Web;
using Moq;
using NHibernate;
using NUnit.Framework;
using StructureMap;
using TinyBlog.Data;
using TinyBlog.Interface;
using System.Web.Security;

namespace TinyBlog.Tests
{
    [TestFixture]
    public abstract class IntegrationTestBase
    {
        [TestFixtureSetUp]
        public virtual void Init()
        {
            ConfigureContainer();
        }

        protected virtual void ConfigureContainer()
        {
            ObjectFactory.Initialize(s =>
                                         {
                                             s.Scan(x =>
                                                        {
                                                            x.AssembliesFromApplicationBaseDirectory(
                                                                assembly => assembly.GetName().Name.Contains("TinyBlog"));
                                                            x.WithDefaultConventions();
                                                        });
                                             s.For<IQueryExecutor>().Use<QueryExecutor>();
                                             s.For<HttpContextBase>().Use(new Mock<HttpContextBase>().Object);
                                             s.For<IUserValidator>().Use<TestUserValidator>();
                                         });
        }


        protected ISession _session;
        protected ISessionBuilder _builder;
        [SetUp]
        public virtual void SetUp()
        {
            _builder = new SessionBuilder();
            ObjectFactory.Inject<ISessionBuilder>(_builder);
            _session = _builder.OpenSession();
            ObjectFactory.Inject<ISession>(_session);

        }

        [TearDown]
        public virtual void TearDown()
        {
            try
            {
                File.Delete("TinySqlite.db");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            dynamic b = _builder;
            b.Reset();

            if (_session != null) _session.Dispose();
        }
    }
}
