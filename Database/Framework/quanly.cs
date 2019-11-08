namespace Database.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("quanly")]
    public partial class quanly
    {
        [Key]
        public int MaQL { get; set; }

        [StringLength(100)]
        public string TK { get; set; }

        [StringLength(100)]
        public string MK { get; set; }
    }
}
