namespace Database.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("thisinh")]
    public partial class thisinh
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public thisinh()
        {
            bailams = new HashSet<bailam>();
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string SoHieu { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string NamThi { get; set; }

        [StringLength(20)]
        public string HoTen { get; set; }

        [StringLength(5)]
        public string Lop { get; set; }

        [StringLength(5)]
        public string ChuyenKhoa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bailam> bailams { get; set; }
    }
}
