using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace QLSV
{
    public partial class SearchNameForm : Form
    {
        // Khởi tạo đối tượng quản lý sinh viên
        private readonly STUDENT STUDENT = new STUDENT();

        public SearchNameForm()
        {
            InitializeComponent();
        }
        User user = new User();
        public SearchNameForm(User user)
        {
            InitializeComponent();
            this.user = user;
        }
        private void SearchNameForm_Load(object sender, EventArgs e)
        {
            // Nạp dữ liệu vào bảng 'std' từ DataSet khi form được load
            this.stdTableAdapter.Fill(this.baiTapWinformDataSet1.std);

            // Tạo câu lệnh SQL để lấy tất cả sinh viên
            SqlCommand cmd = new SqlCommand("SELECT * FROM std");

            // Cấu hình hiển thị dữ liệu trên DataGridView
            DataGridView.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            DataGridView.RowTemplate.Height = 80;
            DataGridView.DataSource = STUDENT.getStudents(cmd);
            picCol = (DataGridViewImageColumn)DataGridView.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            DataGridView.AllowUserToAddRows = false;
            DataGridView.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
        }
        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (user.Role == "admin")
            {
                UpdateDeleteStudentForm updateDeleteStudentForm = new UpdateDeleteStudentForm();
                updateDeleteStudentForm.TextBoxID.Text = DataGridView.CurrentRow.Cells[0].Value.ToString();
                updateDeleteStudentForm.TextBoxFname.Text = DataGridView.CurrentRow.Cells[1].Value.ToString();
                updateDeleteStudentForm.TextBoxLname.Text = DataGridView.CurrentRow.Cells[2].Value.ToString();
                updateDeleteStudentForm.DateTimePicker1.Value = (DateTime)DataGridView.CurrentRow.Cells[3].Value;

                if (DataGridView.CurrentRow.Cells[4].Value.ToString().Trim() == "Female")
                {
                    updateDeleteStudentForm.RadioButtonFemale.Checked = true;
                }
                else
                {
                    updateDeleteStudentForm.RadioButtonMale.Checked = true;
                }
                updateDeleteStudentForm.TextBoxPhone.Text = DataGridView.CurrentRow.Cells[5].Value.ToString();
                updateDeleteStudentForm.TextBoxAddress.Text = DataGridView.CurrentRow.Cells[6].Value.ToString();
                byte[] pic;
                pic = (byte[])DataGridView.CurrentRow.Cells[7].Value;
                MemoryStream picture = new MemoryStream(pic);
                updateDeleteStudentForm.PictureBoxStudentImage.Image = Image.FromStream(picture);
                this.Show();
                updateDeleteStudentForm.Show();
            }


        }
        private void ButtonSearch_Click_1(object sender, EventArgs e)
        {
            // Lấy thông tin tên họ cần tìm kiếm từ TextBoxSearch
            string searchValue = TextBoxSearch.Text.Trim();

            // Xác định loại tìm kiếm dựa trên giá trị của ComboBoxSearchType
            string columnName = (comboBox.SelectedItem as string);

            // Tạo câu lệnh SQL với tham số để tránh SQL injection
            SqlCommand cmd;

            if (columnName == "Tên")
            {
                cmd = new SqlCommand("SELECT * FROM std WHERE lname LIKE '%' + @SearchValue + '%'");
            }
            else if (columnName == "Số điện thoại")
            {
                cmd = new SqlCommand("SELECT * FROM std WHERE phone LIKE '%' + @SearchValue + '%'");
            }
            else if (columnName == "MSSV")
            {
                cmd = new SqlCommand("SELECT * FROM std WHERE id LIKE '%' + @SearchValue + '%'");
            }


            else
            {
                // Mặc định nếu không có lựa chọn hợp lệ
                MessageBox.Show("Vui lòng chọn một loại tìm kiếm hợp lệ.");
                return;
            }

            cmd.Parameters.AddWithValue("@SearchValue", searchValue);

            // Cấu hình hiển thị dữ liệu trên DataGridView
            DataGridView.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            DataGridView.RowTemplate.Height = 80;
            DataGridView.DataSource = STUDENT.getStudents(cmd);
            picCol = (DataGridViewImageColumn)DataGridView.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            DataGridView.AllowUserToAddRows = false;
            DataGridView.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
        }
        private void Button_Refresh_Click(object sender, EventArgs e)
        {
            this.stdTableAdapter.Fill(this.baiTapWinformDataSet1.std);
            SqlCommand cmd = new SqlCommand("select * from std");
            DataGridView.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            DataGridView.RowTemplate.Height = 80;
            DataGridView.DataSource = STUDENT.getStudents(cmd);
            picCol = (DataGridViewImageColumn)DataGridView.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            DataGridView.AllowUserToAddRows = false;
            TextBoxSearch.Text = string.Empty;
            comboBox.SelectedIndex = -1;
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TextBoxSearch_KeyUp(object sender, KeyEventArgs e)
        {
            string searchValue = TextBoxSearch.Text.Trim();

            // Xác định loại tìm kiếm dựa trên giá trị của ComboBoxSearchType
            string columnName = (comboBox.SelectedItem as string);

            // Tạo câu lệnh SQL với tham số để tránh SQL injection
            SqlCommand cmd;

            if (columnName == "Tên")
            {
                cmd = new SqlCommand("SELECT * FROM std WHERE lname LIKE '%' + @SearchValue + '%'");
            }
            else if (columnName == "Số điện thoại")
            {
                cmd = new SqlCommand("SELECT * FROM std WHERE phone LIKE '%' + @SearchValue + '%'");
            }
            else if (columnName == "MSSV")
            {
                cmd = new SqlCommand("SELECT * FROM std WHERE id LIKE '%' + @SearchValue + '%'");
            }


            else
            {
                // Mặc định nếu không có lựa chọn hợp lệ
                MessageBox.Show("Vui lòng chọn một loại tìm kiếm hợp lệ.");
                return;
            }

            cmd.Parameters.AddWithValue("@SearchValue", searchValue);

            // Cấu hình hiển thị dữ liệu trên DataGridView
            DataGridView.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            DataGridView.RowTemplate.Height = 80;
            DataGridView.DataSource = STUDENT.getStudents(cmd);
            picCol = (DataGridViewImageColumn)DataGridView.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            DataGridView.AllowUserToAddRows = false;
            DataGridView.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
