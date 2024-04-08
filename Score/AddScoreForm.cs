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
    public partial class AddScoreForm : Form
    {
        public AddScoreForm()
        {
            InitializeComponent();
        }
        STUDENT sTUDENT = new STUDENT();
        List<string> list = new List<string>();
        private void AddScoreForm_Load(object sender, EventArgs e)
        {

            // TODO: This line of code loads data into the 'baiTapWinformDataSet5.std' table. You can move, or remove it, as needed.
            SqlCommand sqlCommand = new SqlCommand("Select id,fname,lname from std");
            dataGridView1.DataSource = sTUDENT.getStudents(sqlCommand);


        }
        Score score = new Score();


        private void buttonAdd_Click(object sender, EventArgs e)
        {

            if (verif())
            {
                string id = textBoxID.Text;
                string courseID = comboBoxCourse.Text;
                float scoreValue = float.Parse(textBoxScore.Text);
                string description = textBoxDesc.Text;
                // Gọi phương thức insertscore của đối tượng score
                if (score.insertOrUpdateScore(id, courseID, scoreValue, description))
                {
                    MessageBox.Show("Thanh cong");
                }
                else
                    MessageBox.Show("That Bai");
            }       }
        bool ContainsNumbers(string input)
        {
            return input.Any(char.IsDigit);
        }

        bool IsNumeric(string input)
        {
            return decimal.TryParse(input, out _);
        }
        bool verif()
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            if (textBoxID.Text == "")
            {
                errorProvider1.SetError(textBoxID, "Không được để trống");
                return false;
            }
            if (comboBoxCourse.Text == "")
            {
                errorProvider2.SetError(comboBoxCourse, "Không được để trống");
                return false;
            }
            if (textBoxScore.Text == "")
            {
                errorProvider3.SetError(textBoxScore, "Không được để trống");
                return false;
            }
            else if(!IsNumeric(textBoxScore.Text))
            {
                errorProvider3.SetError(textBoxScore, "Không được chứa chữ");
                return false;
            }    
            else if(Convert.ToDouble(textBoxScore.Text)>10 || Convert.ToDouble(textBoxScore.Text) <0)
            {
                errorProvider3.SetError(textBoxScore, "Điểm phải từ 0-10");
                return false;
            }    
            if (textBoxDesc.Text == "")
            {
                errorProvider4.SetError(textBoxDesc, "Không được để trống");
                return false;
            }
            return true;
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            list = sTUDENT.selectedCourseStudent(textBoxID.Text);
            comboBoxCourse.DataSource = list;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            list = sTUDENT.selectedCourseStudent(textBoxID.Text);
            comboBoxCourse.DataSource = list;
        }
    }
}
