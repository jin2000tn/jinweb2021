using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV.Models;
using SV.Models.Map;

namespace SV.Controllers
{
    public class ChuongTrinhDaoTaoController : Controller
    {
        public ActionResult Index()
        {
            var map = new mapChuongTrinhDaoTao();
            return View(map.DanhSach());
        }

        public ActionResult ChiTiet(int id)
        {
            var map = new mapChuongTrinhDaoTao();
            return View(map.ChiTiet(id));
        }
    }
}