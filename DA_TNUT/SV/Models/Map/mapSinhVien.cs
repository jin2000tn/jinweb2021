using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV.Models;

namespace SV.Models.Map
{
    public class mapSinhVien
    {
        public Entities db = new Entities();
        public string message = "";
         
        public List<SinhVien> DanhSach(string sinhVien)
        {
            try
            { 
                return db.SinhViens.Where(m => (m.TenDangNhap.ToLower().Contains(sinhVien.ToLower()) == true
                | m.HoVaTen.ToLower().Contains(sinhVien.ToLower()) == true
                | m.Email.ToLower().Contains(sinhVien.ToLower()) == true
                | m.Lop.ToLower().Contains(sinhVien.ToLower()) == true
                | m.SoDienThoai.ToLower().Contains(sinhVien.ToLower()) == true
                | string.IsNullOrEmpty(sinhVien) == true)
                    
                ).ToList();
            }
            catch
            {
                return new List<SinhVien>();
            }
           
        } 
        //Hàm chi tiết
        public SinhVien ChiTiet(int id)
        {
            var tk = db.SinhViens.Find(id);
            if(tk == null)
            {
                message = "Không tìm thấy tài khoản của bạn! Vui lòng thử lại.";
                return null;
            }
            else
            {
                return tk;
            }
        }

        //Hàm thêm(đăng ký) tài khoản SV
        public SinhVien DangKy(SinhVien model)
        {
            if (model.TenDangNhap.Trim() == " ")
            {
                message = "Bạn chưa nhập tên tài khoản";
                return null;
            } 
            if ((model.MatKhau??"").Length <6 )
            {
                message = "Bạn chưa nhập mật khẩu";
                return null;
            }
            if (string.IsNullOrEmpty(model.HoVaTen)==true)
            {
                message = "Bạn chưa nhập họ tên";
                return null;
            } 
            if(db.SinhViens.Count(m=>m.TenDangNhap.ToLower().Trim() == model.TenDangNhap.ToLower().Trim()) >0)
            {
                message = "Tên đăng nhập đã tồn tại. Vui lòng nhập tên khác.";
                return null;
            }
            try
            {
                model.MatKhau = new SV.Helper.Encrypt().CreateMD5(model.MatKhau);
                db.SinhViens.Add(model);
                db.SaveChanges();
                return model;
            }
            catch
            {
                message = "Lỗi hệ thống, vui lòng thử lại";
                return null;
            }
        }

        //Hàm cập nhật
        public bool CapNhat(SinhVien model)
        {
            var update = db.SinhViens.Find(model.ID);
            if(update == null)
            {
                message = "Không tìm thấy tải khoản của bạn! Vui lòng thử lại";
                return false;
            }
            else
            {
                try
                {
                    update.TenDangNhap = model.TenDangNhap; 
                    update.HoVaTen = model.HoVaTen;
                    update.Truong = model.Truong;
                    update.Lop = model.Lop;
                    update.Email = model.Email;
                    update.SoDienThoai = model.SoDienThoai;
                    update.NgaySinh = model.NgaySinh;
                    update.AnhDaiDien = model.AnhDaiDien;
                    db.SaveChanges();
                    message = "Cập nhật thành công";
                    return true;
                }
                catch
                {
                    message = "Cập nhật không thành công! Vui lòng thử lại.";
                    return false;
                }
            }
        }

        // Xóa
        public bool Xoa(int id)
        {
            var update = db.SinhViens.Find(id);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return false;
            }
            try
            {
                db.SinhViens.Remove(update);
                db.SaveChanges();
                return true;
            }
            catch
            {
                message = "Lỗi hệ thống, vui lòng thử lại";
                return false;
            }
        }
        public bool Khoa(int id)
        {
            var update = db.SinhViens.Find(id);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return false;
            }
            try
            {
                update.isKhoa = !(update.isKhoa??false);
                db.SaveChanges();
                return true;
            }
            catch
            {
                message = "Lỗi hệ thống, vui lòng thử lại";
                return false;
            }
        }
        public bool ResetMatKhau(int id)
        {
            var update = db.SinhViens.Find(id);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return false;
            }
            try
            {
                update.MatKhau = new SV.Helper.Encrypt().CreateMD5("123456");
                db.SaveChanges();
                return true;
            }
            catch
            {
                message = "Lỗi hệ thống, vui lòng thử lại";
                return false;
            }
        }

        // ĐĂng nhập tài khoản
        public SinhVien DangNhap(string tenDangNhap, string matKhau)
        {
            var tk = db.SinhViens.SingleOrDefault(m => m.TenDangNhap.ToLower() == tenDangNhap.ToLower() & m.MatKhau == matKhau);
            if (tk == null)
            {
                message = "Tên đăng nhập hoặc mật khẩu không đúng, vui lòng thử lại";
                return null;
            }
            else
            {

                return tk;
            }
        }

        public bool DoiMatKhau(int id, string matKhauCu, string matKhauMoi)
        {
            var user = db.SinhViens.Find(id);
            if(user == null)
            {
                message = "Tài khoản không tồn tại";
                return false;
            }
            matKhauCu = new SV.Helper.Encrypt().CreateMD5(matKhauCu ?? "");
            if(user.MatKhau!= matKhauCu)
            {
                message = "Mật khẩu cũ không đúng";
                return false;
            }
            if(matKhauMoi.Length < 6)
            {
                message = "Mật khẩu mới phải lớn hơn 6 ký tự";
                return false;
            }
            matKhauMoi = new SV.Helper.Encrypt().CreateMD5(matKhauMoi ?? "");

            user.MatKhau = matKhauMoi;

            try
            {
                db.SaveChanges();
                return true;
            }
            catch
            {
                message = "lỗi hệ thống, vui lòng thử lại";
                return false;
            } 
        }
    }
}