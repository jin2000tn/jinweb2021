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
    
    public partial class TaiLieu
    {
        public int ID { get; set; }
        public string TenTaiLieu { get; set; }
        public string ThongTin { get; set; }
        public string DuongDanFile { get; set; }
        public Nullable<System.DateTime> ThoiGianUpload { get; set; }
        public string NguoiUpload { get; set; }
        public Nullable<bool> TrangThaiPheDuyet { get; set; }
        public string NhomTaiLieu { get; set; }
    }
}
