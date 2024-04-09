using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QLSV
{
    public partial class StdContext : DbContext
    {
        public StdContext()
            : base("name=StdContext")
        {
        }

        public virtual DbSet<std> std { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
