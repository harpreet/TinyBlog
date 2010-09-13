using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;
using TinyBlog.Interface;

namespace TinyBlog
{
    public class CheckSetupAttribute : ActionFilterAttribute
    {
        private static bool _appIsSetup;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (_appIsSetup) return;

            var membershipService = ObjectFactory.GetInstance<IMembershipService>();
            if (membershipService.ValidUserExists())
            {
                _appIsSetup = true;
                return;
            }

            var redirectTargetDictionary = new RouteValueDictionary {{"action", "Setup"}, {"controller", "Setup"}};

            filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);

        }
    }
}