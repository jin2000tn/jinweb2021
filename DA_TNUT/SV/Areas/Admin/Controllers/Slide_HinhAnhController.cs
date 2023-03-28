using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV.App_Start;
using SV.Models;
using SV.Models.Map;

namespace SV.Areas.Admin.Controllers
{
    public class Slide_HinhAnhController : Controller
    {
        [AdminAuthorize(ChucNang = "Slide_Them")]
        public ActionResult ThemMoi(int idSlide)
        {
            return View(new Slide_HinhAnh() { idSlide = idSlide });
        }
        [HttpPost]
        [AdminAuthorize(ChucNang = "Slide_Them")]
        public ActionResult ThemMoi(Slide_HinhAnh model, HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                // lưu vào thư mục nào
                string thuMuc = "/Data/Slide";
                //tên file là gì
                string name = file.FileName;
                // lưu theo đường dẫn tuyệt đối
                var fullPath = Server.MapPath(thuMuc) + name;
                file.SaveAs(fullPath);

                // lưu theo đường dẫn tươnng đối
                model.HinhAnh = thuMuc + name;
            }


            var map = new mapSlide_HinhAnh();
            if (map.ThemMoi(model) > 0)
            {
                return Redirect("/Admin/Slide/ChiTiet?id=" + model.idSlide);
            }
            else
            {
                ModelState.AddModelError("", map.message);
                return View(model);
            }
        }
        [AdminAuthorize(ChucNang = "Slide_Sua")]
        public ActionResult CapNhat(int id)
        {

            var map = new mapSlide_HinhAnh();
            return View(map.ChiTiet(id));
        }
        [HttpPost]
        [AdminAuthorize(ChucNang = "Slide_Sua")]
        public ActionResult CapNhat(Slide_HinhAnh model, HttpPostedFileBase file)
        {

            var map = new mapSlide_HinhAnh();
            if (map.CapNhat(model) > 0)
            {
                return Redirect("/Admin/Slide/ChiTiet?id=" + model.idSlide);
            }
            else
            {
                ModelState.AddModelError("", map.message);
                return View(model);
            }
        }
        [AdminAuthorize(ChucNang = "Slide_Xoa")]
        public ActionResult Xoa(int id, int idSlide)
        {
            var map = new mapSlide_HinhAnh();
            map.Xoa(id);
            return Redirect("/Admin/Slide/ChiTiet?id=" + idSlide);
        } 
    }
}