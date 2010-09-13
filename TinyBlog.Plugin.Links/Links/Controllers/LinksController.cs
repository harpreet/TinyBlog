using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TinyBlog.Core;
using TinyBlog.Interface;

namespace Links.Links.Controllers
{
    public class LinksController : PluginController
    {
        private readonly IPluginContentService _pluginContentService;

        public LinksController(IPluginContentService pluginContentService)
        {
            _pluginContentService = pluginContentService;
        }

        public override string PluginId
        { 
            get
            {
                return "D4540116-890B-400B-9EE1-F187F6087BA8"; 
            }
        }


        public override ActionResult Index()
        {
            var data = GetData();
            var v = View(data);
            return v;
        }


        public List<Link> GetData()
        {
            return _pluginContentService.GetPluginData<List<Link>>(PluginId);
        }

        public ActionResult Edit()
        {
            return View(GetData());
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (Request.IsAjaxRequest())
            {
                //Response.StatusCode = 500;
                return Json(new
                                {
                                    Success = true,
                                });
            }

            else return Edit();
        }

        public ActionResult Save(List<Link> links)
        {
            _pluginContentService.SavePluginData(PluginId, links);
            return View("Index", GetData());
        }


    }








}
