using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBlog.Interface
{
    public interface IPluginContentService
    {
        T GetPluginData<T>(string key);
        void SavePluginData(string key, object objectToSave);
    }

}
