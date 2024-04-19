using System;
using System.Collections;
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
    public partial class ManageScore : Form
    {
        public ManageScore()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RemoveScore removeCourse = new RemoveScore();
            removeCourse.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           AvgScore avgScore = new AvgScore();
            avgScore.Show();
        }
        Score score = new Score();
        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = score.getAllScore();
        }
        STUDENT std = new STUDENT();
        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("Select id as MSSV,fname as Họ,lname as Tên from std");
            dataGridView2.DataSource = std.getStudents(sqlCommand);
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {

        }

        List<string> list = new List<string>();
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxID.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            list = std.selectedCourseStudent(textBoxID.Text);
            comboBoxCourse.DataSource = list;
        }

        private void buttonAdd_Click_1(object sender, EventArgs e)
        {
            bool isValid = true;
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();

            // Kiểm tra và hiển thị lỗi cho textBoxID
            if (string.IsNullOrWhiteSpace(textBoxID.Text))
            {
                errorProvider1.SetError(textBoxID, "ID không được để trống");
                isValid = false;
            }

            // Kiểm tra và hiển thị lỗi cho comboBoxCourse
            if (comboBoxCourse.SelectedIndex == -1)
            {
                errorProvider2.SetError(comboBoxCourse, "Vui lòng chọn một khóa học");
                isValid = false;
            }


            // Kiểm tra và hiển thị lỗi cho textBoxScore
            if (!float.TryParse(textBoxScore.Text, out float scoreValue))
            {
                errorProvider3.SetError(textBoxScore, "Giá trị điểm không hợp lệ");
                isValid = false;
            }
            else
            {
                errorProvider3.SetError(textBoxScore, ""); // Xóa lỗi nếu có
            }

            // Kiểm tra và hiển thị lỗi cho textBoxDesc
            if (string.IsNullOrWhiteSpace(textBoxDesc.Text))
            {
                errorProvider4.SetError(textBoxDesc, "Mô tả không được để trống");
                isValid = false;
            }
            else
            {
                errorProvider1.SetError(textBoxDesc, ""); // Xóa lỗi nếu có
            }

            // Nếu dữ liệu hợp lệ, thêm vào cơ sở dữ liệu
            if (isValid)
            {
                string id = textBoxID.Text;
                string courseID = comboBoxCourse.Text;
                string description = textBoxDesc.Text;

                // Gọi phương thức insertscore của đối tượng score
                if (score.insertOrUpdateScore(id, courseID, scoreValue, description))
                {
                    MessageBox.Show("Thành công");
                    textBoxScore.Text = "";
                    textBoxDesc.Text = "";
                }
                else
                {
                    MessageBox.Show("Thất bại");
                }
            }
        }

        private void textBoxID_TextChanged(object sender, EventArgs e)
        {
            list = std.selectedCourseStudent(textBoxID.Text);
            comboBoxCourse.DataSource = list;
        }
    }
}

