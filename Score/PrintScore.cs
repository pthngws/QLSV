using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class PrintScore : Form
    {
        public PrintScore()
        {
            InitializeComponent();
        }
        Score score = new Score();

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand(@"SELECT course.label AS 'Tên môn học', 
       std.id AS 'MSSV', 
       std.fname AS 'Họ', 
       std.lname AS 'Tên', 
       score.student_score AS 'Điểm', 
       score.description AS 'Ghi chú' 
FROM score, std, course 
WHERE course.id = score.courseid 
      AND score.studentid = std.id
      AND (CONVERT(NVARCHAR(MAX), std.id) + CONVERT(NVARCHAR(MAX), course.label)) LIKE '%" + textBoxSearch.Text + "%'");

            dataGridView1.DataSource = score.getScore(cmd);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            printDialog1 = new PrintDialog();
            printDocument1.DocumentName = " Print Document";
            printDialog1.Document = printDocument1;
            printDialog1.AllowSelection = true;
            printDialog1.AllowSomePages = true;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void PrintScore_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand(@"SELECT course.label AS 'Tên môn học', 
       std.id AS 'MSSV', 
       std.fname AS 'Họ', 
       std.lname AS 'Tên', 
       score.student_score AS 'Điểm', 
       score.description AS 'Ghi chú' 
FROM score, std, course 
WHERE course.id = score.courseid 
      AND score.studentid = std.id
     ");

            dataGridView1.DataSource = score.getScore(cmd);
        }
    }
    
}

