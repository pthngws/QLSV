using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class PrintCourse : Form
    {
        public PrintCourse()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Tạo một đối tượng SaveFileDialog
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            // Thiết lập các thuộc tính của SaveFileDialog
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            // Mở hộp thoại và kiểm tra nếu người dùng đã chọn vị trí lưu
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn mà người dùng đã chọn
                string path = saveFileDialog1.FileName;

                // Ghi dữ liệu vào tệp
                using (var writer = new StreamWriter(path))
                {
                    DateTime bdate;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count - 1; j++)
                        {
                            if (j == 3)
                            {
                                bdate = Convert.ToDateTime(dataGridView1.Rows[i].Cells[j].Value.ToString());
                                writer.Write("\t" + bdate.ToString("yyyy-MM-dd") + "\t" + "|");
                            }
                            else if (j == dataGridView1.Columns.Count - 2)
                            {
                                writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString());
                            }
                            else
                            {
                                writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t" + "|");
                            }
                        }
                        writer.WriteLine("");
                        writer.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------");
                    }
                }

                // Hiển thị thông báo khi lưu hoàn tất
                MessageBox.Show("Dữ liệu đã được lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            printDialog1 = new PrintDialog();
            printDocument1.DocumentName = " Print Document";
            printDialog1.Document = printDocument1;
            printDialog1.AllowSelection = true;
            printDialog1.AllowSomePages = true;
            if (printDialog1.ShowDialog() == DialogResult.OK) { printDocument1.Print(); }
        }
        Course course = new Course();
        public void fillGrid(SqlCommand command)
        {
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = course.getCourse(command);
            dataGridView1.AllowUserToAddRows = false;
        }

        private void PrintCourse_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'baiTapWinformDataSet12.Course' table. You can move, or remove it, as needed.
            this.courseTableAdapter1.Fill(this.baiTapWinformDataSet12.Course);
            // TODO: This line of code loads data into the 'baiTapWinformDataSet4.Course' table. You can move, or remove it, as needed.
            SqlCommand command = new SqlCommand("select * from course");
            fillGrid(command);
        }
    }
}
