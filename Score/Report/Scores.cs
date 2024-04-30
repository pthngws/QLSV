using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV
{
    internal class Scores
    {
        public int studentID { get; set; }


        public string courseID { get; set; }

        public double? student_score { get; set; }

        public string description { get; set; }
    }
}
