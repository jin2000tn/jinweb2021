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
    public class QuanLyYeuCauController : Controller
    {
        [AdminAuthorize(ChucNang = "QuanLyYeuCau_Xem")]
        public ActionResult DanhSach(string sinhVien, bool? trangThai)
        {
            ViewBag.sinhVien = sinhVien;
            ViewBag.trangThai = trangThai;
            var map = new mapYeuCau();
            return View(map.DanhSach(sinhVien, trangThai));
        }
        [AdminAuthorize(ChucNang = "QuanLyYeuCau_Them")]
        public ActionResult ThemMoi()
        {
            return View(new YeuCau());
        }
        [HttpPost]
        [AdminAuthorize(ChucNang = "QuanLyYeuCau_Them")]
        public ActionResult ThemMoi(YeuCau model)
        {
            var map = new mapYeuCau();
            if (map.ThemMoi(model) > 0)
            {
                return RedirectToAction("DanhSach");
            }
            else
            {
                ModelState.AddModelError("", map.message);
                return View(model);
            }
        }
        [AdminAuthorize(ChucNang = "QuanLyYeuCau_Sua")]
        public ActionResult CapNhat(int id)
        {
            var map = new mapYeuCau();
            return View(map.ChiTiet(id));
        }
        [HttpPost]
        [AdminAuthorize(ChucNang = "QuanLyYeuCau_Sua")]
        public ActionResult CapNhat(YeuCau model)
        {
            var map = new mapYeuCau();
            if (map.CapNhap(model) > 0)
            {
                return RedirectToAction("DanhSach");
            }
            else
            {
                ModelState.AddModelError("", map.message);
                return View(model);
            }
        }
        [AdminAuthorize(ChucNang = "QuanLyYeuCau_Xoa")]
        public ActionResult Xoa(int id)
        {
            var map = new mapYeuCau();
            map.Xoa(id);
            return RedirectToAction("DanhSach");
        }
    }
}