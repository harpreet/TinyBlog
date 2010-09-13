using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyBlog.Objects
{
    public class User : EntityBase
    {
        public virtual string UserId { get; set; }
        public virtual string FullName { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string Password { get; set; }
        public virtual bool PasswordIsHashed { get; set; }
        public virtual UserRole Role { get; set; }
    }

    [Flags]
    public enum UserRole
    {
        User = 1,
        Admin = 2,
    }
}
