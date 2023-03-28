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
    public class TaiKhoanSinhVienController : Controller
    {
        [AdminAuthorize(ChucNang = "TaiKhoanSinhVien_Xem")]
        public ActionResult DanhSach(string sinhVien)
        {
            ViewBag.sinhVien = sinhVien;
            var map = new mapSinhVien();
            return View(map.DanhSach(sinhVien));
        }
        [AdminAuthorize(ChucNang = "TaiKhoanSinhVien_Xem")]
        public ActionResult ChiTiet(int id)
        {
            var map = new mapSinhVien();
            return View(map.ChiTiet(id));
        }

        [AdminAuthorize(ChucNang = "TaiKhoanSinhVien_Sua")]
        public ActionResult Khoa(int id)
        {
            var map = new mapSinhVien();
            map.Khoa(id);
            return RedirectToAction("DanhSach");
        }
        [AdminAuthorize(ChucNang = "TaiKhoanSinhVien_Sua")]
        public ActionResult ResetMatKhau(int id)
        {
            var map = new mapSinhVien();
            map.ResetMatKhau(id);
            return RedirectToAction("ChiTiet", new { id = id });
        }
    }
}