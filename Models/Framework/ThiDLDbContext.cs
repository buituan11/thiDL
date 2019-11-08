namespace Models.Framework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ThiDLDbContext : DbContext
    {
        public ThiDLDbContext()
            : base("name=ThiDLDbContext")
        {
        }

        public virtual DbSet<bailam> bailams { get; set; }
        public virtual DbSet<cauhoi> cauhois { get; set; }
        public virtual DbSet<dethi> dethis { get; set; }
        public virtual DbSet<quanly> quanlies { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<thisinh> thisinhs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<bailam>()
                .Property(e => e.SoHieu)
                .IsUnicode(false);

            modelBuilder.Entity<bailam>()
                .Property(e => e.NamThi)
                .IsUnicode(false);

            modelBuilder.Entity<bailam>()
                .Property(e => e.DA)
                .IsUnicode(false);

            modelBuilder.Entity<cauhoi>()
                .Property(e => e.NamHoc)
                .IsUnicode(false);

            modelBuilder.Entity<cauhoi>()
                .Property(e => e.NoiDung)
                .IsUnicode(false);

            modelBuilder.Entity<cauhoi>()
                .Property(e => e.DA_A)
                .IsUnicode(false);

            modelBuilder.Entity<cauhoi>()
                .Property(e => e.DA_B)
                .IsUnicode(false);

            modelBuilder.Entity<cauhoi>()
                .Property(e => e.DA_C)
                .IsUnicode(false);

            modelBuilder.Entity<cauhoi>()
                .Property(e => e.DA_D)
                .IsUnicode(false);

            modelBuilder.Entity<cauhoi>()
                .Property(e => e.DA)
                .IsUnicode(false);

            modelBuilder.Entity<cauhoi>()
                .HasMany(e => e.dethis)
                .WithRequired(e => e.cauhoi)
                .HasForeignKey(e => new { e.MaCH, e.NamHoc })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<dethi>()
                .Property(e => e.NamHoc)
                .IsUnicode(false);

            modelBuilder.Entity<dethi>()
                .HasMany(e => e.bailams)
                .WithRequired(e => e.dethi)
                .HasForeignKey(e => new { e.MaDe, e.MaCH, e.NamThi })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<quanly>()
                .Property(e => e.TK)
                .IsUnicode(false);

            modelBuilder.Entity<quanly>()
                .Property(e => e.MK)
                .IsUnicode(false);

            modelBuilder.Entity<thisinh>()
                .Property(e => e.SoHieu)
                .IsUnicode(false);

            modelBuilder.Entity<thisinh>()
                .Property(e => e.NamThi)
                .IsUnicode(false);

            modelBuilder.Entity<thisinh>()
                .Property(e => e.HoTen)
                .IsUnicode(false);

            modelBuilder.Entity<thisinh>()
                .Property(e => e.Lop)
                .IsUnicode(false);

            modelBuilder.Entity<thisinh>()
                .Property(e => e.ChuyenKhoa)
                .IsUnicode(false);

            modelBuilder.Entity<thisinh>()
                .HasMany(e => e.bailams)
                .WithRequired(e => e.thisinh)
                .HasForeignKey(e => new { e.SoHieu, e.NamThi })
                .WillCascadeOnDelete(false);
        }
    }
}
