using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV.Models;

namespace SV.App_Start
{
    public class SessionConfig
    {
        public static SinhVien GetTaiKhoan()
        {
            return (SinhVien)HttpContext.Current.Session["TK"];
        }
        public static void SetTaiKhoan(SinhVien tenDangNhap)
        {
            HttpContext.Current.Session["TK"] = tenDangNhap;
        }

        public static TaiKhoanNV GetTaiKhoanNV()
        {
            return (TaiKhoanNV)HttpContext.Current.Session["TKNV"];
        }
        public static void  SetTaiKhoanNV(TaiKhoanNV Username)
        {
            HttpContext.Current.Session["TKNV"] = Username;
        }
    }
}