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
    public partial class ScoreOfStudent : Form
    {
        public ScoreOfStudent()
        {
            InitializeComponent();
        }
        MY_DB mydb = new MY_DB();
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxSearch.Text, out int studentId))
            {
                 string id = textBoxSearch.Text;

                // Tạo một đối tượng DataTable để lưu trữ kết quả truy vấn
                DataTable dataTable = new DataTable();

                // Tạo một đối tượng SqlCommand
                using (SqlCommand command = new SqlCommand())
                {
                    // Thiết lập chuỗi kết nối
                    command.Connection = mydb.getConnection;

                    // Tạo truy vấn SQL
                    string sqlQuery = @"SELECT std.fname, std.lname, std.bdate, Course.Id as 'ID Khóa Học', Course.label'Tên môn học', Score.student_score as 'Điểm số'
                            FROM std
                            INNER JOIN score ON std.id = score.studentID
                            INNER JOIN course ON Course.id = score.courseID
                            WHERE std.id = @StudentId";

                    // Thiết lập truy vấn và tham số
                    command.CommandText = sqlQuery;
                    command.Parameters.AddWithValue("@StudentId", id);

                    // Tạo đối tượng SqlDataAdapter và điền dữ liệu vào DataTable
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }

                // Gán DataTable vào DataGridView
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns.Remove("fname");
                dataGridView1.Columns.Remove("lname");
                dataGridView1.Columns.Remove("bdate");
                // Gán các thông tin cá nhân vào các label
                if (dataTable.Rows.Count > 0)
                {
                    label5.Text = dataTable.Rows[0]["fname"].ToString();
                    label6.Text = dataTable.Rows[0]["lname"].ToString();
                    DateTime bdate = Convert.ToDateTime(dataTable.Rows[0]["bdate"]);
                    label7.Text = bdate.ToString("dd/MM/yyyy");
                }
                else
                {
                    // Nếu không có dữ liệu trả về, xử lý tương ứng
                    label5.Text = "N/A";
                    label6.Text = "N/A";
                    label7.Text = "N/A";
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập lại. Hãy chắc chắn rằng ô tìm kiếm chỉ chứa số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

    
  
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxSearch.Text, out int studentId))
            {

                ScoreOfStudentRP scoreOfStudentRP = new ScoreOfStudentRP(textBoxSearch.Text);
                scoreOfStudentRP.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập lại. Hãy chắc chắn rằng ô tìm kiếm chỉ chứa số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }


    }
