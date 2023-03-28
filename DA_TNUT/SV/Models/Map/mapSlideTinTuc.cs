using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV.Models;
namespace SV.Models.Map
{
    public class mapSlide_TinTuc
    {
        public Entities db = new Entities();
        public string message = "";

        // Hàm danh sách
        public List<Slide_TinTuc> DanhSach()
        {
            try
            {
                return db.Slide_TinTuc.ToList();
            }
            catch
            {
                return new List<Slide_TinTuc>();
            }
        }
        public List<Slide_TinTuc> DanhSach(int ID)
        {
            try
            {
                return db.Slide_TinTuc.Where(m => m.ID == ID).ToList();
            }
            catch
            {
                return new List<Slide_TinTuc>();
            }
        }

        // Chi tiết
        public Slide_TinTuc ChiTiet(int id)
        {
            try
            {
                return db.Slide_TinTuc.Find(id);
            }
            catch
            {
                return new Slide_TinTuc();
            }
        }


        // Thêm mới: ok -> id, false: 0
        public int ThemMoi(Slide_TinTuc model)
        {

            try
            {
                db.Slide_TinTuc.Add(model);
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
        public int CapNhat(Slide_TinTuc model)
        {
            var update = db.Slide_TinTuc.Find(model.ID);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return 0;
            }

            try
            {
                update.LienKet = model.LienKet;
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
            var update = db.Slide_TinTuc.Find(id);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return false;
            }
            try
            {
                db.Slide_TinTuc.Remove(update);
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