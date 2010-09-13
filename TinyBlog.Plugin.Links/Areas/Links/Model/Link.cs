using System;

namespace Links
{
    [Serializable]
    public class Link
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
    }
}
