using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class ReportScoreFrm : Form
    {
        public ReportScoreFrm()
        {
            InitializeComponent();
        }

        private void ReportScoreFrm_Load(object sender, EventArgs e)
        {
            SCRContext context = new SCRContext();
            List<Scr> listScore = context.Scr.ToList();
            List<Scores> listReport = new List<Scores>();
            foreach (Scr sv in listScore)
            {
                Scores tmp = new Scores();
                tmp.studentID = sv.studentID;
                tmp.courseID = sv.courseID;
                tmp.student_score = sv.student_score;
                tmp.description = sv.description;
                listReport.Add(tmp);
            }
            reportViewer1.LocalReport.ReportPath = "C:\\Users\\thang\\Source\\Repos\\pthngws\\QLSV\\Score\\Report\\ReportScr.rdlc";
            var source = new ReportDataSource("DataSetSCR", listReport);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(source);
            this.reportViewer1.RefreshReport();
        }
    }
}
