using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLSV
{
    public partial class UpdateDeleteStudentForm : Form
    {
        public UpdateDeleteStudentForm()
        {
            InitializeComponent();
        }

        STUDENT student = new STUDENT();
        private void ButtonEdit_Click(object sender, EventArgs e)
            {
                try
                {
                    int id = int.Parse(TextBoxID.Text);
                    string fname = TextBoxFname.Text;
                    string lname = TextBoxLname.Text;
                    DateTime bdate = DateTimePicker1.Value;
                    string gender = RadioButtonFemale.Checked ? "Female" : "Male";
                    string phone = TextBoxPhone.Text;
                    string address = TextBoxAddress.Text;

                    // Chuyển đổi hình ảnh thành mảng byte để lưu trong cơ sở dữ liệu
                    MemoryStream pictureMemoryStream = new MemoryStream();
                    PictureBoxStudentImage.Image.Save(pictureMemoryStream, PictureBoxStudentImage.Image.RawFormat);
                    byte[] picture = pictureMemoryStream.ToArray();
                    int born_year = DateTimePicker1.Value.Year;
                    int this_year = DateTime.Now.Year;
                    //  sv tu 10-100,  co the thay doi
                    if (((this_year - born_year) < 10) || ((this_year - born_year) > 100))
                    {
                        MessageBox.Show("The Student Age Must Be Between 10 and 100 year", "Invalid Birth Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (verif())
                    {
                        // Tạo câu lệnh SQL để cập nhật dữ liệu
                        SqlCommand updateCommand = new SqlCommand("UPDATE std SET fname = @Fname, lname = @Lname, bdate = @Bdate, gender = @Gender, phone = @Phone, address = @Address, picture = @Picture WHERE id = @ID");
                        updateCommand.Parameters.AddWithValue("@ID", id);
                        updateCommand.Parameters.AddWithValue("@Fname", fname);
                        updateCommand.Parameters.AddWithValue("@Lname", lname);
                        updateCommand.Parameters.AddWithValue("@Bdate", bdate);
                        updateCommand.Parameters.AddWithValue("@Gender", gender);
                        updateCommand.Parameters.AddWithValue("@Phone", phone);
                        updateCommand.Parameters.AddWithValue("@Address", address);
                        updateCommand.Parameters.AddWithValue("@Picture", picture);

                        // Thực hiện cập nhật
                        if (student.updateStudent(updateCommand))
                        {
                            MessageBox.Show("Data updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
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
            }
        bool verif()
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            bool flag = true;

            if (TextBoxFname.Text.Trim() == "")
            {
                errorProvider1.SetError(TextBoxFname, "Không được để trống");
                flag = false;
            }
            else if (ContainsNumbers(TextBoxFname.Text))
            {
                errorProvider1.SetError(TextBoxFname, "Tên không được chứa số");
                flag = false;
            }

            if (TextBoxLname.Text.Trim() == "")
            {
                errorProvider2.SetError(TextBoxLname, "Không được để trống");
                flag = false;
            }
            else if (ContainsNumbers(TextBoxLname.Text))
            {
                errorProvider2.SetError(TextBoxLname, "Họ không được chứa số");
                flag = false;
            }

            if (TextBoxAddress.Text.Trim() == "")
            {
                errorProvider3.SetError(TextBoxAddress, "Không được để trống");
                flag = false;
            }

            if (TextBoxPhone.Text.Trim() == "")
            {
                errorProvider4.SetError(TextBoxPhone, "Không được để trống");
                flag = false;
            }
            else if (!IsNumeric(TextBoxPhone.Text))
            {
                errorProvider4.SetError(TextBoxPhone, "Số điện thoại chỉ được chứa số");
                flag = false;
            }

            if (PictureBoxStudentImage.Image == null)
            {
                errorProvider5.SetError(PictureBoxStudentImage, "Không được để trống");
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

        private void ButtonRemove_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
                try
                {

                    int id = int.Parse(TextBoxID.Text);

                    // Thực hiện xóa sinh viên
                    if (student.deleteStudent(id))
                    {
                        TextBoxID.Text = "";
                        TextBoxLname.Text = "";
                        TextBoxFname.Text = "";
                        TextBoxAddress.Text = "";
                        TextBoxPhone.Text = "";
                        DateTimePicker1.Value = DateTime.Now;
                        PictureBoxStudentImage.Image = null;
                    
                        MessageBox.Show("Student removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to remove student.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void TextBoxID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void ButtonFind_Click(object sender, EventArgs e)
        {

        }

        private void PictureBoxStudentImage_Click(object sender, EventArgs e)
        {

        }

        private void TextBoxAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label5_Click(object sender, EventArgs e)
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

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void TextBoxPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonAddCourse_Click(object sender, EventArgs e)
        {
            AddCourseForm addCourseForm = new AddCourseForm(TextBoxID.Text);
            addCourseForm.Show();
        }
    }
    }

