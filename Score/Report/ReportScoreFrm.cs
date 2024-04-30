using Microsoft.Reporting.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                // Khởi tạo đối tượng context
                using (SCRContext context = new SCRContext())
                {
                    // Lấy danh sách điểm từ database
                     List <Scr> listScore = context.Scr.ToList();

                    // Tạo danh sách chứa dữ liệu cho báo cáo
                    List<Scores> listReport = new List<Scores>();

                    // Duyệt qua danh sách điểm và thêm vào danh sách báo cáo
                    foreach (Scr sv in listScore)
                    {
                        Scores tmp = new Scores();
                        tmp.studentID = sv.studentID;
                        tmp.courseID = sv.courseID;
                        tmp.student_score = sv.student_score;
                        tmp.description = sv.description;
                        listReport.Add(tmp);
                    }

                    // Đặt đường dẫn cho tệp báo cáo
                    reportViewer1.LocalReport.ReportPath = "C:\\Users\\thang\\Source\\Repos\\pthngws\\QLSV\\Score\\Report\\ReportScr.rdlc";

                    // Tạo nguồn dữ liệu báo cáo
                    var source = new ReportDataSource("DataSetSCR", listReport);

                    // Xóa các nguồn dữ liệu cũ và thêm nguồn dữ liệu mới
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(source);

                    // Làm mới báo cáo
                    this.reportViewer1.RefreshReport();
                }
            
        }
    }
}
