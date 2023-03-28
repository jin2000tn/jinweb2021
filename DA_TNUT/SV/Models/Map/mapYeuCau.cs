using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV.Models;

namespace SV.Models.Map
{
    public class mapYeuCau
    {
        public Entities db = new Entities();
        public string message = "";
        //Hàm danh sách
        public List<YeuCau> DanhSach(string sinhVien, bool? trangThai)
        {
            try
            {
                return db.YeuCaus.Where(m => (m.SinhVien.HoVaTen.ToLower().Contains(sinhVien.ToLower()) == true | string.IsNullOrEmpty(sinhVien) == true)
                                    & ((m.DaTraLoi??false) == trangThai | trangThai == null)
                ).OrderByDescending(m=>m.ThoiGian).ToList();
            }
            catch
            {
                return new List<YeuCau>();
            }
        }
        public List<YeuCau> DanhSachTheoSinhVien(int idSinhVien)
        {
            try
            {
                return db.YeuCaus.Where(m =>   m.idSinhVien  == idSinhVien ).OrderByDescending(m=>m.ThoiGian).ToList();
            }
            catch
            {
                return new List<YeuCau>();
            }
        }
        //Chi tiết
        public YeuCau ChiTiet(int id)
        {
            try
            {
                return db.YeuCaus.Find(id);
            }
            catch
            {
                return new YeuCau();
            }
        }
        //Thêm mới: ok->id;false:0
        public int ThemMoi(YeuCau model)
        {
            if (string.IsNullOrEmpty(model.NoiDung) == true)
            {
                message = "Nhập thiếu nội dung";
                return 0;
            }
            try
            {
                db.YeuCaus.Add(model);
                db.SaveChanges();
                return model.ID;
            }
            catch
            {
                message = "Lỗi hệ thống, vui lòng thử lại";
                return 0;
            }
        }

        //Câp nhập
        public int CapNhap(YeuCau model)
        {
            var update = db.YeuCaus.Find(model.ID);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return 0;
            }
            if (string.IsNullOrEmpty(model.NoiDung) == true)
            {
                message = "Nhập thiếu nội dung";
                return 0;
            }
            try
            {
                update.ID = model.ID;
                update.idSinhVien = model.idSinhVien;
                update.NoiDung = model.NoiDung;
                update.ThoiGian = model.ThoiGian;
                update.TraLoi = model.TraLoi;
                update.DaTraLoi = model.DaTraLoi;
                db.SaveChanges();
                return model.ID;
            }
            catch
            {
                message = "Lỗi hệ thống, vui lòng thử lại";
                return 0;
            }
        }

        //Xóa
        public bool Xoa(int id)
        {
            var update = db.YeuCaus.Find(id);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return false;
            }
            try
            {
                db.YeuCaus.Remove(update);
                db.SaveChanges();
                return true;
            }
            catch
            {
                message = "Lỗi hệ thống , vui lòng thử lại";
                return false;
            }
        }
    }
}