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
    [SinhVienAuthorize]
    public class YeuCauController : Controller
    {  
        public ActionResult Index()
        {
            var map = new mapYeuCau();
            var user = SessionConfig.GetTaiKhoan();
            return View(map.DanhSachTheoSinhVien(user.ID));
        }  
        public ActionResult ThemMoi()
        {
            return View(new YeuCau());
        }
        [HttpPost] 
        public ActionResult ThemMoi(string noiDung)
        {
            var map = new mapYeuCau();
            YeuCau model = new YeuCau();
            model.NoiDung = noiDung;
            model.idSinhVien = SessionConfig.GetTaiKhoan().ID;
            model.ThoiGian = DateTime.Now;
            model.TraLoi = "";
            model.DaTraLoi = false;
            if (map.ThemMoi(model) > 0)
            {
                return Redirect("/yeu-cau");
            }
            else
            {
                ModelState.AddModelError("", map.message);
                return View(model);
            }
        } 
    }
}