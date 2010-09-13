namespace TinyBlog.Objects
{
    public static class StringExtensions
    {
        public static bool IsEmptyOrNull(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
    }
}
