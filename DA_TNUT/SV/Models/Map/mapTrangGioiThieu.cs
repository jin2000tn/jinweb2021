using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV.Models;
namespace SV.Models.Map
{
    public class mapTrangGioiThieu
    {
        public Entities db = new Entities();
        public string message = "";

        // Hàm danh sách
        public List<TrangGioiThieu> DanhSach()
        {
            try
            {
                return db.TrangGioiThieux.ToList();
            }
            catch
            {
                return new List<TrangGioiThieu>();
            }
        }

        // Chi tiết
        public TrangGioiThieu ChiTiet(int id)
        {
            try
            {
                return db.TrangGioiThieux.Find(id);
            }
            catch
            {
                return new TrangGioiThieu();
            }
        }     public TrangGioiThieu ChiTiet(string link)
        {
            try
            {
                return db.TrangGioiThieux.SingleOrDefault(m=>m.LinkSeo.ToLower() == link.ToLower());
            }
            catch
            {
                return new TrangGioiThieu();
            }
        }


        // Thêm mới: ok -> id, false: 0
        public int ThemMoi(TrangGioiThieu model)
        {
            if (string.IsNullOrEmpty(model.TenTrang) == true)
            {
                message = "Nhập thiếu tên trang";
                return 0;
            }
            try
            {
                db.TrangGioiThieux.Add(model);
                db.SaveChanges();
                return model.ID;
            }
            catch
            {
                message = "Lỗi hệ thống, vui lòng thử lại";
                return 0;
            }
        }

        // Cập nhật
        public int CapNhat(TrangGioiThieu model)
        {
            var update = db.TrangGioiThieux.Find(model.ID);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return 0;
            }
            if (string.IsNullOrEmpty(model.TenTrang) == true)
            {
                message = "Nhập thiếu tên trang";
                return 0;
            }
            try
            {
                update.LinkSeo = model.LinkSeo;
                update.NoiDung = model.NoiDung;
                update.TenTrang = model.TenTrang;
                db.SaveChanges();
                return model.ID;
            }
            catch
            {
                message = "Lỗi hệ thống, vui lòng thử lại";
                return 0;
            }
        }

        // Xóa
        public bool Xoa(int id)
        {
            var update = db.TrangGioiThieux.Find(id);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return false;
            }
            try
            {
                db.TrangGioiThieux.Remove(update);
                db.SaveChanges();
                return true;
            }
            catch
            {
                message = "Lỗi hệ thống, vui lòng thử lại";
                return false;
            }
        } 
    }
}