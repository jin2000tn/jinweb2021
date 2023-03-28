using SV.Models.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SV.App_Start
{
    public class AdminAuthorize : AuthorizeAttribute
    {
        public string ChucNang = "";
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (SessionConfig.GetTaiKhoanNV() == null)
            {
                // Chuyển hướng đường link 
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "TaiKhoanNV", action = "DangNhap" }));
                return;
            }
            // Kiểm tra quyền
            if (string.IsNullOrEmpty(ChucNang) == false)
            {
                var mapPQ = new mapPhanQuyen();
                if (mapPQ.KiemTraQuyen(SessionConfig.GetTaiKhoanNV().ID, ChucNang) == false)
                {
                    // Chuyển hướng đường link trang báo lỗi phân quyền
                    filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary(new { controller = "TaiKhoanNV", action = "LoiPhanQuyen" }));
                    return;
                }
            } 
            return;
        }
    }
}