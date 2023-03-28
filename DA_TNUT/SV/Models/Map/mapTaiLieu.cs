using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV.Models;
namespace SV.Models.Map
{
    public class mapTaiLieu
    {
        public Entities db = new Entities();
        public string message = "";

        // Hàm danh sách
        public List<TaiLieu> DanhSach()
        {
            try
            {
                return db.TaiLieux.ToList();
            }
            catch
            {
                return new List<TaiLieu>();
            }
        }
        public List<TaiLieu> DanhSachTop(int soLuong)
        {
            try
            {
                return db.TaiLieux.OrderByDescending(m=>m.ThoiGianUpload).Take(soLuong).ToList();
            }
            catch
            {
                return new List<TaiLieu>();
            }
        }
        public List<TaiLieu> DanhSachTheoNhom(string nhom)
        {
            try
            {
                return db.TaiLieux.Where(m => (m.NhomTaiLieu ?? "").ToLower() == nhom.ToLower()).ToList();
            }
            catch
            {
                return new List<TaiLieu>();
            }
        }

        // Chi tiết
        public TaiLieu ChiTiet(int id)
        {
            try
            {
                return db.TaiLieux.Find(id);
            }
            catch
            {
                return new TaiLieu();
            }
        }


        // Thêm mới: ok -> id, false: 0
        public int ThemMoi(TaiLieu model)
        {
            if (string.IsNullOrEmpty(model.TenTaiLieu) == true)
            {
                message = "Nhập thiếu tên tài liệu";
                return 0;
            }
            try
            {
                db.TaiLieux.Add(model);
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
        public int CapNhat(TaiLieu model)
        {
            var update = db.TaiLieux.Find(model.ID);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return 0;
            }
            if (string.IsNullOrEmpty(model.TenTaiLieu) == true)
            {
                message = "Nhập thiếu tên tài liệu";
                return 0;
            }
            try
            {
                update.TenTaiLieu = model.TenTaiLieu;
                update.ThongTin = model.ThongTin;
                update.DuongDanFile = model.DuongDanFile;
                update.ThoiGianUpload = model.ThoiGianUpload;
                update.NguoiUpload = model.NguoiUpload;
                update.TrangThaiPheDuyet = model.TrangThaiPheDuyet;
                update.NhomTaiLieu = model.NhomTaiLieu;
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
            var update = db.TaiLieux.Find(id);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return false;
            }
            try
            {
                db.TaiLieux.Remove(update);
                db.SaveChanges();
                return true;
            }
            catch
            {
                message = "Lỗi hệ thống, vui lòng thử lại";
                return false;
            }
        }
        public List<TaiLieu> TraCuu(string tenVanBan)
        {
            try
            {
                return (from item in db.TaiLieux
                        where item.TenTaiLieu.ToLower().Contains(tenVanBan.ToLower()) == true | string.IsNullOrEmpty(tenVanBan)
                        orderby item.ThoiGianUpload descending
                        select item
                    ).ToList();
            }
            catch
            {
                return new List<TaiLieu>();
            }
        }

        public List<string> DanhSachNhom()
        {
            try
            {
                var listNhom = (from item in db.TaiLieux
                                where string.IsNullOrEmpty(item.NhomTaiLieu) ==false
                                select item.NhomTaiLieu).Distinct().ToList();
                return listNhom;
            }
            catch
            {
                return new List<string>();
            }
        }
    }
}