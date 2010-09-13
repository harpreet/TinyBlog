using System.Web.Security;
using TinyBlog.Interface;

namespace TinyBlog.Core
{
    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            ValidationUtil.ValidateRequiredStringValue(userName, "userName");
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}