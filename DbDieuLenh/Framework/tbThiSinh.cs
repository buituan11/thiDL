namespace DbDieuLenh.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tbThiSinh")]
    public partial class tbThiSinh
    {
        [StringLength(50)]
        public string id { get; set; }

        [StringLength(50)]
        public string HoTen { get; set; }

        [StringLength(50)]
        public string Lop { get; set; }

        [StringLength(50)]
        public string ChuyenKhoa { get; set; }
    }
}
