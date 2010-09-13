using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TinyBlog.Objects;
using TinyBlog.Interface;
using TinyBlog.Models;

namespace TinyBlog.Controllers
{
    public partial class SetupController : Controller
    {
        private readonly IMembershipService _membershipService;
        private readonly ISiteSettingsService _siteSettingsService;

        public SetupController(IMembershipService membershipService, ISiteSettingsService siteSettingsService)
        {
            _membershipService = membershipService;
            _siteSettingsService = siteSettingsService;
        }

        public virtual ActionResult Setup()
        {
            if (_membershipService.ValidUserExists()) 
                return RedirectToRoute(RouteNames.Default);

            return View(new SetupViewModel());
        }

        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult Save(SetupViewModel setup)
        {
            if (_membershipService.ValidUserExists()) return RedirectToRoute(RouteNames.Posts);

            if (setup.Password != setup.ConfirmPassword)
            {
                ModelState.AddModelError("PasswordSecondTime", "Passwords did not match");

            }

            if (!ModelState.IsValid) return View("Setup", setup);

            _membershipService.CreateUser(new User
                                              {
                                                  EmailAddress = setup.EmailAddress,
                                                  UserId = setup.Username,
                                                  FullName = setup.FullName,
                                                  Password = setup.Password,
                                                  Role = UserRole.Admin
                                              });

            _siteSettingsService.SaveSiteSettings(new Site
                                                      {
                                                          Name = setup.SiteTitle,
                                                          PageFooterScript = setup.PageFooterScript,
                                                          PostFooterScript = setup.PostFooterScript,
                                                          AlternateFeedUrl = setup.AlternateFeedUrl,
                                                      });



            return RedirectToRoute(RouteNames.Posts);
        }

    }


}
