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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLSV
{
    public partial class ContactCourse : Form
    {
        public ContactCourse()
        {
            InitializeComponent();
        }
        string id;
        string fullname;
        List<string> list = new List<string>();
        Course course = new Course();
        Contact contact = new Contact();
        public ContactCourse(string id,string fullname)
        {
            InitializeComponent();
            this.id = id;
            this.fullname = fullname;
            label3.Text += fullname;
        }





        private void ContactCourse_Load(object sender, EventArgs e)

        {
            SqlCommand sqlCommand = new SqlCommand("select id,label from course where idcontact = @id");
            sqlCommand.Parameters.AddWithValue("@id", id);
            listBox1.DataSource = course.getCourse(sqlCommand);
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "Label";
            listBox1.SelectedItem = null;


            SqlCommand sqlCommand1 = new SqlCommand("select id,label from course where idcontact is null or idcontact =''");
            comboBox1.DataSource = course.getCourse(sqlCommand1);
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "Label";
            comboBox1.SelectedItem = null;
        }

        MY_DB mydb = new MY_DB();
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            DataRowView drv = (DataRowView)listBox1.SelectedItem;
            string coursename = drv.Row[1].ToString();
            string courseid = drv.Row[0].ToString();

            label2.Text = ("Danh sách điểm môn " + coursename).ToUpper();
            SqlCommand cmd = new SqlCommand("select std.id AS 'MSSV' , std.fname as 'Họ', std.lname as 'Tên',std.bdate as 'Ngày sinh', Score.student_score as 'Điểm' from std,score where std.id = Score.studentID and score.courseID = @id", mydb.getConnection);
            cmd.Parameters.AddWithValue("id", courseid);

            mydb.closeConnection();
            mydb.openConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable TABLE = new DataTable();
            adapter.Fill(TABLE);
            dataGridView1.DataSource = TABLE;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có mục nào được chọn trong combobox không
            if (comboBox1.SelectedItem != null)
            {
                // Lấy ID của giảng viên từ combobox
                DataRowView drv = (DataRowView)comboBox1.SelectedItem;
                string courseid = drv.Row[0].ToString();

                // Tạo và thực thi truy vấn để cập nhật cột idcontact trong bảng course

                SqlCommand sqlCommand = new SqlCommand("UPDATE course SET idcontact = @idcontact WHERE id = @courseID", mydb.getConnection);
                mydb.openConnection();
                    sqlCommand.Parameters.AddWithValue("@idcontact", id);
                    sqlCommand.Parameters.AddWithValue("@courseID", courseid); // courseId là ID của khóa học bạn đang xử lý

                    sqlCommand.ExecuteNonQuery();
                int rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    // Cập nhật thành công
                    MessageBox.Show("Cập nhật thành công!");
                    // Load lại dữ liệu
                    ContactCourse_Load(sender, e);
                }
                else
                {
                    // Có lỗi xảy ra khi cập nhật
                    MessageBox.Show("Có lỗi xảy ra khi cập nhật!");
                }
            }
            ContactCourse_Load(sender, e);
            }

        }
}
