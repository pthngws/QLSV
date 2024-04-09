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

namespace QLSV.Student
{
    public partial class Selected_CourseForm : Form
    {
        public Selected_CourseForm()
        {
            InitializeComponent();
        }
        string id;
        List<string> courses;
        public Selected_CourseForm(string id)
        {
            InitializeComponent();
            this.id = id;

        }
        STUDENT student = new STUDENT();
        MY_DB mydb = new MY_DB();
private void Selected_CourseForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = id;
            // Tạo một DataTable mới
            DataTable dataTable = new DataTable();

            // Định nghĩa các cột cho DataTable
            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            dataTable.Columns.Add("semester", typeof(string));
            // Thêm các cột khác nếu cần thiết

            // Lấy danh sách các khóa học đã chọn của sinh viên từ phương thức selectedCourseStudent
            courses = student.selectedCourseStudent(id);

            foreach (string courseId in courses)
            {
                // Tạo một hàng mới cho DataTable
                DataRow row = dataTable.NewRow();

                // Truy vấn cơ sở dữ liệu để lấy thông tin về khóa học
                string courseName = ""; // Tên của khóa học
                string courseDescription = ""; // Mô tả của khóa học
                string semester = "";

                // Thực hiện truy vấn để lấy thông tin từ bảng Course dựa trên CourseID
                // Ví dụ sử dụng SQL Server và ADO.NET
                      SqlCommand command = new SqlCommand("SELECT label, Description, semester FROM Course WHERE ID = @CourseID", mydb.getConnection);
                mydb.closeConnection();
                mydb.openConnection();
                    command.Parameters.AddWithValue("@CourseID", courseId);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        courseName = reader["label"].ToString();
                        courseDescription = reader["Description"].ToString();
                        semester = reader["semester"].ToString();

                    }

                // Đặt giá trị cho các cột của hàng
                row["ID"] = courseId;
                row["Name"] = courseName;
                row["Description"] = courseDescription;
                row["semester"] =semester;
                // Đặt giá trị cho các cột khác nếu cần thiết

                // Thêm hàng vào DataTable
                dataTable.Rows.Add(row);
            }
            dataGridView1.DataSource = dataTable;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
