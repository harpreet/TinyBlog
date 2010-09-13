using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyBlog.Objects;

namespace TinyBlog.Interface
{
    public interface IUserRepository
    {
        User GetUser(string userName);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        bool CreateOrUpdateUser(User user);
        UserRole? GetRole(string username);
        bool AtLeastOneUserExists();
    }
}
