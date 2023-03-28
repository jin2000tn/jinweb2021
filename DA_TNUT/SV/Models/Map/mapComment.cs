using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV.Models;

namespace SV.Models.Map
{
    public class mapComment
    {
        Entities db = new Entities();
        public string message = "";

        public List<Comment> DanhSach()
        {
            try
            {
                return db.Comments.ToList();
            }
            catch
            {
                return new List<Comment>();
            }
        }
        public List<Comment> DanhSach(int idBaiDang)
        {
            try
            {
                return db.Comments.Where(m=>m.idBaiDang== idBaiDang).OrderByDescending(m=>m.ID).ToList();
            }
            catch
            {
                return new List<Comment>();
            }
        }

        public Comment ThemMoi(Comment model)
        {
            if (db.SinhViens.Find(model.idTaiKhoan) == null)
            {
                message = "Chưa đăng nhập tài khoản. Vui lòng đăng nhập hoặc đăng ký tài khoản.";
                return null;
            }
            if (db.BaiDangs.Find(model.idBaiDang) == null)
            {
                message = "";
                return null;
            }
            if (string.IsNullOrEmpty(model.NoiDung) == true)
            {
                message = "Bạn chưa nhập nội dung";
                return null;
            }
            try
            {
                db.Comments.Add(model);
                db.SaveChanges();
                return model;
            }
            catch
            {
                message = "Lỗi hệ thống, vui lòng thử lại";
                return null;
            }
        }
    }
}