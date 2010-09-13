using TinyBlog.Interface;

namespace TinyBlog.Tests
{
    public class TestUserValidator : IUserValidator
    {
        public bool ValidateUser(string username, string password)
        {
            return true;
        }
    }
}
