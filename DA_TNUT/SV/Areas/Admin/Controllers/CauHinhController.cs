using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SV.App_Start;
using SV.Models;

namespace SV.Areas.Admin.Controllers
{
    public class CauHinhController : Controller
    {
        private Entities db = new Entities();
        [AdminAuthorize(ChucNang = "CauHinh_Xem")]
        public ActionResult DanhSach(string nhom)
        {
            ViewBag.nhom = nhom;
            return View();
        }
        [AdminAuthorize(ChucNang = "CauHinh_Sua")]
        public ActionResult CapNhat(string maCauHinh)
        { 
            CauHinh cauHinh = db.CauHinhs.Find(maCauHinh);
            if (cauHinh == null)
            {
                return HttpNotFound();
            } 
            return View(cauHinh);
        }
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminAuthorize(ChucNang = "CauHinh_Sua")]
        public ActionResult CapNhat(CauHinh cauHinh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cauHinh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSach", new { nhom = cauHinh.NhomThietLap });
            }
            return View(cauHinh);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
