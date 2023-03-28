using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV.Models.Map;
using SV.Models;
using SV.App_Start;

namespace SV.Areas.Admin.Controllers
{ 
    public class BaiDangController : Controller
    {
        [AdminAuthorize(ChucNang = "BaiDang_Xem")]
        public ActionResult DanhSach(string tensv)
        {
            var map = new mapBaiDang();
            ViewBag.tensv = tensv;
            return View(map.TraCuu(tensv));
        }
        [AdminAuthorize(ChucNang = "BaiDang_Xoa")]
        public ActionResult Xoa(int id)
        {
            var map = new mapBaiDang();
            map.Xoa(id);
            return RedirectToAction("DanhSach");
        }
    }
}