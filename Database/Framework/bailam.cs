namespace Database.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("bailam")]
    public partial class bailam
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string SoHieu { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDe { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaCH { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(5)]
        public string NamThi { get; set; }

        [StringLength(5)]
        public string DA { get; set; }

        public virtual dethi dethi { get; set; }

        public virtual thisinh thisinh { get; set; }
    }
}
