using SV.Models.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV.Models;
using SV.App_Start;
namespace SV.Controllers
{
    public class TaiLieuController : Controller
    {
        public ActionResult Index(string search)
        {
            var map = new mapTaiLieu();
            ViewBag.search = search;
            return View(map.TraCuu(search));
        }
        public ActionResult NhomTaiLieu(string nhom)
        {
            ViewBag.nhom = nhom;
            var map = new mapTaiLieu();
            return View(map.DanhSachTheoNhom(nhom));
        }
        public ActionResult ChiTiet(string tenTaiLieu, int id)
        {
            var map = new mapTaiLieu();
            return View(map.ChiTiet(id));
        }
        public ActionResult TaiTaiLieu(int id)
        {
            var taiLieu = new mapTaiLieu().ChiTiet(id);
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(taiLieu.DuongDanFile));
            string fileName = System.IO.Path.GetFileName(taiLieu.DuongDanFile);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Upload(string thongTin, string tenTaiLieu, HttpPostedFileBase file)
        {
            var model = new TaiLieu();
            if (file != null)
            {
                string thuMuc = "/Data/TaiLieu/";
                string name = file.FileName;
                var fullPath = Server.MapPath(thuMuc) + name;
                int i = 0;
                while (System.IO.File.Exists(fullPath))
                {
                    i++;
                    name = i + name;
                    fullPath = Server.MapPath(thuMuc) + name;
                }
                file.SaveAs(fullPath);
                model.DuongDanFile = thuMuc + name;
            }
            var user = SessionConfig.GetTaiKhoan();
            if (user != null)
            {
                model.NguoiUpload = user.HoVaTen;
            }
            else
            {
                model.NguoiUpload = "Không xác định";
            }
            model.TrangThaiPheDuyet = false;
            model.ThoiGianUpload = DateTime.Now;
            model.ThongTin = thongTin;
            model.TenTaiLieu = tenTaiLieu;
            var map = new mapTaiLieu();
            if (map.ThemMoi(model) > 0)
            {
                return Redirect("/van-ban?search=" + tenTaiLieu);
            }
            else
            {
               TempData["errorupload"] =   map.message;
               return Redirect("/van-ban?search=" + tenTaiLieu);
            }
        } 
    }
}