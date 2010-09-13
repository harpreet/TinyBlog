using System.Web.Mvc;
using TinyBlog.Interface;

namespace TinyBlog.Controllers
{
    public class UploadController : Controller
    {
        private IContentService _contentService;
        public UploadController(IContentService contentService)
        {
            _contentService = contentService;
        }

        private const int BYTE_SIZE_LIMIT = 10000000;

        [AcceptVerbs(HttpVerbs.Post)]
        public string up(object o)
        {
            string callback = Request.QueryString["CKEditorFuncNum"];
            if (Request.Files != null && Request.Files[0] != null)
            {

                if (Request.Files[0].ContentLength > BYTE_SIZE_LIMIT)
                {
                    return "error";
                }
                else
                {
                    byte[] buffer = new byte[Request.Files[0].ContentLength];

                    Request.Files[0].InputStream.Read(buffer, 0, Request.Files[0].ContentLength);

                    string url = _contentService.SaveFileReturnUrl(HttpContext, buffer, Request.Files[0].FileName);

                    string result = "<script type=\"text/javascript\">";
                    result += "window.parent.CKEDITOR.tools.callFunction(" + Request.QueryString["CKEditorFuncNum"] + ", \"" +
                              url + "\",\"\");</script>";
                    return result;

                }
            }

                return "error" ;
        }

    }
}
