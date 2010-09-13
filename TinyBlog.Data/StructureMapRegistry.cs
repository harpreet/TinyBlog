using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using StructureMap.Configuration.DSL;

namespace TinyBlog.Data
{
    public class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            For<ISession>().HttpContextScoped().Use(context => context.GetInstance<ISessionBuilder>().OpenSession());
        }

    }
}
