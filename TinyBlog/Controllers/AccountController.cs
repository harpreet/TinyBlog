using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using TinyBlog.Models;
using TinyBlog.Interface;

namespace TinyBlog.Controllers
{

    [HandleError]
    public class AccountController : Controller
    {
        public AccountController(): this(null, null) { }

        public AccountController(IFormsAuthenticationService formsService, IMembershipService membershipService)
        {
            FormsService = formsService;
            MembershipService = membershipService;
        }

        public IFormsAuthenticationService FormsService { get; private set; }

        public IMembershipService MembershipService { get; private set; }


        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewData["PasswordLength"] = MembershipService.MinPasswordLength;
            base.OnActionExecuting(filterContext);
        }


        public virtual ActionResult LogOff()
        {
            FormsService.SignOut();
            return RedirectToRoute(RouteNames.Posts);
        }

        public virtual ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings",
            Justification = "Needs to take same parameter type as Controller.Redirect()")]
        public virtual ActionResult LogOn(LogOnModel model, bool rememberMe, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    FormsService.SignIn(model.UserName, rememberMe);
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToRoute(RouteNames.Posts);
                }
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

    }

}
