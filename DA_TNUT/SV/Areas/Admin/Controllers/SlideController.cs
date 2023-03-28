using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV.App_Start;
using SV.Models;
using SV.Models.Map;

namespace SV.Areas.Admin.Controllers
{
    public class SlideController : Controller
    {
        [AdminAuthorize(ChucNang = "Slide_Xem")]
        public ActionResult DanhSach()
        {
            var map = new mapSlide();
            return View(map.DanhSach());
        }
        [AdminAuthorize(ChucNang = "Slide_Xem")]
        public ActionResult ChiTiet(int ID)
        {
            var map = new mapSlide();
            return View(map.ChiTiet(ID));
        }
        [AdminAuthorize(ChucNang = "Slide_Them")]
        public ActionResult ThemMoi()
        {
            return View(new Slide() { TuNgay = DateTime.Now, DenNgay = DateTime.Now });
        }
        [HttpPost]
        [AdminAuthorize(ChucNang = "Slide_Them")]
        public ActionResult ThemMoi(Slide model)
        {
            var map = new mapSlide();
            var id = map.ThemMoi(model);
            if (id > 0)
            {
                return RedirectToAction("ChiTiet", new { id = id });
            }
            else
            {
                ModelState.AddModelError("", map.message);
                return View(model);
            }
        }
        [AdminAuthorize(ChucNang = "Slide_Sua")]
        public ActionResult CapNhat(int id)
        {
            var map = new mapSlide();
            return View(map.ChiTiet(id));
        }
        [HttpPost]
        [AdminAuthorize(ChucNang = "Slide_Sua")]
        public ActionResult CapNhat(Slide model)
        {
            var map = new mapSlide();
            if (map.CapNhat(model) > 0)
            {
                return RedirectToAction("ChiTiet", new { id = model.ID });
            }
            else
            {
                ModelState.AddModelError("", map.message);
                return View(model);
            }
        }
        [AdminAuthorize(ChucNang = "Slide_Xoa")]
        public ActionResult Xoa(int id)
        {
            var map = new mapSlide();
            map.Xoa(id);
            return RedirectToAction("DanhSach");
        }
    }
}