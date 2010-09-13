using System;
using System.Security.Cryptography;
using System.Web.Security;
using TinyBlog.Objects;
using TinyBlog.Interface;

namespace TinyBlog.Core
{
    public class MembershipService : IMembershipService
    {
        private readonly IUserRepository _userRepository;

        public MembershipService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int MinPasswordLength
        {
            get
            {
                return 8;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            ValidationUtil.ValidateRequiredStringValue(userName, "userName");
            ValidationUtil.ValidateRequiredStringValue(password, "password");

            var user = _userRepository.GetUser(userName);

            if (user == null) return false;

            if (!user.PasswordIsHashed)
            {
                user.Password = HashToBase64(user.Password);
                user.PasswordIsHashed = true;
                _userRepository.CreateOrUpdateUser(user);
            }

            return user.Password == HashToBase64(password);

        }

        public void CreateUser(User user)
        {
            CreateUser(user.UserId, user.FullName, user.Password, user.EmailAddress, user.Role);
        }

        public MembershipCreateStatus CreateUser(string userName, string fullName, string password, string email, UserRole role)
        {
            ValidationUtil.ValidateRequiredStringValue(userName, "userName");
            ValidationUtil.ValidateRequiredStringValue(password, "password");
            ValidationUtil.ValidateRequiredStringValue(email, "email");


            var newUser = new User
                               {
                                   UserId = userName, 
                                   FullName = fullName, 
                                   Password = HashToBase64(password), 
                                   PasswordIsHashed = true, 
                                   Role = role,
                                   EmailAddress = email
                               };

            bool success = _userRepository.CreateOrUpdateUser(newUser);

            return success ? MembershipCreateStatus.Success : MembershipCreateStatus.UserRejected;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            ValidationUtil.ValidateRequiredStringValue(userName, "userName");
            ValidationUtil.ValidateRequiredStringValue(oldPassword, "oldPassword");
            ValidationUtil.ValidateRequiredStringValue(newPassword, "newPassword");

            return _userRepository.ChangePassword(userName, oldPassword, newPassword);
        }

        public bool IsUserInRole(string username, string rolename)
        {
            var role = GetUserRole(username);
            return role.ToString() == rolename;
        }

        public UserRole? GetUserRole(string username)
        {
            return _userRepository.GetRole(username);
        }

        public User GetUser(string username)
        {
            return _userRepository.GetUser(username);
        }

        public bool ValidUserExists()
        {
            return _userRepository.AtLeastOneUserExists();
        }


        public static string HashToBase64(string stringToHash)
        {
            if (stringToHash.IsEmptyOrNull()) return string.Empty;

            byte[] bytesToHash = System.Text.Encoding.UTF8.GetBytes(stringToHash);

            SHA256 sha256 = new SHA256Managed();

            byte[] hashedBytes = sha256.ComputeHash(bytesToHash);

            return Convert.ToBase64String(hashedBytes);
        }

    }
}