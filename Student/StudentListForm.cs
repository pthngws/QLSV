
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
            this.stdTableAdapter.Fill(this.baiTapWinformDataSet.std);

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
            this.stdTableAdapter.Fill(this.baiTapWinformDataSet.std);
            SqlCommand cmd = new SqlCommand("select * from std");
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = STUDENT.getStudents(cmd);
            picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateDeleteStudentForm updateDeleteStudentForm = new UpdateDeleteStudentForm();
            updateDeleteStudentForm.TextBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            updateDeleteStudentForm.TextBoxFname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            updateDeleteStudentForm.TextBoxLname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            updateDeleteStudentForm.DateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells[3].Value;

            if (dataGridView1.CurrentRow.Cells[4].Value.ToString().Trim() == "Female")
            {
                updateDeleteStudentForm.RadioButtonFemale.Checked = true;
            }
            else
            {
                updateDeleteStudentForm.RadioButtonMale.Checked = true;
            }
            updateDeleteStudentForm.TextBoxPhone.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            updateDeleteStudentForm.TextBoxAddress.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            byte[] pic;
            pic = (byte[])dataGridView1.CurrentRow.Cells[7].Value;
            MemoryStream picture = new MemoryStream(pic);
            updateDeleteStudentForm.PictureBoxStudentImage.Image = Image.FromStream(picture);
            this.Show();
            updateDeleteStudentForm.Show();



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog.Title = "Select an Excel File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                // Thực hiện kết nối với tệp Excel
                string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // Lấy danh sách các sheet trong tệp Excel
                    DataTable excelSchema = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    // Lấy tên của sheet đầu tiên
                    string sheetName = excelSchema.Rows[0]["TABLE_NAME"].ToString();

                    // Thực hiện truy vấn để lấy dữ liệu từ sheet
                    string query = $"SELECT * FROM [{sheetName}]";
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Hiển thị dữ liệu trên DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
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

        }



}

