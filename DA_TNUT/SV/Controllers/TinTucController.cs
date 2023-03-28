using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV.Models;
using SV.Models.Map;
using System.IO;

namespace SV.Controllers
{
    public class TinTucController : Controller
    { 
        public ActionResult Index(int? page)
        {
            ViewBag.page = page ?? 1;
            return View(new mapTinTuc().DanhSach(page??1));
        }
        public ActionResult ChiTiet(string link)
        {
            var map = new mapTinTuc();
            return View(map.ChiTiet(link));
        } 
        public ActionResult TimKiem(string tukhoa)
        {
            var map = new mapTinTuc();
            ViewBag.tukhoa = tukhoa;
            return View(map.TimKiem(tukhoa));
        }
        public ActionResult TinTrongThang(int nam, int thang)
        {
            var map = new mapTinTuc();
            ViewBag.nam = nam;
            ViewBag.thang = thang;
            return View(map.TinTrongThang(nam, thang));
        }
    }
}