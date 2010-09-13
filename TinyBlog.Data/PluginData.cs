namespace TinyBlog.Data
{
    public class PluginData
    {
        public virtual string PluginId { get; set; }
        public virtual byte[] Binary { get; set; }
    }
}