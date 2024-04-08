﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class Manage : Form
    {
        public Manage()
        {
            InitializeComponent();
        }

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
        STUDENT student = new STUDENT();
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

        private void ButtonUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
            if ((opf.ShowDialog() == DialogResult.OK))
            {
                PictureBoxStudentImage.Image = Image.FromFile(opf.FileName);
            }
        }

        private void Manage_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'baiTapWinformDataSet7.std' table. You can move, or remove it, as needed.
            this.stdTableAdapter1.Fill(this.baiTapWinformDataSet7.std);
            // TODO: This line of code loads data into the 'baiTapWinformDataSet2.std' table. You can move, or remove it, as needed.
            SqlCommand cmd = new SqlCommand("SELECT * FROM std");
            this.stdTableAdapter.Fill(this.baiTapWinformDataSet2.std);
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = student.getStudents(cmd);
            picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
            label9.Text = "Total student:" + dataGridView1.Rows.Count;
            label9.Text = "Total student:" + dataGridView1.Rows.Count;

        }
        public void fillGrid(SqlCommand command)
        {
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = student.getStudents(command);
            picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
            label9.Text = "Total student:" + dataGridView1.Rows.Count;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TextBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            TextBoxFname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            TextBoxLname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            DateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells[3].Value;

            if (dataGridView1.CurrentRow.Cells[4].Value.ToString().Trim() == "Female")
            {
                RadioButtonFemale.Checked = true;
            }
            else
            {
                RadioButtonMale.Checked = true;
            }
            TextBoxPhone.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            TextBoxAddress.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            byte[] pic;
            pic = (byte[])dataGridView1.CurrentRow.Cells[7].Value;
            MemoryStream picture = new MemoryStream(pic);
            PictureBoxStudentImage.Image = Image.FromStream(picture);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from std where concat(fname,lname,address) like'%" + textBoxSearch.Text + "%'");
            fillGrid(cmd);
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName =("student_"+TextBoxID.Text);
            if(PictureBoxStudentImage.Image == null) {
                MessageBox.Show("Noimage");
            }
            else if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                PictureBoxStudentImage.Image.Save(saveFileDialog.FileName+("."+ImageFormat.Jpeg.ToString()));
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            TextBoxAddress.Text = "";
            TextBoxFname.Text = "";
            TextBoxLname.Text = "";
            TextBoxPhone.Text = "";
            TextBoxID.Text = "";
            PictureBoxStudentImage.Image = null;
            DateTimePicker1.Value = DateTime.Now;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
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
            if (((this_year - born_year) < 10) || ((this_year - born_year) > 100))
            {
                MessageBox.Show("The Student Age Must Be Between 10 and 100 year", "Invalid Birth Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (verif())
            {
                int id = Convert.ToInt32(TextBoxID.Text);
                PictureBoxStudentImage.Image.Save(pic, PictureBoxStudentImage.Image.RawFormat);
                if (student.insertStudent(id, fname, lname, bdate, gender, phone, adrs, pic))
                {
                    MessageBox.Show("New Student Added", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Empty Fields", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}