using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QLSV
{
    public partial class CrsContext : DbContext
    {
        public CrsContext()
            : base("name=CrsContext")
        {
        }

        public virtual DbSet<Crs> Crs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crs>()
                .Property(e => e.description)
                .IsUnicode(false);
        }
    }
}
