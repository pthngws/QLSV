using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace QLSV
{
    public partial class ReportAvg : Form
    {
        DataSet dataset;

        public ReportAvg(DataSet dataSet)
        {
            InitializeComponent();
            this.dataset = dataSet;
        }

        private void ReportAvg_Load(object sender, EventArgs e)
        {
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables["AVGScore"].Rows.Count > 0)
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "QLSV.Report2.rdlc";

                // Xác định nguồn dữ liệu cho báo cáo
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "DataSet1"; // Tên DataSet trong file .rdlc
                rds.Value = dataset.Tables["AVGScore"]; // Giả sử dữ liệu của bạn được lưu trong table đầu tiên của DataSet

                // Đặt nguồn dữ liệu cho reportViewer
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(rds);

                // Cập nhật và hiển thị báo cáo
                this.reportViewer1.RefreshReport();
            }
            else
            {
                MessageBox.Show("DataSet không có dữ liệu hoặc dữ liệu trống.");
            }
        }
    }
}
