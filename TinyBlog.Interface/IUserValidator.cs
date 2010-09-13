using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBlog.Interface
{
    public interface IUserValidator
    {
        bool ValidateUser(string username, string password);
    }
}
