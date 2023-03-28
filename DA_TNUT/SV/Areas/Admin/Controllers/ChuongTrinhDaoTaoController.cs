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
    public class ChuongTrinhDaoTaoController : Controller
    {
        [AdminAuthorize(ChucNang = "ChuongTrinhDaoTao_Xem")]
        public ActionResult DanhSach()
        {
            var map = new mapChuongTrinhDaoTao();
            return View(map.DanhSach());
        }
        [AdminAuthorize(ChucNang = "ChuongTrinhDaoTao_Xem")]
        public ActionResult ChiTiet(int ID)
        {
            var map = new mapChuongTrinhDaoTao();
            return View(map.ChiTiet(ID));
        }
        [AdminAuthorize(ChucNang = "ChuongTrinhDaoTao_Them")]
        public ActionResult ThemMoi()
        {
            return View(new ChuongTrinhDaoTao());
        }
        [HttpPost]
        [ValidateInput(false)]
        [AdminAuthorize(ChucNang = "ChuongTrinhDaoTao_Them")]
        public ActionResult ThemMoi(ChuongTrinhDaoTao model)
        {
            var map = new mapChuongTrinhDaoTao();
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
        [AdminAuthorize(ChucNang = "ChuongTrinhDaoTao_Sua")]
        public ActionResult CapNhat(int id)
        {
            var map = new mapChuongTrinhDaoTao();
            return View(map.ChiTiet(id));
        }
        [HttpPost]
        [ValidateInput(false)]
        [AdminAuthorize(ChucNang = "ChuongTrinhDaoTao_Sua")]
        public ActionResult CapNhat(ChuongTrinhDaoTao model)
        {
            var map = new mapChuongTrinhDaoTao();
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
        [AdminAuthorize(ChucNang = "ChuongTrinhDaoTao_Xoa")]
        public ActionResult Xoa(int id)
        {
            var map = new mapChuongTrinhDaoTao();
            map.Xoa(id);
            return RedirectToAction("DanhSach");
        }
    }
}