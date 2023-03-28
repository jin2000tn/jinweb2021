using SV.Models.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SV.App_Start
{
    public class SinhVienAuthorize : AuthorizeAttribute
    { 
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (SessionConfig.GetTaiKhoan() == null)
            { 
                filterContext.Result = new RedirectResult("/dang-nhap");
                return;
            } 
            return;
        }
    }
}