using System.Linq;
using TinyBlog.Interface;
using NHibernate.Linq;

namespace TinyBlog.Data
{
    public class PluginDataRepository : RepositoryBase, IPluginDataRepository
    {
        public PluginDataRepository(IQueryExecutor queryExecutor) : base(queryExecutor)
        {
        }

        public void DeleteData(string key)
        {
            CUDQuery del = session => session.CreateQuery("delete PluginData p where p.PluginId = :id").SetString("id", key);
            QueryExecutor.UpdateDelete(del);
        }

        public void SaveData(string key, byte[] binarySerializedObject)
        {
            CUDQuery save;
            var existingData = GetPluginData(key);
            if (existingData != null)
            {
                existingData.Binary = binarySerializedObject;
                save = session => session.SaveOrUpdate(existingData);
            }
            else
            {
                var pluginData = new PluginData {PluginId = key, Binary = binarySerializedObject};
                save = session => session.SaveOrUpdate(pluginData);
            }
            QueryExecutor.UpdateDelete(save);
        }

        private PluginData GetPluginData(string key)
        {
            Query<PluginData> query = session =>
                                          {
                                              var result = from p in session.Linq<PluginData>()
                                                           where p.PluginId == key
                                                           select p;

                                              return result.FirstOrDefault();
                                          };

            return QueryExecutor.ExecuteQuery(query);
        }

        public byte[] GetData(string key)
        {
            var pluginData = GetPluginData(key);

            return pluginData == null ? new byte[0] : pluginData.Binary;
        }
    }
}
