using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace QLSV
{
    public partial class ScoreOfStudentRP : Form
    {
        MY_DB mydb = new MY_DB();
        public ScoreOfStudentRP()
        {
            InitializeComponent();
        }
        string id;
        public ScoreOfStudentRP(string id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void ScoreOfStudentRP_Load(object sender, EventArgs e)
        {
            string sql = @"SELECT std.fname, std.lname, std.bdate, Course.Id, Course.label, Score.student_score
                   FROM std
                   INNER JOIN score ON std.id = score.studentID
                   INNER JOIN course ON Course.id = score.courseID
                   WHERE std.id = @StudentId";

            SqlDataAdapter adapter = new SqlDataAdapter(sql, mydb.getConnection);
            adapter.SelectCommand.Parameters.AddWithValue("@StudentId", id); // Sử dụng tham số thay vì giá trị cứng '22110424'

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "DiemSo");

            this.reportViewer1.LocalReport.ReportEmbeddedResource = "QLSV.Report1.rdlc";

            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = dataSet.Tables["DiemSo"];

            this.reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.RefreshReport();
        }
    }
}
