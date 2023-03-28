using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV.Models;
using SV.Models.Map;

namespace SV.Controllers
{
    public class TuyenSinhController : Controller
    {
        public ActionResult Index(int? page)
        {
            var map = new mapBaiVietTuyenSinh();
            ViewBag.page = page ?? 1;
            return View(map.DanhSach(page??1));
        }

        public ActionResult ChiTiet(string link)
        {
            var map = new mapBaiVietTuyenSinh();
            return View(map.ChiTiet(link));
        }

        [HttpPost]
        public ActionResult DangKy(LienHeTuyenSinh model)
        {
            var map = new mapLienHeTuyenSinh();
            var baiViet = new mapBaiVietTuyenSinh().ChiTiet(model.idBaiVietTuyenSinh);
            model.ID = map.ThemMoi(model);
            if (model.ID > 0)
            { 
                return View(model);
            }
            else
            {
                TempData["error"]=map.message;
                return Redirect("/tuyen-sinh/"+ baiViet.LinkSeo);
            }
        } 
    }
}