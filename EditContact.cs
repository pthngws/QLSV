using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
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
    public partial class EditContact : Form
    {
        public EditContact()
        {
            InitializeComponent();
        }
        





    private void buttonSelectContact_Click(object sender, EventArgs e)
    {

        SelectContact SelectContactF = new SelectContact();
        SelectContactF.ShowDialog();

        try
        {

            string contactId = SelectContactF.dataGridView1.CurrentRow.Cells[0].Value.ToString();
              
            Contact contact = new Contact();
            DataTable table = contact.GetContactById(contactId);
               
                txtStudentID.Text = table.Rows[0]["id"].ToString();
            TextBoxFname.Text = table.Rows[0]["fname"].ToString();
            TextBoxLname.Text = table.Rows[0]["lname"].ToString();
            comboBox1.SelectedValue = table.Rows[0]["groupid"];
            TextBoxPhone.Text = table.Rows[0]["phone"].ToString();
            textBoxEmail.Text = table.Rows[0]["email"].ToString();
            TextBoxAddress.Text = table.Rows[0]["address"].ToString();
               

                byte[] pic = (byte[])table.Rows[0]["picture"];
            MemoryStream picture = new MemoryStream(pic);
            PictureBoxStudentImage.Image = Image.FromStream(picture);

        }
        catch (Exception)
        {

        }
    }





    private void ButtonUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if ((opf.ShowDialog() == DialogResult.OK))
            {
                PictureBoxStudentImage.Image = Image.FromFile(opf.FileName);
            }

        }

        bool verif()
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();
            errorProvider10.Clear();
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
                errorProvider10.SetError(textBoxEmail, "Không được để trống");
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
            Contact contact = new Contact();

            string fname = TextBoxFname.Text;
            string lname = TextBoxLname.Text;
            string phone = TextBoxPhone.Text;
            string address = TextBoxAddress.Text;
            string email = textBoxEmail.Text;

            try
            {
                string id =txtStudentID.Text;

                string groupid = (string)comboBox1.SelectedValue;

              
                if (verif())
                {
                    MemoryStream pic = new MemoryStream();
                    PictureBoxStudentImage.Image.Save(pic, PictureBoxStudentImage.Image.RawFormat);
                    if (contact.updateContact(id, fname, lname, phone, address, email, groupid, pic))
                    {
                        MessageBox.Show("Contact Inormation UpDated", "Edit Contact", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error", "Add Contact", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Contact", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditContact_Load(object sender, EventArgs e)
        {

            Group group = new Group();
            comboBox1.DataSource = group.getGroups(Global.GlobalUserID);
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "id";
        }

        private void TextBoxLname_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TextBoxPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBoxAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void PictureBoxStudentImage_Click(object sender, EventArgs e)
        {

        }
    }
   
}
