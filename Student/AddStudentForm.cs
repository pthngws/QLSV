using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class AddStudentForm : Form
    {
        public AddStudentForm()
        {
            InitializeComponent();
        }

        // button cancel ( close the form )
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // button insert a new student
        private void ButtonAddStudent_Click(object sender, EventArgs e)
        {
            STUDENT student = new STUDENT();
            string fname = TextBoxFname.Text.Trim();
            string lname = TextBoxLname.Text.Trim();
            DateTime bdate = DateTimePicker1.Value;
            string phone = TextBoxPhone.Text.Trim();
            string adrs = TextBoxAddress.Text.Trim();
            string gender = "Male";

            if (RadioButtonFemale.Checked)
            {
                gender = "Female";
            }

            MemoryStream pic = new MemoryStream();
            int born_year = DateTimePicker1.Value.Year;
            int this_year = DateTime.Now.Year;
            //  sv tu 10-100,  co the thay doi
            if ( ((this_year - born_year) < 10) || ((this_year - born_year) > 100) )
            {
                MessageBox.Show("The Student Age Must Be Between 10 and 100 year", "Invalid Birth Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verif())
            {
                int id = Convert.ToInt32(txtStudentID.Text);
                PictureBoxStudentImage.Image.Save(pic, PictureBoxStudentImage.Image.RawFormat);
                if (student.insertStudent(id,fname, lname, bdate, gender, phone, adrs, pic))
                {
                    MessageBox.Show("New Student Added", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error","Add Student",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Empty Fields", "Add Student",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            errorProvider6.Clear();
            bool flag = true;

            if (txtStudentID.Text.Trim() == "")
            {
                errorProvider1.SetError(txtStudentID, "Không được để trống");
                flag = false;
            }
            else if(!IsNumeric(txtStudentID.Text.Trim()) )

            {
                errorProvider1.SetError(txtStudentID, "Không được chứa chữ");
                flag = false;
            }

            if (TextBoxFname.Text.Trim() == "")
            {
                errorProvider2.SetError(TextBoxFname, "Không được để trống");
                flag = false;
            }
            else if (ContainsNumbers(TextBoxFname.Text))
            {
                errorProvider2.SetError(TextBoxFname, "Tên không được chứa số");
                flag = false;
            }

            if (TextBoxLname.Text.Trim() == "")
            {
                errorProvider3.SetError(TextBoxLname, "Không được để trống");
                flag = false;
            }
            else if (ContainsNumbers(TextBoxLname.Text))
            {
                errorProvider3.SetError(TextBoxLname, "Họ không được chứa số");
                flag = false;
            }

            if (TextBoxAddress.Text.Trim() == "")
            {
                errorProvider4.SetError(TextBoxAddress, "Không được để trống");
                flag = false;
            }

            if (TextBoxPhone.Text.Trim() == "")
            {
                errorProvider5.SetError(TextBoxPhone, "Không được để trống");
                flag = false;
            }
            else if (!IsNumeric(TextBoxPhone.Text))
            {
                errorProvider5.SetError(TextBoxPhone, "Số điện thoại chỉ được chứa số");
                flag = false;
            }

            if (PictureBoxStudentImage.Image == null)
            {
                errorProvider6.SetError(PictureBoxStudentImage, "Không được để trống");
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



        // button browse image
        private void ButtonUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if ((opf.ShowDialog() == DialogResult.OK))
            {
                PictureBoxStudentImage.Image = Image.FromFile(opf.FileName);
            }
        }

        private void AddStudentForm_Load(object sender, EventArgs e)
        {

        }

        private void RadioButtonMale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void RadioButtonFemale_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void TextBoxAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void TextBoxPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void TextBoxLname_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void TextBoxFname_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtStudentID_TextChanged(object sender, EventArgs e)
        {

        }

        private void PictureBoxStudentImage_Click(object sender, EventArgs e)
        {

        }
    }
}
