using System.Web.Security;
using TinyBlog.Objects;

namespace TinyBlog.Interface
{
    public interface IMembershipService
    {
        int MinPasswordLength { get; }
        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string fullName, string password, string email, UserRole role);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        bool IsUserInRole(string username, string rolename);
        UserRole? GetUserRole(string username);
        User GetUser(string username);
        bool ValidUserExists();
        void CreateUser(User user);
    }
}