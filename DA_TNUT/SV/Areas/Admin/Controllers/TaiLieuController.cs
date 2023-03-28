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
    public class TaiLieuController : Controller
    {
        [AdminAuthorize(ChucNang = "TaiLieu_Xem")]
        public ActionResult DanhSach(string tenvb)
        {
            var map = new mapTaiLieu();
            ViewBag.tenvb = tenvb;
            return View(map.TraCuu(tenvb));
        }
        [AdminAuthorize(ChucNang = "TaiLieu_Them")]
        public ActionResult ThemMoi()
        {
            return View(new TaiLieu());
        }
        [HttpPost]
        [ValidateInput(false)]
        [AdminAuthorize(ChucNang = "TaiLieu_Them")]
        public ActionResult ThemMoi(TaiLieu model, HttpPostedFileBase file)
        {
            // Lưu dữ liệu
            // b1. Kiểm tra file có tồn tại hay không
            // nếu tồn tại thì lưu, và lấy đường dẫn truyền Model.HinhAnh
            if (file != null)
            {
                //1. Lưu vào thư nào
                string thuMuc = "/Data/TaiLieu/";

                //2. Tên file là gì
                string name = file.FileName;

                // 3. Lưu vào server file bằng đường dẫn tuyệt đối
                var fullPath = Server.MapPath(thuMuc) + name;

                // Kiểm tra tên file tồn tại không 
                int i = 0;
                while (System.IO.File.Exists(fullPath))
                {
                    i++;
                    name = i + name;
                    fullPath = Server.MapPath(thuMuc) + name;
                }
                file.SaveAs(fullPath);

                // lưu vào database đường dẫn tương đối
                model.DuongDanFile = thuMuc + name;
            }

            var map = new mapTaiLieu();
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
        [AdminAuthorize(ChucNang = "TaiLieu_Sua")]
        public ActionResult CapNhat(int id)
        {
            var map = new mapTaiLieu();
            return View(map.ChiTiet(id));
        }
        [HttpPost]
        [AdminAuthorize(ChucNang = "TaiLieu_Sua")]
        public ActionResult CapNhat(TaiLieu model, HttpPostedFileBase file)
        {
            // Lưu dữ liệu
            // b1. Kiểm tra file có tồn tại hay không
            // nếu tồn tại thì lưu, và lấy đường dẫn truyền Model.HinhAnh
            var tl = new mapTaiLieu().ChiTiet(model.ID);
            if (file != null)
            {
                //1. Lưu vào thư nào
                string thuMuc = "/Data/TaiLieu/"; 
                //2. Tên file là gì
                string name = file.FileName; 
                // 3. Lưu vào server file bằng đường dẫn tuyệt đối
                var fullPath = Server.MapPath(thuMuc) + name;
                int i = 0;
                while (System.IO.File.Exists(fullPath))
                {
                    i++;
                    name = i + name;
                    fullPath = Server.MapPath(thuMuc) + name;
                }
                // Kiểm tra tên file tồn tại không  
                file.SaveAs(fullPath); 
                // lưu vào database đường dẫn tương đối
                model.DuongDanFile = thuMuc + name;
            }
            else
            {
                model.DuongDanFile = tl.DuongDanFile;
            }
            var map = new mapTaiLieu();
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
        [AdminAuthorize(ChucNang = "TaiLieu_Xoa")]
        public ActionResult Xoa(int id)
        {
            var map = new mapTaiLieu();
            map.Xoa(id);
            return RedirectToAction("DanhSach");
        }
    }
}
