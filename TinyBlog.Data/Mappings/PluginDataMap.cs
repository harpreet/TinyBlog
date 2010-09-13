using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace TinyBlog.Data.Mappings
{
    public sealed class PluginDataMap : ClassMap<PluginData>
    {
        public PluginDataMap()
        {
            Id(x => x.PluginId).GeneratedBy.Assigned();
            Map(x => x.Binary).Length(20000);

        }
    }
}
