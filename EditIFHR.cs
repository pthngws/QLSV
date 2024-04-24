using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class EditIFHR : Form
    {
        MY_DB mydb = new MY_DB();
        public EditIFHR()
        {
            InitializeComponent(); // Đặt phương thức này ở đầu tiên

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from hr where username = @us", mydb.getConnection);
            cmd.Parameters.AddWithValue("@us", Global.GlobalUserID);
            sqlDataAdapter.SelectCommand = cmd;
            sqlDataAdapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                TextBoxFname.Text = dt.Rows[0]["fname"].ToString();
                TextBoxLname.Text = dt.Rows[0]["lname"].ToString();
                byte[] pic = (byte[])dt.Rows[0]["picture"];
                MemoryStream picture = new MemoryStream(pic);
                PictureBoxStudentImage.Image = Image.FromStream(picture);
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
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            if (string.IsNullOrEmpty(TextBoxFname.Text)) {
                errorProvider1.SetError(TextBoxFname, "Vui long nhap");
                return;
            }
            else if (ContainsNumbers(TextBoxFname.Text))
            {
                errorProvider1.SetError(TextBoxFname, "Tên không được chứa số");
                return;
            }
            if (string.IsNullOrEmpty(TextBoxLname.Text))
            {
                errorProvider2.SetError(TextBoxLname, "Vui long nhap");
                return;
            }
            else if (ContainsNumbers(TextBoxLname.Text))
            {
                errorProvider2.SetError(TextBoxLname, "Họ không được chứa số");
                return;
            }

            if (PictureBoxStudentImage.Image == null)
            {
                errorProvider3.SetError(PictureBoxStudentImage, "Không được để trống");
                return;
            }
            HR hR = new HR();
            string id = Global.GlobalUserID.ToString();
            MemoryStream pic = new MemoryStream();
            PictureBoxStudentImage.Image.Save(pic, PictureBoxStudentImage.Image.RawFormat);
            if (hR.updateHR(id,TextBoxFname.Text,TextBoxLname.Text,pic))
            {
                MessageBox.Show("Thanh cong");
              
            }
            else { MessageBox.Show("That bai"); }
        }
    }
}
