using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV.Models.Map;
using SV.Models;
using System.IO;
using SV.App_Start;
namespace SV.Areas.Admin.Controllers
{
    public class BaiVietTuyenSinhController : Controller
    {
        [AdminAuthorize(ChucNang = "BaiVietTuyenSinh_Xem")]
        public ActionResult DanhSach()
        {
            var map = new mapBaiVietTuyenSinh();
            return View(map.DanhSach());
        }
        [AdminAuthorize(ChucNang = "BaiVietTuyenSinh_Xem")]
        public ActionResult ChiTiet(int ID)
        {
            var map = new mapBaiVietTuyenSinh();
            return View(map.ChiTiet(ID));
        }
        [AdminAuthorize(ChucNang = "BaiVietTuyenSinh_Them")]
        public ActionResult ThemMoi()
        {
            return View(new BaiVietTuyenSinh() { NgayBatDau = DateTime.Now, NgayKetThuc = DateTime.Now });
        }
        [HttpPost]
        [ValidateInput(false)]
        [AdminAuthorize(ChucNang = "BaiVietTuyenSinh_Them")]
        public ActionResult ThemMoi(BaiVietTuyenSinh model, HttpPostedFileBase file)
        {
            if (file != null)
            {
                string thuMuc = "/Data/BaiVietTuyenSinh/";
                string name = file.FileName;
                var fullPath = Server.MapPath(thuMuc) + name;
                int i = 0;
                while (System.IO.File.Exists(fullPath))
                {
                    i++;
                    name = i + "-" + name;
                    fullPath = Server.MapPath(thuMuc) + name;
                }
                file.SaveAs(fullPath);
                model.HinhAnh = thuMuc + name;
            }

            var map = new mapBaiVietTuyenSinh();
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
        [AdminAuthorize(ChucNang = "BaiVietTuyenSinh_Sua")]
        public ActionResult CapNhat(int id)
        {
            var map = new mapBaiVietTuyenSinh();
            return View(map.ChiTiet(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        [AdminAuthorize(ChucNang = "BaiVietTuyenSinh_Sua")]
        public ActionResult CapNhat(BaiVietTuyenSinh model, HttpPostedFileBase file)
        {
            if (file != null)
            {
                string thuMuc = "/Data/BaiVietTuyenSinh/";
                string name = file.FileName;
                var fullPath = Server.MapPath(thuMuc) + name;
                int i = 0;
                while (System.IO.File.Exists(fullPath))
                {
                    i++;
                    name = i + "-" + name;
                    fullPath = Server.MapPath(thuMuc) + name;
                }
                file.SaveAs(fullPath);
                model.HinhAnh = thuMuc + name;
            }
            var map = new mapBaiVietTuyenSinh();
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
        [AdminAuthorize(ChucNang = "BaiVietTuyenSinh_Xoa")]
        public ActionResult Xoa(int id)
        {
            var map = new mapBaiVietTuyenSinh();
            map.Xoa(id);
            return RedirectToAction("DanhSach");
        }
    }
}