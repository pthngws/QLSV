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
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            StdContext context = new StdContext();
            List<std> listStudent = context.std.ToList();
            List<STUDENT> listReport = new List<STUDENT>();
            foreach(std sv in listStudent)
            {
                STUDENT tmp = new STUDENT();
                tmp.Id = sv.id;
                tmp.Fname = sv.fname;
                tmp.Lname = sv.lname;
                tmp.date = sv.bdate;
                tmp.Gender = sv.gender;
                tmp.Phone = sv.phone;

                tmp.Address = sv.address;
                listReport.Add(tmp);
            }
            reportViewer1.LocalReport.ReportPath = "ReportStd.rdlc";
            var source = new ReportDataSource("DataSetSTD", listReport);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(source);
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
