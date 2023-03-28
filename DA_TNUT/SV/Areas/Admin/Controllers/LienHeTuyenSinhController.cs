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
    public class LienHeTuyenSinhController : Controller
    {
        [AdminAuthorize(ChucNang = "LienHeTuyenSinh_Xem")]
        public ActionResult DanhSach(int? idBaiViet, bool? trangThai)
        {
            ViewBag.idBaiViet = idBaiViet;
            ViewBag.trangThai = trangThai??false;
            var map = new mapLienHeTuyenSinh();
            return View(map.DanhSach(idBaiViet, trangThai??false));
        }
        [AdminAuthorize(ChucNang = "LienHeTuyenSinh_Them")]
        public ActionResult ThemMoi()
        {
            
            return View(new LienHeTuyenSinh());
        }
        [HttpPost]
        [AdminAuthorize(ChucNang = "LienHeTuyenSinh_Them")]
        public ActionResult ThemMoi(LienHeTuyenSinh model)
        {
            model.ThoiGianNhan = DateTime.Now;
            model.DaTraLoi = false;
            var map = new mapLienHeTuyenSinh();
            if (map.ThemMoi(model) > 0)
            {
                ModelState.AddModelError("", "Đăng ký thành công");
                return RedirectToAction("DanhSach", new { idBaiViet  = model.idBaiVietTuyenSinh, trangThai =  model.DaTraLoi });
            }
            else
            {
                ModelState.AddModelError("", map.message);
                return View(model);
            }
        }
        [AdminAuthorize(ChucNang = "LienHeTuyenSinh_Sua")]
        public ActionResult CapNhat(int id)
        {
            var map = new mapLienHeTuyenSinh();
            return View(map.ChiTiet(id));
        } 
        [HttpPost]
        [ValidateInput(false)]
        [AdminAuthorize(ChucNang = "LienHeTuyenSinh_Sua")]
        public ActionResult CapNhat(LienHeTuyenSinh model)
        {
            var map = new mapLienHeTuyenSinh();
            if (map.CapNhat(model) > 0)
            {
                return RedirectToAction("DanhSach", new { idBaiViet = model.idBaiVietTuyenSinh, trangThai = model.DaTraLoi });
            }
            else
            {
                ModelState.AddModelError("", map.message);
                return View(model);
            }
        }
        [AdminAuthorize(ChucNang = "LienHeTuyenSinh_Xoa")]
        public ActionResult Xoa(int id)
        {
            var map = new mapLienHeTuyenSinh();
            map.Xoa(id);
            return RedirectToAction("DanhSach");
        }
       
    }
}