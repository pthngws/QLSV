using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QLSV
{
    public partial class SCRContext : DbContext
    {
        public SCRContext()
            : base("name=SCRContext")
        {
        }

        public virtual DbSet<Scr> Scr { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Scr>()
                .Property(e => e.description)
                .IsUnicode(false);
        }
    }
}
