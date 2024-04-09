namespace QLSV
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("std")]
    public partial class std
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public string fname { get; set; }

        public string lname { get; set; }

        public DateTime bdate { get; set; }

        [StringLength(10)]
        public string gender { get; set; }

        [StringLength(10)]
        public string phone { get; set; }

        [StringLength(20)]
        public string address { get; set; }

        [Column(TypeName = "image")]
        public byte[] picture { get; set; }

        [StringLength(50)]
        public string selected_course { get; set; }
    }
}
