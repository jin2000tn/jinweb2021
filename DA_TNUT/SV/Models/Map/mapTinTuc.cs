using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV.Models;

namespace SV.Models.Map
{
    public class mapTinTuc
    {
        public Entities db = new Entities();
        public string message = "";

        // Hàm danh sách
        public List<TinTuc>DanhSach()
        {
            try
            {
                return db.TinTucs.ToList();
            }
            catch
            {
                return new List<TinTuc>();
            }
        }
        public List<TinTuc> DanhSach(int page)
        {
            try
            {
                return db.TinTucs.OrderByDescending(m => m.ThoiGian).Skip((page-1)*8).Take(8).ToList();
            }
            catch
            {
                return new List<TinTuc>();
            }
        }
        public List<TinTuc> DanhSachTinTuc()
        {
            try
            {
                return db.TinTucs.Where(m=>m.isSuKien !=true).OrderByDescending(m=>m.ID).ToList();
            }
            catch
            {
                return new List<TinTuc>();
            }
        }
        public List<TinTuc> DanhSachSuKien()
        {
            try
            {
                return db.TinTucs.Where(m=>m.isSuKien ==true).OrderByDescending(m => m.ID).ToList();
            }
            catch
            {
                return new List<TinTuc>();
            }
        }
        public List<TinTuc> DanhSachTrangChu()
        {
            try
            {
                return db.TinTucs.OrderByDescending(m=>m.ThoiGian).Take(4).ToList();
            }
            catch
            {
                return new List<TinTuc>();
            }
        }
        public List<TinTuc> TinTrongThang(int nam, int thang)
        {
            try
            {
                return db.TinTucs.Where(m=>m.ThoiGian.Value.Year == nam & m.ThoiGian.Value.Month == thang).ToList();
            }
            catch
            {
                return new List<TinTuc>();
            }
        }
        public int SoTinTrongThang(int nam, int thang)
        {
            try
            {
                return db.TinTucs.Count(m => m.ThoiGian.Value.Year == nam & m.ThoiGian.Value.Month == thang);
            }
            catch
            {
                return 0;
            }
        }

        // Chi tiết
        public TinTuc ChiTiet(int id)
        {
            try
            {
                return db.TinTucs.Find(id);
            }
            catch
            {
                return new TinTuc();
            }
        }
        public TinTuc ChiTiet(string link)
        {
            try
            {
                return db.TinTucs.SingleOrDefault(m=>m.LinkSeo.ToLower() == link.ToLower());
            }
            catch
            {
                return new TinTuc();
            }
        }


        // Thêm mới: ok -> id, false: 0
        public int ThemMoi(TinTuc model)
        {
            if (string.IsNullOrEmpty(model.TieuDe) == true)
            {
                message = "Nhập thiếu tên trang";
                return 0;
            }
            try
            {
                model.LuotXem = 0;
                db.TinTucs.Add(model);
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
        public int CapNhat(TinTuc model)
        {
            var update = db.TinTucs.Find(model.ID);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return 0;
            }
            if (string.IsNullOrEmpty(model.TieuDe) == true)
            {
                message = "Nhập thiếu tên trang";
                return 0;
            }
            try
            {
                update.TieuDe = model.TieuDe;
                update.HinhAnh = model.HinhAnh;
                update.TacGia = model.TacGia;
                update.ThoiGian = model.ThoiGian;
                update.LuotXem = model.LuotXem;
                update.LinkSeo = model.LinkSeo;
                update.TomTat = model.TomTat;              
                update.NoiDung = model.NoiDung;
                update.isSuKien = model.isSuKien;
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
            var update = db.TinTucs.Find(id);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return false;
            }
            try
            {
                db.TinTucs.Remove(update);
                db.SaveChanges();
                return true;
            }
            catch
            {
                message = "Lỗi hệ thống, vui lòng thử lại";
                return false;
            }
        }
        public List<TinTuc> Top5()
        {
            try
            {
                var Top5 = (from item in db.TinTucs
                            orderby item.LuotXem descending
                            select item).Take(5).ToList();

                return Top5;
            }
            catch
            {
                return new List<TinTuc>();
            }
        }
            public List<TinTuc> TimKiem(string TieuDe)
            {
            try{
                return (from item in db.TinTucs
                            where item.TieuDe.ToLower().Contains(TieuDe.ToLower()) == true | string.IsNullOrEmpty(TieuDe)
                            select item).ToList();
                }
                catch
                {
                    return new List<TinTuc>();
                }
            }
        }
    }

