using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV.Models;
using SV.Models.Map;
using SV.App_Start;

namespace SV.Controllers
{
    public class TaiKhoanController : Controller
    {
        public ActionResult DangNhap(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string tenDangNhap, string matKhau, string returnUrl)
        {
            ViewBag.tenDangNhap = tenDangNhap;
            ViewBag.matKhau = matKhau;
            var map = new Models.Map.mapSinhVien();
            matKhau = new SV.Helper.Encrypt().CreateMD5(matKhau ?? "");
            var tk = map.DangNhap(tenDangNhap, matKhau);
            if (tk != null)
            {
                SV.App_Start.SessionConfig.SetTaiKhoan(tk);
                if (string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect("/");
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
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DangKy(SinhVien model, HttpPostedFileBase file)
        {
            if (file != null)
            {
                //1. Lưu vào thư nào
                string thuMuc = "/Data/TaiKhoanSV/";

                //2. Tên file là gì
                string name = file.FileName;

                // 3. Lưu vào server file bằng đường dẫn tuyệt đối
                var fullPath = Server.MapPath(thuMuc) + name;

                // Kiểm tra tên file tồn tại không 

                file.SaveAs(fullPath);

                // lưu vào database đường dẫn tương đối
                model.AnhDaiDien = thuMuc + name;
            }
            var map = new Models.Map.mapSinhVien();
            if (model.TenDangNhap == null | model.MatKhau == null | model.HoVaTen == null)
            {
                ViewBag.error = "Bạn chưa điền đầy đủ thông tin";
                return View();
            }
            var tk = map.DangKy(model);
            if (tk != null)
            {
                SV.App_Start.SessionConfig.SetTaiKhoan(tk);
                return Redirect("/thong-tin-tai-khoan");
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
            return Redirect("/");
        }
        [SinhVienAuthorize]
        public ActionResult ThongTin()
        {
            var user = SV.App_Start.SessionConfig.GetTaiKhoan();
            return View(new mapSinhVien().ChiTiet(user.ID));
        }
        [SinhVienAuthorize]
        public ActionResult CapNhat()
        {
            var user = SV.App_Start.SessionConfig.GetTaiKhoan();
            return View(new mapSinhVien().ChiTiet(user.ID));
        }
        [HttpPost]
        [SinhVienAuthorize]
        public ActionResult CapNhat(SinhVien model, HttpPostedFileBase file)
        {
            var user = SV.App_Start.SessionConfig.GetTaiKhoan();
            if (file != null)
            {
                //1. Lưu vào thư nào
                string thuMuc = "/Data/TaiKhoanSV/";
                //2. Tên file là gì
                string name = file.FileName;
                // 3. Lưu vào server file bằng đường dẫn tuyệt đối
                var fullPath = Server.MapPath(thuMuc) + name;
                // Kiểm tra tên file tồn tại không  
                file.SaveAs(fullPath);
                // lưu vào database đường dẫn tương đối
                model.AnhDaiDien = thuMuc + name;
            }
            else
            {
                model.AnhDaiDien = user.AnhDaiDien;
            }
            var map = new Models.Map.mapSinhVien();
            if (model.TenDangNhap == null | model.HoVaTen == null)
            {
                ModelState.AddModelError("", "Bạn chưa điền đầy đủ thông tin");
                return View(model);
            }
            model.ID = user.ID;
            if (map.CapNhat(model))
            {
                SV.App_Start.SessionConfig.SetTaiKhoan(model);
                return Redirect("/thong-tin-tai-khoan");
            }
            else
            {
                ModelState.AddModelError("", map.message);
                return View();
            }
        }

        [SinhVienAuthorize]
        public ActionResult DoiMatKhau()
        {
            var user = SV.App_Start.SessionConfig.GetTaiKhoan();
            return View(new mapSinhVien().ChiTiet(user.ID));
        }
        [HttpPost]
        [SinhVienAuthorize]
        public ActionResult DoiMatKhau(string matKhauCu, string matKhauMoi)
        {
            var user = SV.App_Start.SessionConfig.GetTaiKhoan();
            var map = new Models.Map.mapSinhVien();
            if (map.DoiMatKhau(user.ID, matKhauCu, matKhauMoi))
            {
                return Redirect("/thong-tin-tai-khoan");
            }
            else
            {
                ModelState.AddModelError("", map.message);
                return View();
            }
        }
    }
}