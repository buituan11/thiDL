namespace Models.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cauhoi")]
    public partial class cauhoi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cauhoi()
        {
            dethis = new HashSet<dethi>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaCH { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string NamHoc { get; set; }

        [StringLength(1000)]
        public string NoiDung { get; set; }

        [StringLength(1000)]
        public string DA_A { get; set; }

        [StringLength(1000)]
        public string DA_B { get; set; }

        [StringLength(1000)]
        public string DA_C { get; set; }

        [StringLength(1000)]
        public string DA_D { get; set; }

        [StringLength(5)]
        public string DA { get; set; }

        public int? Loai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dethi> dethis { get; set; }
    }
}
