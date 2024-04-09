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
    public partial class ManageCourse : Form
    {
        Course course = new Course();
        int pos;
        public ManageCourse()
        {
            InitializeComponent();
        }

        private void ManageCourse_Load(object sender, EventArgs e)
        {
            reloadListBoxData();
        }
        void reloadListBoxData()
        {
            listBox1.DataSource = course.getAllCourses();
            listBox1.ValueMember = "id";
            listBox1.DisplayMember = "label";

            listBox1.SelectedItem = null;

            label1.Text  = ("Total Course: " + course.totalCourse());
        }
        void showData(int index)
        {
            DataRow dr = course.getAllCourses().Rows[index];
            listBox1.SelectedIndex = index;
            textBoxID.Text = dr.ItemArray[0].ToString();
            textBoxLabel.Text = dr.ItemArray[1].ToString();
            numericUpDown1.Value = int.Parse(dr.ItemArray[2].ToString());
            textBoxDescription.Text = dr.ItemArray[3].ToString();
            comboBox1.Text = dr.ItemArray[4].ToString();
        }

private void listBox1_Click(object sender, EventArgs e)
        {
            DataRowView drv = (DataRowView)listBox1.SelectedItem;
            pos = listBox1.SelectedIndex;
            showData(pos);
        }

        private void buttonFirst_Click(object sender, EventArgs e)
        {
            pos = 0;
            showData(pos);
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if(pos<(course.getAllCourses().Rows.Count-1))
            {
                pos = pos + 1;
                showData(pos);
            }
            if(pos==(course.getAllCourses().Rows.Count-1))
            {
                showData(pos);
                pos = -1;
            }
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if(pos>0)
            {
                pos = pos - 1;
                showData((int)pos);
            }
            if (pos == 0)
            {
                showData(pos);
                pos = course.getAllCourses().Rows.Count ;
            }
        }

        private void buttonLast_Click(object sender, EventArgs e)
        {
            pos = course.getAllCourses().Rows.Count-1;
            showData(pos);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            string name = textBoxLabel.Text;
            string descr = textBoxDescription.Text;
            string id = textBoxID.Text;
            string semester = comboBox1.Text;
            if (verif())
            {
                int period = Convert.ToInt32(numericUpDown1.Text.Trim());
                if (course.updateCourse(id, name, period, descr, semester))
                {
                    MessageBox.Show("Successfull");
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {

            Course course = new Course();
            string label = textBoxLabel.Text.Trim();

            string description = textBoxDescription.Text.Trim();
            string semester = comboBox1.Text.Trim();
            //  sv tu 10-100,  co the thay doi
            if (verif())
            {
                int period = Convert.ToInt32(numericUpDown1.Text.Trim());
                string courseID = textBoxID.Text.Trim();

                if (course.insertCourse(courseID, label, period, description, semester))
                {
                    MessageBox.Show("New Course Added", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("CourseID Existed", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Empty Fields", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        //  chuc nang kiem tra du lieu input
        bool verif()
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();

            bool flag = true;

            if (textBoxID.Text.Trim() == "")
            {
                errorProvider1.SetError(textBoxID, "Không được để trống");
                flag = false;
            }
            if(comboBox1.Text=="")
            {
                errorProvider5.SetError(comboBox1, "Không được để trống");
                flag = false;
            }
            if (textBoxLabel.Text.Trim() == "")
            {
                errorProvider2.SetError(textBoxLabel, "Không được để trống");
                flag = false;
            }

            if (numericUpDown1.Text.Trim() == "")
            {
                errorProvider3.SetError(numericUpDown1, "Không được để trống");
                flag = false;
            }
            else if (!IsNumeric(numericUpDown1.Text))
            {
                errorProvider3.SetError(numericUpDown1, "Họ không được chứa chữ");
                flag = false;
            }
            else if (Convert.ToInt32(numericUpDown1.Text) < 10)
            {
                errorProvider3.SetError(numericUpDown1, "Phải lớn hơn 10");
                flag = false;
            }

            if (textBoxDescription.Text.Trim() == "")
            {
                errorProvider4.SetError(textBoxDescription, "Không được để trống");
                flag = false;
            }



            return flag;
        }

        bool ContainsNumbers(string input)
        {
            return input.Any(char.IsDigit);
        }

        bool IsNumeric(string input)
        {
            return int.TryParse(input, out _);
        }


        private void btnRemove_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa Course này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
                try
                {

                    string id = textBoxID.Text;

                    // Thực hiện xóa sinh viên
                    if (course.deleteCourse(id))
                    {
                        textBoxID.Text = "";
                        textBoxLabel.Text = "";
                        textBoxDescription.Text = "";
                        MessageBox.Show("Student removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to remove.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"SQL error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            ManageCourse_Load(sender, e);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            DataRowView drv = (DataRowView)listBox1.SelectedItem;
            string coursename = drv.Row[1].ToString();
            string courseid = drv.Row[0].ToString();
            string semester = (string)comboBox1.Text;

            CourseStdList courseStdList = new CourseStdList(coursename,semester,courseid);
            courseStdList.Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
