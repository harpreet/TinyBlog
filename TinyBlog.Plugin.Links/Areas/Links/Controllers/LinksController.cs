using System.Collections.Generic;
using System.Web.Mvc;
using TinyBlog.Core;
using TinyBlog.Interface;

namespace Links.Areas.Links.Controllers
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
            return View(data);
        }


        public List<Link> GetData()
        {
            var data = _pluginContentService.GetPluginData<List<Link>>(PluginId);
            if (data == null)
            {
                data = new List<Link>();
            }
            return data;
        }

        public void SaveData(List<Link> data)
        {
            _pluginContentService.SavePluginData(PluginId, data);
        }

        public ActionResult Edit()
        {
            var data = GetData();
            if (data == null || data.Count == 0)
                data = new List<Link> {new Link()};

            return View("Edit", data);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (Request.IsAjaxRequest())
            {
                var data = GetData();
                data.Remove(data.Find(link => link.Id == id));
                SaveData(data);

                return Json(new

                                {
                                    Success = true,
                                });
            }

            else return Edit();
        }

        public ActionResult Save(Link links)
        {
            var data = GetData();
            data.Add(links);
            _pluginContentService.SavePluginData(PluginId, data);
            return RedirectToAction("Edit");
        }


    }








}
