using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV.Models;

namespace SV.Models.Map
{
    public class mapPhanQuyen
    {
        Entities db = new Entities();
        public string message = "";
        public List<string> DanhSachNhomChucNang()
        {
            return db.ChucNangs.Select(m => m.Nhom).Distinct().ToList();
        }
        public List<ChucNang> DanhSachChucNang()
        {
            return db.ChucNangs.ToList();
        }

        public List<PhanQuyen> ThongTinPhanQuyen(int taiKhoan)
        {
            return db.PhanQuyens.Where(m => m.idTaiKhoan == taiKhoan).ToList();
        }

        public bool KiemTraQuyen(int taiKhoan, string maChucNang)
        {
            if (db.PhanQuyens.Count(m => m.idTaiKhoan == taiKhoan & m.maChucNang == maChucNang) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CapNhatQuyen(int idTaiKhoan, string maChucNang, bool check)
        {
            var quyen = db.PhanQuyens.SingleOrDefault(m => m.idTaiKhoan == idTaiKhoan & m.maChucNang == maChucNang);
            if (check == true)
            { 
                if (quyen == null)
                {
                    quyen = new PhanQuyen()
                    {
                        idTaiKhoan = idTaiKhoan,
                        maChucNang = maChucNang,
                        GhiChu = ""
                    };
                    db.PhanQuyens.Add(quyen);
                    db.SaveChanges();
                }
                message = "Thêm quyền thành công";
                return true;

            }
            else
            {
                if (quyen != null)
                { 
                    db.PhanQuyens.Remove(quyen);
                    db.SaveChanges();
                }
                message = "Xóa quyền thành công";
                return true;
            }
        }
    }
}