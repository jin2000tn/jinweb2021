using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV.Models;

namespace SV.Models.Map
{
    public class mapLienHeTuyenSinh
    {
        public Entities db = new Entities();
        public string message = "";
        // hàm dánh sách
        public List<LienHeTuyenSinh> DanhSach(int? idBaiViet, bool? traLoi)
        {
            try
            {
                var ds = (from item in db.LienHeTuyenSinhs
                          where (item.idBaiVietTuyenSinh == idBaiViet | idBaiViet ==null) & ((item.DaTraLoi??false) == traLoi | traLoi == null)
                          orderby item.ThoiGianNhan descending
                          select item).ToList();
                return ds;
            }
            catch
            {
                return new List<LienHeTuyenSinh>();
            }
        }
        // chi tiết
        public LienHeTuyenSinh ChiTiet(int id)
        {
            try
            {
                return db.LienHeTuyenSinhs.Find(id);
            }
            catch
            {
                return new LienHeTuyenSinh();
            }
        }
        // Thêm Mới
        public int ThemMoi(LienHeTuyenSinh model)
        {
            try
            {
                if(string.IsNullOrEmpty(model.HoTen)== true)
                {
                    message = "Bạn chưa nhập thông tin họ tên";
                    return 0;
                }
                if (string.IsNullOrEmpty(model.HoTen)== true)
                {
                    message = "Bạn chưa nhập thông tin số điện thoại";
                    return 0;
                }
                model.ThoiGianNhan = DateTime.Now;
                model.DaTraLoi = false; 
                db.LienHeTuyenSinhs.Add(model);
                db.SaveChanges(); 
                return model.ID;
            }
            catch
            {
                message = "Lỗi hệ thống, vui lòng thử lại";
                return 0;
            }
        }
        public int CapNhat(LienHeTuyenSinh model)
        {
            var update = db.LienHeTuyenSinhs.Find(model.ID);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return 0;
            }
           
            try
            {
                update.ThoiGianNhan = model.ThoiGianNhan;
                update.HoTen = model.HoTen;
                update.Email = model.Email;
                update.GhiChu= model.GhiChu;
                update.QueQuan = model.QueQuan;
                update.DaTraLoi = model.DaTraLoi;
                update.SoDienThoai = model.SoDienThoai;
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
            var update = db.LienHeTuyenSinhs.Find(id);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return false;
            }
            try
            {
                db.LienHeTuyenSinhs.Remove(update);
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