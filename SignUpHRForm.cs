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
using static System.Windows.Forms.AxHost;

namespace QLSV
{
    public partial class SignUpHRForm : Form
    {
        public SignUpHRForm()
        {
            InitializeComponent();
        }
        HR hr = new HR();
        bool ContainsNumbers(string input)
        {
            return input.Any(char.IsDigit);
        }

        bool IsNumeric(string input)
        {
            return int.TryParse(input, out _);
        }
        private void ButtonSignUp_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();

            string fname = TextBoxFname.Text.Trim();
            string lname = TextBoxLname.Text.Trim();
            string username = textBoxUsername.Text.Trim();
            string password = textBoxPassword.Text.Trim();

            if (string.IsNullOrEmpty(fname))
            {
                errorProvider1.SetError(TextBoxFname, "First name is required.");
                return;
            }
            if (ContainsNumbers(fname))
            {
                errorProvider1.SetError(TextBoxFname, "First name is required.");
                return;
            }
            if (string.IsNullOrEmpty(lname))
            {
                errorProvider2.SetError(TextBoxLname, "Last name is required.");
                return;
            }

            if (ContainsNumbers(lname))
            {
                errorProvider1.SetError(TextBoxLname, "Last name is required.");
                return;
            }
            if (string.IsNullOrEmpty(username))
            {
                errorProvider3.SetError(textBoxUsername, "Username is required.");
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                errorProvider4.SetError(textBoxPassword, "Password is required.");
                return;
            }




            // Your sign-up logic here

            MemoryStream pic = new MemoryStream();
            
            // Kiểm tra xem có hình ảnh nào được chọn không
            if (PictureBoxStudentImage.Image != null)
            {
                // Lưu hình ảnh vào MemoryStream
                PictureBoxStudentImage.Image.Save(pic, PictureBoxStudentImage.Image.RawFormat);
            }
            else
            {
                // Hiển thị thông báo khi không có hình ảnh được chọn
                MessageBox.Show("Please select an image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Dừng việc thực hiện thêm mới
            }

            if (hr.insertHR(fname, lname, username, password, pic))
            {
                MessageBox.Show("New Student Added", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


    }
}
