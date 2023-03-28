using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV.Models;

namespace SV.Models.Map
{
    public class mapChuongTrinhDaoTao
    {
        public Entities db = new Entities();
        public string message = "";

        // Hàm danh sách
        public List<ChuongTrinhDaoTao> DanhSach()
        {
            try
            {
                return db.ChuongTrinhDaoTaos.ToList();
            }
            catch
            {
                return new List<ChuongTrinhDaoTao>();
            }
        }

        // Chi tiết
        public ChuongTrinhDaoTao ChiTiet(int id)
        {
            try
            {
                return db.ChuongTrinhDaoTaos.Find(id);
            }
            catch
            {
                return new ChuongTrinhDaoTao();
            }
        }


        // Thêm mới: ok -> id, false: 0
        public int ThemMoi(ChuongTrinhDaoTao model)
        {
            if (string.IsNullOrEmpty(model.TenChuongTrinh) == true)
            {
                message = "Nhập thiếu tên bài viết";
                return 0;
            }
            try
            {
                db.ChuongTrinhDaoTaos.Add(model);
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
        public int CapNhat(ChuongTrinhDaoTao model)
        {
            var update = db.ChuongTrinhDaoTaos.Find(model.ID);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return 0;
            }
            if (string.IsNullOrEmpty(model.TenChuongTrinh) == true)
            {
                message = "Nhập thiếu tên bài viết";
                return 0;
            }
            try
            {
                update.Icon = model.Icon;
                update.TenChuongTrinh = model.TenChuongTrinh;
                update.MoTa = model.MoTa;
                update.NoiDung = model.NoiDung;
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
            var update = db.ChuongTrinhDaoTaos.Find(id);
            if (update == null)
            {
                message = "Không tìm thấy đối tượng";
                return false;
            }
            try
            {
                db.ChuongTrinhDaoTaos.Remove(update);
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