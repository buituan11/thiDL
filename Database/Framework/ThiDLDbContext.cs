namespace Database.Framework
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

        
        
        
        public virtual DbSet<quanly> quanlies { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        

        internal object SqlQuery<T>(string v, object[] sqlPara)
        {
            throw new NotImplementedException();
        }

        internal object SqlQuery()
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<quanly>()
                .Property(e => e.TK)
                .IsUnicode(false);

            modelBuilder.Entity<quanly>()
                .Property(e => e.MK)
                .IsUnicode(false);
        }
    }
}
