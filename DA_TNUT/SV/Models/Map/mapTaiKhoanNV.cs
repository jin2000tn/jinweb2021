using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV.Models;

namespace SV.Models.Map
{
    public class mapTaiKhoanNV
    {
        Entities db = new Entities();
        public string message = "";

        public List<TaiKhoanNV> DanhSach()
        {
            try
            {
                return db.TaiKhoanNVs.ToList();
            }
            catch
            {
                return new List<TaiKhoanNV>();
            }
        }

        public TaiKhoanNV DangKy(TaiKhoanNV model)
        {
            if(string.IsNullOrEmpty(model.Username) == true)
            {
                message = "Bạn chưa nhập tài khoản";
                return null;
            }
            if ((model.Password ?? "").Length < 6)
            {
                message = "Bạn chưa nhập mật khẩu.";
                return null;
            }
            if(string.IsNullOrEmpty(model.HoVaTen) == true)
            {
                message = "Bạn chưa nhập họ và tên.";
                return null;
            }
            if(db.TaiKhoanNVs.Count(m=>m.Username.ToLower().Trim() == model.Username.ToLower().Trim()) > 0)
            {
                message = "Tên đăng nhập đã tồn tại. Vui lòng nhập tên khác.";
                return null;
            }
            try
            {
                //model.Password = new SV.Helper.Encrypt().CreateMD5(model.Password);
                db.TaiKhoanNVs.Add(model);
                db.SaveChanges();
                return model;
            }
            catch
            {
                message = "Lỗi hệ thống! Vui lòng thử lại.";
                return null;
            }
        }
         public TaiKhoanNV CapNhat(TaiKhoanNV model)
        {
            var update = db.TaiKhoanNVs.Find(model.ID);
            if (update == null)
            {
                message = "Tài khoản không tồn tại";
                return null;
            }
            if (string.IsNullOrEmpty(model.Username) == true)
            {
                message = "Bạn chưa nhập tên đăng nhập";
                return null;
            }
            if (db.TaiKhoanNVs.Count(m => m.Username.ToLower().Trim() == model.Username.ToLower().Trim() & m.ID != model.ID) > 0)
            {
                message = "Tên đăng nhập đã tồn tại. Vui lòng nhập tên khác.";
                return null;
            }
            if (string.IsNullOrEmpty(model.HoVaTen) == true)
            {
                message = "Bạn chưa nhập họ và tên.";
                return null;
            } 
            try
            {
                update.HoVaTen = model.HoVaTen;
                update.Username = model.Username;
                update. Email = model.Email;   
                db.SaveChanges();
                return model;
            }
            catch
            {
                message = "Lỗi hệ thống! Vui lòng thử lại.";
                return null;
            }
        }

        public TaiKhoanNV DangNhap(string tenTaiKhoan, string matKhau)
        {
            var tk = db.TaiKhoanNVs.SingleOrDefault(m => m.Username.ToLower() == tenTaiKhoan.ToLower() & m.Password == matKhau);
            if (tk == null)
            {
                message = "Tên đăng nhập hoặc mật khẩu không đúng. Vui lòng nhập lại.";
                return null;
            }
            else
            {
                return tk;
            }
        }

        public TaiKhoanNV ChiTiet(string Username)
        {
            var tk = db.TaiKhoanNVs.SingleOrDefault(m => m.Username.ToLower() == Username.ToLower());
            if (tk == null)
            {
                message = "Tên đăng nhập hoặc mật khẩu không đúng. Vui lòng nhập lại.";
                return null;
            }
            else
            {
                return tk;
            }
        }
        public TaiKhoanNV ChiTiet(int  id)
        {
            var tk = db.TaiKhoanNVs.Find(id);
            if (tk == null)
            {
                message = "Tên đăng nhập hoặc mật khẩu không đúng. Vui lòng nhập lại.";
                return null;
            }
            else
            {
                return tk;
            }
        }

        public bool Khoa(int id)
        {
            var update = db.TaiKhoanNVs.Find(id);
            if (update == null)
            {
                message = "Không tìm thấy tài khoản";
                return false;
            }
            try
            {
                update.isKhoa = !(update.isKhoa ?? false);
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
            var update = db.TaiKhoanNVs.Find(id);
            if (update == null)
            {
                message = "Không tìm thấy tài khoản";
                return false;
            }
            try
            {
                update.Password = new SV.Helper.Encrypt().CreateMD5("123456");
                db.SaveChanges();
                return true;
            }
            catch
            {
                message = "Lỗi hệ thống, vui lòng thử lại";
                return false;
            }
        }

        public bool DoiMatKhau(int id, string newPass, string oldPass)
        {
            var update = db.TaiKhoanNVs.Find(id);
            if (update == null)
            {
                message = "Không tìm thấy tài khoản";
                return false;
            }
            if ((newPass ?? "").Length < 6)
            {
                message = "Vui lòng nhập mật khẩu mới dài hơn 6 ký tự";
                return false;
            }
            if (update.Password != new SV.Helper.Encrypt().CreateMD5(oldPass))
            {
                message = "Mật khẩu cũ không đúng";
                return false;
            }
            try
            { 
                update.Password = new SV.Helper.Encrypt().CreateMD5(newPass);
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