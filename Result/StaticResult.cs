using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class StaticResult : Form
    {
        public StaticResult()
        {
            InitializeComponent();
        }
        MY_DB mydb = new MY_DB();
        private void StaticResult_Load(object sender, EventArgs e)
        {
            double total = Convert.ToDouble(totalStudent());
            double totalXuatSac = Convert.ToDouble(totalXS());
            double totalKha = Convert.ToDouble(totalK());
            double totalTrungBinh = Convert.ToDouble(totalTB());

            // Tính phần trăm học sinh nam và nữ
            double XSStudentsPercentage = Math.Round((totalXuatSac / total) * 100, 1);
            double KStudentsPercentage = Math.Round((totalKha / total) * 100, 1);
            double TBStudentsPercentage = Math.Round((totalTrungBinh / total) * 100, 1);
            double YStudentsPercentage = 100 - XSStudentsPercentage - KStudentsPercentage - TBStudentsPercentage;

            label1.Text = ("Excellent: " + XSStudentsPercentage.ToString("0.0") + "%");
            label2.Text = ("Good: " + KStudentsPercentage.ToString("0.0") + "%");
            label5.Text = ("Average: " + TBStudentsPercentage.ToString("0.0") + "%");
            label6.Text = ("Weak: " + YStudentsPercentage.ToString("0.0") + "%");
            ShowCourseLabels();
        }


        private void ShowCourseLabels()
        {
            // Lấy danh sách các courseID và avg score từ cơ sở dữ liệu
            Dictionary<string, double> courseAvgScores = GetCourseAvgScores();

            // Tạo và hiển thị label cho từng courseID và avg score
            int topMargin = 5; // Top margin cho label đầu tiên
            int labelHeight = 20; // Chiều cao của label
            int spacing = 5; // Khoảng cách giữa các label

            // Xóa các control cũ trong panel2 trước khi thêm mới
            panel2.Controls.Clear();

            foreach (var pair in courseAvgScores)
            {
                string courseID = pair.Key;
                double avgScore = pair.Value;

                Label lblCourseInfo = new Label();
                lblCourseInfo.Text = $"{courseID}: {avgScore.ToString("0.00")}";
                lblCourseInfo.AutoSize = true;

                // Đặt vị trí và kích thước của label
                lblCourseInfo.Left = 5; // Đặt left margin tùy ý
                lblCourseInfo.Top = topMargin;
                lblCourseInfo.Height = labelHeight;

                // Thêm label vào panel2
                panel2.Controls.Add(lblCourseInfo);

                // Cập nhật top margin cho label tiếp theo
                topMargin += labelHeight + spacing;
            }

            // Đặt lại kích thước của panel2 dựa trên tổng chiều cao của các label
            panel2.Height = topMargin;
        }



        private Dictionary<string, double> GetCourseAvgScores()
        {
            Dictionary<string, double> courseAvgScores = new Dictionary<string, double>();

            // Truy vấn cơ sở dữ liệu để lấy danh sách các courseID và avg score từ bảng score
            mydb.openConnection();
            string query = "SELECT courseid, label, SUM(student_score) AS total_score, \r\n    (SELECT COUNT(*) FROM std WHERE selected_course LIKE '%' + courseid + '%') AS std_count\r\nFROM score\r\nJOIN course ON score.courseid = course.id \r\nGROUP BY courseid, label";
            SqlCommand command = new SqlCommand(query, mydb.getConnection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string courseID = reader.GetString(0);
                string label = reader.GetString(1);
                double sumscore = reader.GetDouble(2);
                int student = reader.GetInt32(3);
                double avgScore = sumscore / student;
                courseAvgScores.Add(label, avgScore);
            }
            reader.Close();

            return courseAvgScores;
        }

        string exeCount(string query)
        {
            SqlCommand command = new SqlCommand(query, mydb.getConnection);
            mydb.openConnection();
            String count = command.ExecuteScalar().ToString();
            mydb.closeConnection();
            return count;
        }
        public string totalStudent()
        {
            return exeCount("Select count(*) from  std");
        }
        public string totalXS()
        {
            return exeCount("SELECT COUNT(*) FROM (\r\n    SELECT studentID\r\n    FROM score\r\n    GROUP BY studentID\r\n    HAVING AVG(student_score) >= 8\r\n) as AA");
        }
        public string totalK()
        {
            return exeCount("SELECT COUNT(*) FROM (\r\n    SELECT studentID\r\n    FROM score\r\n    GROUP BY studentID\r\n    HAVING AVG(student_score) >= 6.5\r\n) as AA");
        }
        public string totalTB()
        {
            return exeCount("SELECT COUNT(*) FROM (\r\n    SELECT studentID\r\n    FROM score\r\n    GROUP BY studentID\r\n    HAVING AVG(student_score) >= 5\r\n) as AA");
            
        }
    }
}
