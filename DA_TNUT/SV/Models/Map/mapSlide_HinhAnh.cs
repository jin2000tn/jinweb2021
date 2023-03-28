using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV.Models;
namespace SV.Models.Map
{
    public class mapSlide_HinhAnh
    {
        public Entities db = new Entities();
        public string message = "";

        // Hàm danh sách
        public List<Slide_HinhAnh> DanhSach()
        {
            try
            {
                return db.Slide_HinhAnh.ToList();
            }
            catch
            {
                return new List<Slide_HinhAnh>();
            }
        } 
        public List<Slide_HinhAnh> DanhSach(int idSlide)
        {
            try
            {
                return db.Slide_HinhAnh.Where(m=>m.idSlide == idSlide).ToList();
            }
            catch
            {
                return new List<Slide_HinhAnh>();
            }
        }

        // Chi tiết
        public Slide_HinhAnh ChiTiet(int id)
        {
            try
            {
                return db.Slide_HinhAnh.Find(id);
            }
            catch
            {
                return new Slide_HinhAnh();
            }
        }


        // Thêm mới: ok -> id, false: 0
        public int ThemMoi(Slide_HinhAnh model)
        {

            try
            {
                db.Slide_HinhAnh.Add(model);
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
        public int CapNhat(Slide_HinhAnh model)
        {
            var update = db.Slide_HinhAnh.Find(model.ID);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return 0;
            }

            try
            {
                update.idSlide = model.idSlide;
                update.TenHinhAnh = model.TenHinhAnh;
                update.MoTa= model.MoTa;
                update.LienKet = model.LienKet;
                update.ThuTu = model.ThuTu;
                update.HinhAnh = model.HinhAnh;
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
            var update = db.Slide_HinhAnh.Find(id);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return false;
            }
            try
            {
                db.Slide_HinhAnh.Remove(update);
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