using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
namespace SV.Models.Map
{
    public class mapCauHinh
    {
        Entities db = new Entities();
        public string message = "";
        public List<CauHinh> DanhSach(string nhom)
        {
            var ch = db.CauHinhs.Where(m=>m.NhomThietLap == nhom).ToList();
            try
            {
                return ch;
            }
            catch
            {
                return new List<CauHinh>();
            }
        }
        public string GiaTri(string MaCauHinh)
        {
            var ch = db.CauHinhs.Find(MaCauHinh);
            if (ch == null)
            {
                return "";
            }
            else { return ch.GiaTri; }
        }
        public CauHinh ChiTiet(string MaCauHinh)
        {
            var ch = db.CauHinhs.Find(MaCauHinh);
            if (ch == null)
            {
                return new CauHinh();
            }
            else { return ch; }
        }
    }
}