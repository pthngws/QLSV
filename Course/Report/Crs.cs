namespace QLSV
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Crs
    {
        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(50)]
        public string label { get; set; }

        public int? period { get; set; }

        [Column(TypeName = "text")]
        public string description { get; set; }

        [StringLength(50)]
        public string semester { get; set; }
    }
}
