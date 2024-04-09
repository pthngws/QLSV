namespace QLSV
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Score")]
    public partial class Scr
    {
        [Key]
        [StringLength(50)]
        public string studentID { get; set; }

        [StringLength(50)]
        public string courseID { get; set; }

        public double? student_score { get; set; }

        [Column(TypeName = "text")]
        public string description { get; set; }
    }
}
