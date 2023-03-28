using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV.Models.Map;
using SV.Models;
using SV.App_Start;

namespace SV.Controllers
{
    public class TraoDoiController : Controller
    {
        public ActionResult Index(int? page)
        {
            ViewBag.page = page ?? 1;
            return View(new mapBaiDang().DanhSach(page ?? 1));
        }
        public ActionResult ChiTiet(int post)
        {
            new mapBaiDang().TangLuotXem(post);
            return View(new mapBaiDang().ChiTiet(post));
        }
        [HttpPost]
        [ValidateInput(false)]
        [SinhVienAuthorize]
        public ActionResult BaiDang(BaiDang model, HttpPostedFileBase file)
        {
            var user = SV.App_Start.SessionConfig.GetTaiKhoan(); 
            if (model.NoiDung == null)
            {
                ViewBag.error = "Bài đăng chưa có nội dung";
                return View();
            }
            if (file != null)
            {
                //1. Lưu vào thư nào
                string thuMuc = "/Data/BaiDang/";
                //2. Tên file là gì
                string name = file.FileName;
                // 3. Lưu vào server file bằng đường dẫn tuyệt đối
                var fullPath = Server.MapPath(thuMuc) + name;
                int i = 0;
                if (System.IO.File.Exists(fullPath))
                {
                    name = i + "_" + name;
                    fullPath = Server.MapPath(thuMuc) + name;
                }
                // Kiểm tra tên file tồn tại không  
                file.SaveAs(fullPath);
                // lưu vào database đường dẫn tương đối
                model.HinhAnh = thuMuc + name;
            } 
            model.idSinhVien = SessionConfig.GetTaiKhoan().ID;
            model.ThoiGian = DateTime.Now;
            model.LuotXem = 0;
            var map = new Models.Map.mapBaiDang();
            var tk = map.ThemMoi(model);
            if (tk != null)
            {
                return Redirect("/TraoDoi/baidangcuatoi");
            }
            else
            {
                ViewBag.error = map.message;
                return Redirect("/thao-luan");
            } 
        }
        [SinhVienAuthorize]
        public ActionResult BaiDangCuaToi()
        {
            var user = SV.App_Start.SessionConfig.GetTaiKhoan();
            return View(new mapBaiDang().DanhSachCaNhan(SessionConfig.GetTaiKhoan().ID));
        }
        public ActionResult BaiDangTrongThang(int thang)
        {
            ViewBag.thang = thang;
            return View(new mapBaiDang().BaiDangTrongThang(thang, DateTime.Now.Year));
        } 
        [SinhVienAuthorize]
        public ActionResult XoaBaiDang(int post)
        {
            var baiDang = new mapBaiDang().ChiTiet(post);
            if(baiDang.idSinhVien == SessionConfig.GetTaiKhoan().ID)
            {
                new mapBaiDang().Xoa(post);
            }
            return Redirect("bai-dang-cua-toi");
        }
    }
}