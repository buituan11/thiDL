namespace DbDieuLenh.Framework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DLDbContext : DbContext
    {
        public DLDbContext()
            : base("name=DLDbContext")
        {
        }

        public virtual DbSet<tbQuanLy> tbQuanLies { get; set; }
        public virtual DbSet<tbThiSinh> tbThiSinhs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
