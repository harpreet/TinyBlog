using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TinyBlog.Objects;
using TinyBlog.Interface;

namespace TinyBlog.Core
{

    public class PluginContentService : IPluginContentService
    {
        private readonly IPluginDataRepository _pluginDataRepository;

        public PluginContentService(IPluginDataRepository pluginDataRepository)
        {
            _pluginDataRepository = pluginDataRepository;
        }

        public T GetPluginData<T>(string key)
        {
            if (key.IsEmptyOrNull()) throw new Exception("No key provided");

            byte[] binary = _pluginDataRepository.GetData(key);

            return DeserializeTo<T>(binary);
        }

        public void SavePluginData(string key, object objectToSave)
        {
            //_pluginDataRepository.DeleteData(key);
            _pluginDataRepository.SaveData(key, Serialize(objectToSave));
        }


        public byte[] Serialize(object obj)
        {
            var binaryFormatter = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                binaryFormatter.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public T DeserializeTo<T>(byte[] binary)
        {
            if (!binary.HasData()) return default(T);

            var binaryFormatter = new BinaryFormatter();
            using (var ms = new MemoryStream(binary))
            {
                return (T)binaryFormatter.Deserialize(ms);
            }
        }
    }
}
