//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SV.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LienHeTuyenSinh
    {
        public int ID { get; set; }
        public int idBaiVietTuyenSinh { get; set; }
        public string HoTen { get; set; }
        public Nullable<System.DateTime> ThoiGianNhan { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string GhiChu { get; set; }
        public Nullable<bool> DaTraLoi { get; set; }
        public string QueQuan { get; set; }
    
        public virtual BaiVietTuyenSinh BaiVietTuyenSinh { get; set; }
    }
}
