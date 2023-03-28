using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV.Models;
using SV.App_Start;
using SV.Models.Map;

namespace SV.Controllers
{
    public class CommentController : Controller
    {
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult DangCMT(Comment model)
        { 
            var user = SV.App_Start.SessionConfig.GetTaiKhoan();
            if (user == null)
            {
                return Json(new
                {
                    result = false,
                    status = "Chưa đăng nhập"
                });
            }
            if (model.NoiDung == null)
            {
                return Json(new
                {
                    result = false,
                    status = "Chưa nhập nội dung"
                });
            }
            var map = new Models.Map.mapComment();
            var tk = map.ThemMoi(model);
            return Json(new
            {
                result = tk != null ? true : false,
                status = map.message
            });  
        }
    }
}