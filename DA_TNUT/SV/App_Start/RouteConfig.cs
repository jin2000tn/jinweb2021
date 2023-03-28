using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SV
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                         name: "thong-tin",
                         url: "thong-tin/{link}",
                         defaults: new { controller = "Home", action = "BaiViet", id = UrlParameter.Optional },
                         namespaces: new string[] { "SV.Controllers" }
                        );

            #region TRAO ĐỔI
            routes.MapRoute(
                         name: "thao-luan",
                         url: "thao-luan",
                         defaults: new { controller = "TraoDoi", action = "Index", id = UrlParameter.Optional },
                         namespaces: new string[] { "SV.Controllers" }
                        );
            routes.MapRoute(
                         name: "thong-tin-thao-luan",
                         url: "thong-tin-thao-luan",
                         defaults: new { controller = "TraoDoi", action = "ChiTiet", id = UrlParameter.Optional },
                         namespaces: new string[] { "SV.Controllers" }
                        );
            routes.MapRoute(
                        name: "bai-dang-cua-toi",
                        url: "bai-dang-cua-toi",
                        defaults: new { controller = "TraoDoi", action = "BaiDangCuaToi", id = UrlParameter.Optional },
                        namespaces: new string[] { "SV.Controllers" }
                       );
            routes.MapRoute(
                        name: "bai-dang-trong-thang",
                        url: "bai-dang-trong-thang",
                        defaults: new { controller = "TraoDoi", action = "BaiDangTrongThang", id = UrlParameter.Optional },
                        namespaces: new string[] { "SV.Controllers" }
                       );
            routes.MapRoute(
                        name: "xoa-bai-dang",
                        url: "xoa-bai-dang",
                        defaults: new { controller = "TraoDoi", action = "XoaBaiDang", id = UrlParameter.Optional },
                        namespaces: new string[] { "SV.Controllers" }
                       );
            #endregion

            #region VĂN BẢN
            routes.MapRoute(
             name: "van-ban",
             url: "van-ban",
             defaults: new { controller = "TaiLieu", action = "Index", id = UrlParameter.Optional },
             namespaces: new string[] { "SV.Controllers" }
            );
            routes.MapRoute(
            name: "nhom-van-ban",
            url: "nhom-van-ban",
            defaults: new { controller = "TaiLieu", action = "NhomTaiLieu", id = UrlParameter.Optional },
            namespaces: new string[] { "SV.Controllers" }
            );
            #endregion

            #region YÊU CẦU
            routes.MapRoute(
        name: "yeu-cau",
        url: "yeu-cau",
        defaults: new { controller = "YeuCau", action = "Index", id = UrlParameter.Optional },
        namespaces: new string[] { "SV.Controllers" }
    );
            routes.MapRoute(
        name: "gui-yeu-cau",
        url: "gui-yeu-cau",
        defaults: new { controller = "YeuCau", action = "ThemMoi", id = UrlParameter.Optional },
        namespaces: new string[] { "SV.Controllers" }
    );

            #endregion

            #region Đăng nhập
            routes.MapRoute(
          name: "dang-nhap",
          url: "dang-nhap",
          defaults: new { controller = "TaiKhoan", action = "DangNhap", id = UrlParameter.Optional },
          namespaces: new string[] { "SV.Controllers" }
      );
            routes.MapRoute(
        name: "thong-tin-tai-khoan",
        url: "thong-tin-tai-khoan",
        defaults: new { controller = "TaiKhoan", action = "ThongTin", id = UrlParameter.Optional },
        namespaces: new string[] { "SV.Controllers" }
    );
            routes.MapRoute(
          name: "dang-xuat",
          url: "dang-xuat",
          defaults: new { controller = "TaiKhoan", action = "DangXuat", id = UrlParameter.Optional },
          namespaces: new string[] { "SV.Controllers" }
      );
            routes.MapRoute(
          name: "dang-ky",
          url: "dang-ky",
          defaults: new { controller = "TaiKhoan", action = "DangKy", id = UrlParameter.Optional },
          namespaces: new string[] { "SV.Controllers" }
      );
            routes.MapRoute(
          name: "cap-nhat-tai-khoan",
          url: "cap-nhat-tai-khoan",
          defaults: new { controller = "TaiKhoan", action = "CapNhat", id = UrlParameter.Optional },
          namespaces: new string[] { "SV.Controllers" }
      );
            routes.MapRoute(
          name: "doi-mat-khau",
          url: "doi-mat-khau",
          defaults: new { controller = "TaiKhoan", action = "DoiMatKhau", id = UrlParameter.Optional },
          namespaces: new string[] { "SV.Controllers" }
      );

            #endregion

            #region TUYỂN SINH
            routes.MapRoute(
            name: "dang-ky-tu-van-tuyen-sinh",
            url: "dang-ky-tu-van-tuyen-sinh",
            defaults: new { controller = "TuyenSinh", action = "DangKy", id = UrlParameter.Optional },
            namespaces: new string[] { "SV.Controllers" }
        );
            routes.MapRoute(
            name: "Thông tin tuyển sinh",
            url: "tuyen-sinh",
            defaults: new { controller = "TuyenSinh", action = "Index", id = UrlParameter.Optional },
            namespaces: new string[] { "SV.Controllers" }
        );
            routes.MapRoute(
            name: "Xem bài viết tuyển sinh",
            url: "tuyen-sinh/{link}",
            defaults: new { controller = "TuyenSinh", action = "ChiTiet", id = UrlParameter.Optional },
            namespaces: new string[] { "SV.Controllers" }
        );
            #endregion


            #region TIN TỨC
            routes.MapRoute(
            name: "Tin tức trong tháng",
            url: "tin-tuc-trong-thang",
            defaults: new { controller = "TinTuc", action = "TinTrongThang", id = UrlParameter.Optional },
            namespaces: new string[] { "SV.Controllers" }
        );
            routes.MapRoute(
            name: "Tìm kiếm tin tức",
            url: "tim-kiem-tin-tuc",
            defaults: new { controller = "TinTuc", action = "TimKiem", id = UrlParameter.Optional },
            namespaces: new string[] { "SV.Controllers" }
        );
            routes.MapRoute(
            name: "Chi tiết tin tức",
            url: "tin-tuc/{link}",
            defaults: new { controller = "TinTuc", action = "ChiTiet", id = UrlParameter.Optional },
            namespaces: new string[] { "SV.Controllers" }
        );
            routes.MapRoute(
                name: "Danh sách tin tức",
                url: "tin-tuc",
                defaults: new { controller = "TinTuc", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "SV.Controllers" }
            );
            #endregion
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "SV.Controllers" }
            );
        }
    }
}
