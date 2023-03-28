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
    public class TinTucController : Controller
    {
        [AdminAuthorize(ChucNang = "TinTuc_Xem")]
        public ActionResult DanhSach(bool? sukien)
        {
            var map = new mapTinTuc();
            ViewBag.sukien = sukien ?? false;
            if (sukien != true)
            {
                return View(map.DanhSachTinTuc());
            }
            else
            {
                return View(map.DanhSachSuKien());
            }
        }
        [AdminAuthorize(ChucNang = "TinTuc_Xem")]
        public ActionResult ChiTiet(int ID)
        {
            var map = new mapTinTuc();
            return View(map.ChiTiet(ID));
        }
        [AdminAuthorize(ChucNang = "TinTuc_Them")]
        public ActionResult ThemMoi()
        {
            return View(new TinTuc() { ThoiGian = DateTime.Now });
        }
        [HttpPost]
        [ValidateInput(false)]
        [AdminAuthorize(ChucNang = "TinTuc_Them")]
        public ActionResult ThemMoi(TinTuc model, HttpPostedFileBase file)
        {
            // Lưu dữ liệu
            // b1. Kiểm tra file có tồn tại hay không
            // nếu tồn tại thì lưu, và lấy đường dẫn truyền Model.HinhAnh
            if (file != null)
            {
                //1. Lưu vào thư nào
                string thuMuc = "/Data/TinTuc/";

                //2. Tên file là gì
                string name = file.FileName;

                // 3. Lưu vào server file bằng đường dẫn tuyệt đối
                var fullPath = Server.MapPath(thuMuc) + name;

                // Kiểm tra tên file tồn tại không 
                int i = 0;
                while (System.IO.File.Exists(fullPath))
                {
                    i++;
                    name = i + "-" + name;
                    fullPath = Server.MapPath(thuMuc) + name;
                }
                file.SaveAs(fullPath);

                // lưu vào database đường dẫn tương đối
                model.HinhAnh = thuMuc + name;
            }

            var map = new mapTinTuc();
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
        [AdminAuthorize(ChucNang = "TinTuc_Sua")]
        public ActionResult CapNhat(int id)
        {
            var map = new mapTinTuc();
            return View(map.ChiTiet(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        [AdminAuthorize(ChucNang = "TinTuc_Sua")]
        public ActionResult CapNhat(TinTuc model, HttpPostedFileBase file)
        {// Lưu dữ liệu
            // b1. Kiểm tra file có tồn tại hay không
            // nếu tồn tại thì lưu, và lấy đường dẫn truyền Model.HinhAnh
            if (file != null)
            {
                //1. Lưu vào thư nào
                string thuMuc = "/Data/TinTuc/";
                //2. Tên file là gì
                string name = file.FileName;
                // 3. Lưu vào server file bằng đường dẫn tuyệt đối
                var fullPath = Server.MapPath(thuMuc) + name;
                int i = 0;
                while (System.IO.File.Exists(fullPath))
                {
                    i++;
                    name = i + "-" + name;
                    fullPath = Server.MapPath(thuMuc) + name;
                }
                // Kiểm tra tên file tồn tại không  
                file.SaveAs(fullPath);
                // lưu vào database đường dẫn tương đối
                model.HinhAnh = thuMuc + name;
            }
            var map = new mapTinTuc();
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
        [AdminAuthorize(ChucNang = "TinTuc_Xoa")]
        public ActionResult Xoa(int id)
        {
            var tinTuc = new mapTinTuc().ChiTiet(id);
            bool suKien = tinTuc.isSuKien ?? false;
            var map = new mapTinTuc();
            map.Xoa(id);
            return RedirectToAction("DanhSach", new { sukien = suKien });
        }

    }
}
