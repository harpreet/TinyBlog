using TinyBlog.Interface;

namespace TinyBlog.Core
{
    // This class is not necessary but it works easier with structuremap's default conventions.  
    // Can be removed by changing that and placing the interface IUserValidator on MembershipService directly.
    // Deferred
    public class UserValidator : IUserValidator
    {
        private readonly IMembershipService _membershipService;

        public UserValidator(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        public bool ValidateUser(string username, string password)
        {
            return _membershipService.ValidateUser(username, password);
        }
    }
}
