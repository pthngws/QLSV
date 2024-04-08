
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using System.Xml.Linq;


namespace QLSV
{
    public partial class StudentListForm : Form
    {
        public StudentListForm()
        {
            InitializeComponent();
        }

        private void StudentListForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'baiTapWinformDataSet.std' table. You can move, or remove it, as needed.

            // Tạo câu lệnh SQL để lấy tất cả sinh viên
            SqlCommand cmd = new SqlCommand("SELECT * FROM std");

            // Cấu hình hiển thị dữ liệu trên DataGridView
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = STUDENT.getStudents(cmd);
            picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Kiểm tra hàng không phải hàng mới thêm vào
                if (!row.IsNewRow)
                {
                    // Lấy giá trị cột ID từ hàng
                    int id = Convert.ToInt32(row.Cells["idDataGridViewTextBoxColumn"].Value);
                    // Tạo giá trị email và đặt vào cột "email"
                    row.Cells["Email"].Value = id.ToString() + "@student.hcmute.edu.vn";
                }
            }



        }
        STUDENT STUDENT = new STUDENT();
        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            StudentListForm_Load(sender, e);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateDeleteStudentForm updateDeleteStudentForm = new UpdateDeleteStudentForm();
            try
            {
                updateDeleteStudentForm.TextBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value?.ToString() ?? null;
                updateDeleteStudentForm.TextBoxFname.Text = dataGridView1.CurrentRow.Cells[1].Value?.ToString() ?? null;
                updateDeleteStudentForm.TextBoxLname.Text = dataGridView1.CurrentRow.Cells[2].Value?.ToString() ?? null;

                // Kiểm tra nếu giá trị của ô là null hoặc không thể chuyển đổi sang kiểu DateTime
                if (dataGridView1.CurrentRow.Cells[3].Value == null || !DateTime.TryParse(dataGridView1.CurrentRow.Cells[3].Value.ToString(), out DateTime dateValue))
                {
                    updateDeleteStudentForm.DateTimePicker1.Value = DateTime.Now; // hoặc giá trị mặc định khác tùy thuộc vào yêu cầu của bạn
                }
                else
                {
                    updateDeleteStudentForm.DateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells[3].Value;
                }

                if (dataGridView1.CurrentRow.Cells[4].Value?.ToString().Trim() == "Female")
                {
                    updateDeleteStudentForm.RadioButtonFemale.Checked = true;
                }
                else
                {
                    updateDeleteStudentForm.RadioButtonMale.Checked = true;
                }

                updateDeleteStudentForm.TextBoxPhone.Text = dataGridView1.CurrentRow.Cells[5].Value?.ToString() ?? null;
                updateDeleteStudentForm.TextBoxAddress.Text = dataGridView1.CurrentRow.Cells[6].Value?.ToString() ?? null;

                byte[] pic = dataGridView1.CurrentRow.Cells[7].Value as byte[];
                if (pic != null && pic.Length > 0)
                {
                    MemoryStream picture = new MemoryStream(pic);
                    updateDeleteStudentForm.PictureBoxStudentImage.Image = Image.FromStream(picture);
                }
                else
                {
                    updateDeleteStudentForm.PictureBoxStudentImage.Image = null; // hoặc thiết lập hình ảnh mặc định khác
                }

                this.Show();
                updateDeleteStudentForm.Show();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }




        }
        MY_DB MY_DB = new MY_DB();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            string filepath = "";
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                filepath = op.FileName;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Fname");
            dt.Columns.Add("Lname");
            dt.Columns.Add("bdate");

            // Cài đặt LicenseContext ở đầu phương thức
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            try
            {
                var package = new ExcelPackage(new FileInfo(filepath));
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    int j = 3;
                    string mssv = worksheet.Cells[i, j++].Value?.ToString();
                    string fname = worksheet.Cells[i, j++].Value?.ToString();
                    string lname = worksheet.Cells[i, j++].Value?.ToString();
                    var bdatetemp = worksheet.Cells[i, j++].Value;

                    DateTime bdate;
                    if (DateTime.TryParse(bdatetemp?.ToString(), out bdate))
                    {
                        bdate = bdate.Date;
                        dt.Rows.Add(mssv, fname, lname, bdate);
                    }
                    else
                    {
                        dt.Rows.Add(mssv, fname, lname, DBNull.Value);
                    }
                }

                // Đặt DataSource của DataGridView sau khi đã thêm dữ liệu vào DataTable

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data from Excel: " + ex.Message);

            }
            foreach (DataRow row in dt.Rows)
            {
                // Lấy thông tin sinh viên từ từng hàng trong DataTable
                string mssv = row["ID"].ToString();
                string fname = row["Fname"].ToString();
                string lname = row["Lname"].ToString();
                DateTime bdate = Convert.ToDateTime(row["bdate"]);

                // Kiểm tra xem sinh viên đã tồn tại trong cơ sở dữ liệu chưa
                string queryCheckStudent = "SELECT COUNT(*) FROM std WHERE id = @mssv";
                SqlCommand cmdCheckStudent = new SqlCommand(queryCheckStudent, MY_DB.getConnection);
                MY_DB.closeConnection();
                MY_DB.openConnection();
                cmdCheckStudent.Parameters.AddWithValue("@mssv", mssv);

                int existingStudentCount = (int)cmdCheckStudent.ExecuteScalar();

                // Nếu sinh viên chưa tồn tại trong cơ sở dữ liệu, thêm mới
                if (existingStudentCount == 0)
                {
                    // Thực hiện truy vấn để chèn sinh viên mới vào cơ sở dữ liệu
                    string queryInsertStudent = "INSERT INTO std (id, fname, lname, bdate) VALUES (@mssv, @fname, @lname, @bdate)";
                    SqlCommand cmdInsertStudent = new SqlCommand(queryInsertStudent, MY_DB.getConnection);
                    MY_DB.closeConnection();
                    MY_DB.openConnection();
                    cmdInsertStudent.Parameters.AddWithValue("@mssv", mssv);
                    cmdInsertStudent.Parameters.AddWithValue("@fname", fname);
                    cmdInsertStudent.Parameters.AddWithValue("@lname", lname);
                    cmdInsertStudent.Parameters.AddWithValue("@bdate", bdate);
                    cmdInsertStudent.ExecuteNonQuery();
                }
                else
                {
                    // Sinh viên đã tồn tại trong cơ sở dữ liệu, bạn có thể thực hiện xử lý khác tùy thuộc vào yêu cầu của bạn, ví dụ: cập nhật thông tin
                    // MessageBox.Show("Sinh viên với mã số sinh viên " + mssv + " đã tồn tại trong cơ sở dữ liệu.");
                }
                SqlCommand cmd = new SqlCommand("Select * from std");
                dataGridView1.DataSource = STUDENT.getStudents(cmd);
            }
            StudentListForm_Load(sender, e);

        }
    }
}




















            /*        private void buttonImport_Click(object sender, EventArgs e)
                    {
                        this.stdTableAdapter.Fill(this.baiTapWinformDataSet.std);
                        SqlCommand cmd = new SqlCommand("select * from std");
                        dataGridView1.ReadOnly = true;
                        DataGridViewImageColumn picCol = new DataGridViewImageColumn();
                        dataGridView1.RowTemplate.Height = 80;
                        dataGridView1.DataSource = STUDENT.getStudents(cmd);
                        picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];
                        picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
                        dataGridView1.AllowUserToAddRows = false;
                        this.stdTableAdapter.Fill(this.baiTapWinformDataSet.std);
                        dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";

                        // Thêm cột "email" nếu chưa tồn tại
                        if (!dataGridView1.Columns.Contains("email"))
                        {
                            DataGridViewTextBoxColumn emailColumn = new DataGridViewTextBoxColumn();
                            emailColumn.HeaderText = "Email";
                            emailColumn.Name = "email";
                            dataGridView1.Columns.Add(emailColumn);
                        }
            */

// Đặt giá trị cho cột "email"

/*        private void buttonImport_Click(object sender, EventArgs e)
        {
            string filepath = "";
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                filepath = op.FileName;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("MSSV");
            dt.Columns.Add("Họ");
            dt.Columns.Add("Tên");
            dt.Columns.Add("Ngày sinh");

            try
            {
                var package = new ExcelPackage(new FileInfo(filepath));
*//*                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;*//*
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
                {
                    try
                    {
                        int j = 1; // Bắt đầu từ cột 1 (A)
                        string mssv = worksheet.Cells[i, j++].Value?.ToString();
                        string fname = worksheet.Cells[i, j++].Value?.ToString();
                        string lname = worksheet.Cells[i, j++].Value?.ToString();
                        DateTime bdate = DateTime.MinValue;
                        if (DateTime.TryParse(worksheet.Cells[i, j].Value?.ToString(), out bdate))
                        {
                            dt.Rows.Add(mssv, fname, lname, bdate);
                        }
                        else
                        {
                            MessageBox.Show($"Invalid date format at row {i}");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error importing data from Excel: " + ex.Message);
            }
        }*/







