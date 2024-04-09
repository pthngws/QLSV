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
    public partial class ReportCrsForm : Form
    {
        public ReportCrsForm()
        {
            InitializeComponent();
        }

        private void ReportCrsForm_Load(object sender, EventArgs e)
        {
            CrsContext context = new CrsContext();
            List<Crs> listCourse = context.Crs.ToList();
            List<COURSEE> listReport = new List<COURSEE>();
            foreach (Crs c in listCourse)
            {
                COURSEE tmp = new COURSEE();
                tmp.Id = c.Id;
                tmp.Label = c.label;
                tmp.Period = c.period;
                tmp.Description = c.description;
                tmp.Semester = c.semester;

                listReport.Add(tmp);
            }
            reportViewer1.LocalReport.ReportPath = "C:\\Users\\thang\\Source\\Repos\\pthngws\\QLSV\\Course\\Report\\ReportCrs.rdlc";
            var source = new ReportDataSource("DataSetCRS", listReport);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(source);
            this.reportViewer1.RefreshReport();
        }
    }
}
