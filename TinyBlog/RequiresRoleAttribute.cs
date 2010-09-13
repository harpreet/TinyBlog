using System.Security.Principal;
using System.Web.Mvc;
using StructureMap;
using TinyBlog.Interface;
using TinyBlog.Objects;

namespace TinyBlog
{
    public class RequiresRoleAttribute : AuthorizeAttribute
    {
        private readonly UserRole _requiredRoles;

        public RequiresRoleAttribute(UserRole requiredRoles)
        {
            _requiredRoles = requiredRoles;
        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            return httpContext.User.Identity.IsAuthenticated && httpContext.User.HasRole(_requiredRoles);
        }
    }

    public static class IPrincipalExtensions
    {
        public static bool HasRole(this IPrincipal user, UserRole neededRole)
        {
            if (user == null || user.Identity == null || user.Identity.Name == null) return false;
            
            var membershipService = ObjectFactory.GetInstance<IMembershipService>();
            var role = membershipService.GetUserRole(user.Identity.Name);

            if (role == null) return false;

            if ((int)(role.Value & neededRole) > 0) return true;

            return false;

        }
    }
}
