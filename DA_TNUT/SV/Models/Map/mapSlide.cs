using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV.Models.Map
{
    public class mapSlide
    {
        public Entities db = new Entities();
        public string message = "";

        // Hàm danh sách
        public List<Slide> DanhSach()
        {
            try
            {
                return db.Slides.OrderByDescending(m=>m.ID).ToList();
            }
            catch
            {
                return new List<Slide>();
            }
        }  
        public Slide DanhSach(string loai)
        {
            try
            {
                 var data  = db.Slides.Where(m=>m.LoaiSlide == loai & m.isActive == true & m.TuNgay <= DateTime.Now & DateTime.Now <= m.DenNgay).ToList();
                if (data.Count() > 0)
                {
                    return data.FirstOrDefault();
                }
                else
                {
                    return new Slide();
                }
            }
            catch
            {
                return new Slide();
            }
        }





        // Chi tiết
        public Slide ChiTiet(int id)
        {
            try
            {
                return db.Slides.Find(id);
            }
            catch
            {
                return new Slide();
            }
        }


        // Thêm mới: ok -> id, false: 0
        public int ThemMoi(Slide model)
        {
            
            try
            {
                db.Slides.Add(model);
                db.SaveChanges();
                return model.ID;
            }
            catch(Exception ex)
            {
                message = "Lỗi hệ thống, vui lòng thử lại";
                return 0;
            }
        }

        // Cập nhật
        public int CapNhat(Slide model)
        {
            var update = db.Slides.Find(model.ID);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return 0;
            }
            
            try
            {
                update.TenSlide = model.TenSlide;
                update.MoTa = model.MoTa;
                update.TuNgay = model.TuNgay;
                update.isActive = model.isActive;
                update.DenNgay = model.DenNgay;
                update.LoaiSlide = model.LoaiSlide;
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
            var update = db.Slides.Find(id);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return false;
            }
            try
            {
                db.Slides.Remove(update);
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