using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QLSV
{
    public partial class EditCourse : Form
    {
        public EditCourse()
        {
            InitializeComponent();
        }
        Course course = new Course();
        public void fillCombo(int index)
        {
            comboBoxCourse.DataSource = course.getAllCourses();
            comboBoxCourse.DisplayMember = "id";
            comboBoxCourse.ValueMember = "id";
            comboBoxCourse.SelectedItem = null;
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            string name = textBoxLabel.Text;
            int hrs = (int)numericUpDown1.Value;
            string descr = textBoxDescription.Text;
            string id = (string)comboBoxCourse.SelectedValue;
            string semester = comboBox1.Text;
            if (verif())
            {
                if (course.updateCourse(id, name, hrs, descr, semester))
                {
                    MessageBox.Show("Successfull");
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }

        }
        bool verif()
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();

            bool flag = true;

            if(comboBoxCourse.Text.Trim() =="")
            {
                errorProvider5.SetError(comboBoxCourse, "Không được để trống");
                flag = false;
            }
            if (comboBox1.Text.Trim() == "")
            {
                errorProvider1.SetError(comboBox1, "Không được để trống");
                flag = false;
            }
            if (textBoxLabel.Text.Trim() == "")
            {
                errorProvider2.SetError(textBoxLabel, "Không được để trống");
                flag = false;
            }

            if (numericUpDown1.Value<10)
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

        private void comboBoxCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string id = (string)comboBoxCourse.SelectedValue;
                DataTable table = new DataTable();
                table = course.getCourseById(id);
                textBoxLabel.Text = table.Rows[0][1].ToString();
                numericUpDown1.Value = Int32.Parse(table.Rows[0][2].ToString());
                textBoxDescription.Text = table.Rows[0][3].ToString();
                comboBox1.Text = table.Rows[0][4].ToString();
            }
            catch { }
        }

        private void EditCourse_Load(object sender, EventArgs e)
        {
            comboBoxCourse.DataSource = course.getAllCourses();
            comboBoxCourse.DisplayMember = "id";
            comboBoxCourse.ValueMember = "id";
            comboBoxCourse.SelectedItem = null;

        }

      
    }

}
