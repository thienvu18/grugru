//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GruGru.Model
{
    using System;
    using System.Collections.Generic;

    public partial class DanhSach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DanhSach()
        {
            this.HoaDons = new HashSet<HoaDon>();
        }

        public int stt { get; set; }
        public string maHoaDon { get; set; }
        public string nhanVien { get; set; }
        public string khachHang { get; set; }
        public string thoiGian { get; set; }
        public decimal gia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
