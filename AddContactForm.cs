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
                MemoryStream pic = new MemoryStream();
                PictureBoxStudentImage.Image.Save(pic, PictureBoxStudentImage.Image.RawFormat);
                Contact contact = new Contact();
                if(contact.insertContact(id,fname, lname, phone, addre,email,userid,groupid,pic))
                {
                    MessageBox.Show("Thanh cong");
                }
                else
                {
                    MessageBox.Show("That bai");
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
