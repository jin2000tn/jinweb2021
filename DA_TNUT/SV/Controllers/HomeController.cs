using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV.Models.Map;

namespace SV.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BaiViet(string link)
        {
            var bv = new mapTrangGioiThieu().ChiTiet(link);
            return View(bv);
        } 
    }
}