using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV.App_Start;
using SV.Models.Map;
using SV.Models;

namespace SV.Areas.Admin.Controllers
{
    public class TaiKhoanNVController : Controller
    {
        public ActionResult DangNhap(string returnUrl)
        {
            var user = SessionConfig.GetTaiKhoanNV();
            if (user != null)
            {
                return Redirect("/Admin");
            }
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string Username, string Password, string returnUrl)
        {
            var map = new Models.Map.mapTaiKhoanNV();
            var tk = map.DangNhap(Username, new SV.Helper.Encrypt().CreateMD5(Password));
            if (tk != null)
            {
                SV.App_Start.SessionConfig.SetTaiKhoanNV(tk);
                if (string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect("~/admin");
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }
            else
            {
                ViewBag.error = map.message;
                return View();
            }
        }
        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("DangNhap");
        }
        [AdminAuthorize]
        public ActionResult ThongTin()
        {
            var user = SV.App_Start.SessionConfig.GetTaiKhoanNV();
            return View(new mapTaiKhoanNV().ChiTiet(user.Username));
        }
        [AdminAuthorize]
        public ActionResult DoiMatKhau()
        { 
            return View();
        }
        [HttpPost]
        public ActionResult DoiMatKhau(string newPass, string oldPass)
        {
            var user = SV.App_Start.SessionConfig.GetTaiKhoanNV();
            var map = new mapTaiKhoanNV();
            if (map.DoiMatKhau(user.ID, newPass, oldPass))
            {
                return RedirectToAction("ThongTin");
            }
            ModelState.AddModelError("",map.message);
            return View();
        }



        [AdminAuthorize(ChucNang = "TaiKhoanNV_Xem")]
        public ActionResult DanhSach()
        {
            return View(new mapTaiKhoanNV().DanhSach());
        }
        [AdminAuthorize(ChucNang = "TaiKhoanNV_Xem")]
        public ActionResult ChiTiet(int id)
        {
            return View(new mapTaiKhoanNV().ChiTiet(id));
        }

        [HttpPost]
        [AdminAuthorize(ChucNang = "TaiKhoanNV_Sua")]
        public JsonResult PhanQuyen(int idTaiKhoan, string maChucNang, bool check)
        {
            var map = new mapPhanQuyen();
            return Json(new
            {
                result = map.CapNhatQuyen(idTaiKhoan, maChucNang, check),
                status = map.message
            });
        }
        public ActionResult LoiPhanQuyen()
        {
            return View();
        }
        [AdminAuthorize(ChucNang = "TaiKhoanNV_Them")]
        public ActionResult ThemMoi()
        {
            return View();
        }
        [HttpPost]
        [AdminAuthorize(ChucNang = "TaiKhoanNV_Them")]
        public ActionResult ThemMoi(TaiKhoanNV model)
        {
            var map = new Models.Map.mapTaiKhoanNV();
            if (model.Username == null | model.Password == null | model.HoVaTen == null)
            {
                ViewBag.error = "Bạn chưa nhập đủ thông tin.";
                return View();
            }
            var user = map.DangKy(model);
            if (user != null)
            {
                SV.App_Start.SessionConfig.SetTaiKhoanNV(user);
                return Redirect("~/Admin/TaiKhoanNV/ChiTiet?id=" + user.ID);
            }
            else
            {
                ModelState.AddModelError("", map.message);
                return View(model);
            }
        }
        [AdminAuthorize(ChucNang = "TaiKhoanNV_Sua")]
        public ActionResult CapNhat(int id)
        {
            return View(new mapTaiKhoanNV().ChiTiet(id));
        }
        [HttpPost]
        public ActionResult CapNhat(TaiKhoanNV model)
        {
            var map = new Models.Map.mapTaiKhoanNV();
            if (model.Username == null | model.HoVaTen == null)
            {
                ViewBag.error = "Bạn chưa nhập đủ thông tin.";
                return View();
            }
            var user = map.CapNhat(model);
            if (user != null)
            {
                SV.App_Start.SessionConfig.SetTaiKhoanNV(user);
                return Redirect("~/Admin/TaiKhoanNV/ChiTiet?id=" + user.ID);
            }
            else
            {
                ModelState.AddModelError("", map.message);
                return View(model);
            }
        }
        [AdminAuthorize(ChucNang = "TaiKhoanNV_Sua")]
        public ActionResult Khoa(int id)
        {
            var map = new mapTaiKhoanNV();
            map.Khoa(id);
            return RedirectToAction("DanhSach");
        }
        [AdminAuthorize(ChucNang = "TaiKhoanNV_Sua")]
        public ActionResult ResetMatKhau(int id)
        {
            var map = new mapTaiKhoanNV();
            map.ResetMatKhau(id);
            return RedirectToAction("ChiTiet", new { id = id });
        }
    }
}