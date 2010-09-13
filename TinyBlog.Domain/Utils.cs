using System;
using System.Text;

namespace TinyBlog.Objects
{
    public class Utils
    {
        public static string RandomString(int length)
        {
            var s = new StringBuilder(length);
            var random = new Random();
            char ch;
            for (int i = 0; i < length; i++)
            {
                ch = Convert.ToChar(random.Next(26) + 65);
                s.Append(ch);
            }

            return s.ToString();
        }
    }
}
