using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV.Models;

namespace SV.Models.Map
{
    public class mapBaiDang
    {
        Entities db = new Entities();
        public string message = "";

        //Hàm danh sách
        public List<BaiDang> DanhSach()
        {
            try
            {
                return db.BaiDangs.ToList();
            }
            catch
            {
                return new List<BaiDang>();
            }
        }
        public List<BaiDang> DanhSachTop(int soLuong)
        {
            try
            {
                return db.BaiDangs.OrderByDescending(m=>m.ThoiGian).Take(soLuong).ToList();
            }
            catch
            {
                return new List<BaiDang>();
            }
        }
        public List<BaiDang> DanhSach(int page)
        {
            try
            {
                return db.BaiDangs.OrderByDescending(m=>m.ThoiGian).Skip((page-1)*10).Take(10).ToList();
            }
            catch
            {
                return new List<BaiDang>();
            }
        }
        public List<BaiDang> TraCuu(string tenSinhVien)
        {
            try
            {
                return (from item in db.BaiDangs
                        join sv in db.SinhViens on item.idSinhVien equals sv.ID
                        where sv.HoVaTen.ToLower().Contains(tenSinhVien.ToLower()) == true | string.IsNullOrEmpty(tenSinhVien)
                        select item
                        ).ToList();
            }
            catch
            {
                return new List<BaiDang>();
            }
        }

        public List<BaiDang> DanhSachCaNhan(int taiKhoan)
        {
            try
            {
                return db.BaiDangs.Where(m => m.idSinhVien == taiKhoan).OrderByDescending(m => m.ID).ToList();
            }
            catch
            {
                return new List<BaiDang>();
            }
        }
        public List<BaiDang> BaiDangTrongThang(int thang, int nam)
        {
            try
            {
                return db.BaiDangs.Where(m => m.ThoiGian.Value.Month == thang & m.ThoiGian.Value.Year == nam).OrderByDescending(m => m.ID).ToList();
            }
            catch
            {
                return new List<BaiDang>();
            }
        }
        public int SoLuongTrongThang(int thang, int nam)
        {
            try
            {
                return db.BaiDangs.Count(m => m.ThoiGian.Value.Month == thang & m.ThoiGian.Value.Year == nam);
            }
            catch
            {
                return 0;
            }
        }

        // Chi tiết
        public BaiDang ChiTiet(int id)
        {
            try
            {
                return db.BaiDangs.Find(id);
            }
            catch
            {
                return new BaiDang();
            }
        }
        public void TangLuotXem(int id)
        {
            try
            {
                var baiDang =  db.BaiDangs.Find(id);
                baiDang.LuotXem = (baiDang.LuotXem ?? 0) + 1;
                db.SaveChanges();
            }
            catch
            { 
            }
        }


        // Thêm mới: ok -> id, false: 0
        public BaiDang ThemMoi(BaiDang model)
        {
            if (db.SinhViens.Find(model.idSinhVien) == null)
            {
                message = "Chưa đăng nhập tài khoản. Vui lòng đăng nhập hoặc đăng ký tài khoản.";
                return null;
            }
            if (string.IsNullOrEmpty(model.NoiDung) == true)
            {
                message = "Bạn chưa nhập nội dung";
                return null;
            }
            try
            {
                model.ThoiGian = DateTime.Now;
                db.BaiDangs.Add(model);
                db.SaveChanges();
                return model;
            }
            catch
            {
                message = "Lỗi hệ thống, vui lòng thử lại";
                return null;
            }
        }

        // Cập nhật
        public BaiDang CapNhat(BaiDang model, int idSinhVien)
        {
            var update = db.BaiDangs.Find(model.ID);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return null;
            }
            if (update.idSinhVien != idSinhVien)
            {
                message = "Bài đăng không phải của bạn, không thể cập nhật bài đăng";
                return null;
            }
            if (db.SinhViens.Find(model.idSinhVien) == null)
            {
                message = "Chưa đăng nhập tài khoản. Vui lòng đăng nhập hoặc đăng ký tài khoản.";
                return null;
            }
            if (string.IsNullOrEmpty(model.NoiDung) == true)
            {
                message = "Bạn chưa nhập nội dung";
                return null;
            }
            try
            {
                update.NoiDung = model.NoiDung;
                update.HinhAnh = model.HinhAnh;
                db.SaveChanges();
                return model;
            }
            catch
            {
                message = "Lỗi hệ thống, vui lòng thử lại";
                return null;
            }
        }

        // Xóa
        public bool Xoa(int id)
        {
            var update = db.BaiDangs.Find(id);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return false;
            }
            try
            {
                db.BaiDangs.Remove(update);
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