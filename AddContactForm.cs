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
    public partial class AddContactForm : Form
    {
        public AddContactForm()
        {
            InitializeComponent();
        }

        private void AddContactForm_Load(object sender, EventArgs e)
        {
            Group group = new Group();
            comboBox1.DataSource = group.getGroups(Global.GlobalUserID);
            comboBox1.DisplayMember ="Name";
            comboBox1.ValueMember = "id";
        }
        bool verif()
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();
            errorProvider7.Clear();
            bool flag = true;

            if (txtStudentID.Text.Trim() == "")
            {
                errorProvider1.SetError(txtStudentID, "Không được để trống");
                flag = false;
            }
            else if (!IsNumeric(txtStudentID.Text.Trim()))

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
            if (textBoxEmail.Text.Trim() == "")
            {
                errorProvider7.SetError(textBoxEmail, "Không được để trống");
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
        private void ButtonAddStudent_Click(object sender, EventArgs e)
        {
            string id = txtStudentID.Text;
            string fname = TextBoxFname.Text;
            string lname = TextBoxLname.Text;
            string phone = TextBoxPhone.Text;
            string addre = TextBoxAddress.Text;
            string email = textBoxEmail.Text;
            string userid = Global.GlobalUserID;
            try
            {
                string groupid = (string)comboBox1.SelectedValue;

                Contact contact = new Contact();
                if (verif())
                {
                    MemoryStream pic = new MemoryStream();
                    PictureBoxStudentImage.Image.Save(pic, PictureBoxStudentImage.Image.RawFormat);
                    if (contact.insertContact(id, fname, lname, phone, addre, email, userid, groupid, pic))
                    {
                        MessageBox.Show("Thanh cong");
                    }
                    else
                    {
                        MessageBox.Show("That bai");
                    }
                }
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        private void ButtonUploadImage_Click(object sender, EventArgs e)
        {

        }

        private void ButtonUploadImage_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if ((opf.ShowDialog() == DialogResult.OK))
            {
                PictureBoxStudentImage.Image = Image.FromFile(opf.FileName);
            }
        }

        private void txtStudentID_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label6_Click(object sender, EventArgs e)
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

        private void ButtonCancel_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void PictureBoxStudentImage_Click(object sender, EventArgs e)
        {

        }

        private void TextBoxAddress_TextChanged(object sender, EventArgs e)
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
