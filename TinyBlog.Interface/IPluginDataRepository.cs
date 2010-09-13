using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBlog.Interface
{
    public interface IPluginDataRepository
    {
        void SaveData(string key, byte[] binarySerializedObject);
        void DeleteData(string key);
        byte[] GetData(string key);
    }
}
