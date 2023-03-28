using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV.Models;
namespace SV.Models.Map
{
    public class mapBaiVietTuyenSinh
    {
        public Entities db = new Entities();
        public string message = "";

        // Hàm danh sách
        public List<BaiVietTuyenSinh> DanhSach()
        {
            try
            {
                return db.BaiVietTuyenSinhs.ToList();
            }
            catch
            {
                return new List<BaiVietTuyenSinh>();
            }
        }
        public List<BaiVietTuyenSinh> DanhSachDangTuyen()
        {
            try
            {
                return db.BaiVietTuyenSinhs.Where(m=>m.NgayBatDau <= DateTime.Now & DateTime.Now <= m.NgayKetThuc).ToList();
            }
            catch
            {
                return new List<BaiVietTuyenSinh>();
            }
        }
        public List<BaiVietTuyenSinh> DanhSach(int page)
        {
            try
            {
                return db.BaiVietTuyenSinhs.OrderByDescending(m => m.ID).Skip((page - 1) * 8).Take(8).ToList();
            }
            catch
            {
                return new List<BaiVietTuyenSinh>();
            }
        }
        // Chi tiết
        public BaiVietTuyenSinh ChiTiet(int id)
        {
            try
            {
                return db.BaiVietTuyenSinhs.Find(id);
            }
            catch
            {
                return new BaiVietTuyenSinh();
            }
        }
        public BaiVietTuyenSinh ChiTiet(string link)
        {
            try
            {
                return db.BaiVietTuyenSinhs.SingleOrDefault(m=>m.LinkSeo.ToLower() == link.ToLower());
            }
            catch
            {
                return new BaiVietTuyenSinh();
            }
        }


        // Thêm mới: ok -> id, false: 0
        public int ThemMoi(BaiVietTuyenSinh model)
        {
            if (string.IsNullOrEmpty(model.TenBaiViet) == true)
            {
                message = "Nhập thiếu tên bài viết";
                return 0;
            }
            try
            {
                db.BaiVietTuyenSinhs.Add(model);
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
        public int CapNhat(BaiVietTuyenSinh model)
        {
            var update = db.BaiVietTuyenSinhs.Find(model.ID);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return 0;
            }
            if (string.IsNullOrEmpty(model.TenBaiViet) == true)
            {
                message = "Nhập thiếu tên bài viết";
                return 0;
            }
            try
            {
                update.LinkSeo = model.LinkSeo;
                update.NoiDung = model.NoiDung;
                update.TenBaiViet = model.TenBaiViet;
                update.HinhAnh = model.HinhAnh;
                update.NgayBatDau = model.NgayBatDau;
                update.NgayKetThuc = model.NgayKetThuc;
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
            var update = db.BaiVietTuyenSinhs.Find(id);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return false;
            }
            try
            {
                db.BaiVietTuyenSinhs.Remove(update);
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