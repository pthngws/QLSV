using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class Print : Form
    {
        public Print()
        {
            InitializeComponent();
        }
        STUDENT student = new STUDENT();
        private void Print_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'baiTapWinformDataSet8.std' table. You can move, or remove it, as needed.
            this.stdTableAdapter1.Fill(this.baiTapWinformDataSet8.std);
            // TODO: This line of code loads data into the 'baiTapWinformDataSet3.std' table. You can move, or remove it, as needed.
            SqlCommand cmd = new SqlCommand("Select * from std");
            fillGrid(cmd);
            radioButtonNo.Checked = true;
            if(radioButtonNo.Checked )
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }

        }
        
        public void fillGrid(SqlCommand command)
        {
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = student.getStudents(command);
            picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.AllowUserToAddRows = false;
        }

        private void radioButtonYes_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
        }

        private void radioButtonNo_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            SqlCommand command;
            string query;
            if(radioButtonYes.Checked) {
                string date1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string date2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                if(radioButtonMale.Checked)
                {
                    query = "select * from std where gender = 'Male' and bdate between '"+date1+" 'and'"+date2+"'";
                }
                else if (radioButtonFemale.Checked)
                {
                    query = "select * from std where gender = 'Female' and bdate between '" + date1 + " 'and'" + date2 + "'";
                }
                else
                {
                    query = "select * from std where  bdate between '" + date1 + " 'and'" + date2 + "'";
                }
            }
            else
            {
                if(radioButtonMale.Checked)
                {
                    query = "select * from std where gender = 'Male' ";
                }
                else if(radioButtonFemale.Checked)
                {
                    query = "select * from std where gender = 'Female'" ;
                }
                else
                {
                    query = "select * from std";
                }
            }
            command = new SqlCommand(query);
            fillGrid(command);
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument1 = new PrintDocument();
            printDocument1.DocumentName = "Print Document";

            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            PrintDialog printDialog1 = new PrintDialog();
            printDialog1.Document = printDocument1;

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Thực hiện vẽ nội dung cần in ở đây
            e.Graphics.DrawString("Hello, world!", new Font("Arial", 12), Brushes.Black, new PointF(100, 100));
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

    }
}
