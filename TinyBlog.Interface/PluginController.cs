using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace TinyBlog.Core
{
    public abstract class PluginController : Controller
    {

        public abstract ActionResult Index();
        public abstract string PluginId { get; }
    }
}
