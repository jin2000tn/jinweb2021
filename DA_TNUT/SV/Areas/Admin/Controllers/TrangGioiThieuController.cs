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
    public class TrangGioiThieuController : Controller
    {
        [AdminAuthorize(ChucNang = "TrangGioiThieu_Xem")]
        public ActionResult DanhSach()
        {
            var map = new mapTrangGioiThieu();
            return View(map.DanhSach());
        }
        [AdminAuthorize(ChucNang = "TrangGioiThieu_Them")]
        public ActionResult ThemMoi()
        {
            return View(new TrangGioiThieu());
        }
        [HttpPost]
        [ValidateInput(false)]
        [AdminAuthorize(ChucNang = "TrangGioiThieu_Them")]
        public ActionResult ThemMoi(TrangGioiThieu model)
        {
            var map = new mapTrangGioiThieu();
            if(map.ThemMoi(model) > 0)
            {
                return RedirectToAction("DanhSach");
            }
            else
            {
                ModelState.AddModelError("", map.message);
                return View(model);
            }
        }
        [AdminAuthorize(ChucNang = "TrangGioiThieu_Sua")]
        public ActionResult CapNhat(int id)
        {
            var map = new mapTrangGioiThieu();
            return View(map.ChiTiet(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        [AdminAuthorize(ChucNang = "TrangGioiThieu_Sua")]
        public ActionResult CapNhat(TrangGioiThieu model)
        {
            var map = new mapTrangGioiThieu();
            if (map.CapNhat(model) > 0)
            {
                return RedirectToAction("DanhSach");
            }
            else
            {
                ModelState.AddModelError("", map.message);
                return View(model);
            }
        }
        [AdminAuthorize(ChucNang = "TrangGioiThieu_Xoa")]
        public ActionResult Xoa(int id)
        {
            var map = new mapTrangGioiThieu();
            map.Xoa(id);
            return RedirectToAction("DanhSach");
        }
    }
}