using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class Approve : Form
    {
        public Approve()
        {
            InitializeComponent();
        }

        private void Approve_Load(object sender, EventArgs e)
        {
            // Load dữ liệu dựa trên radio button được chọn
            if (radioButtonSTD.Checked)
            {
                // Load dữ liệu vào bảng log_in
                this.log_inTableAdapter.Fill(this.baiTapWinformDataSet6.log_in);
                // Thiết lập DataSource của DataGridView thành bảng log_in trong dataset
                dataGridView2.DataSource = this.baiTapWinformDataSet6.log_in;
            }
            else if (radioButtonHR.Checked)
            {
                // Load dữ liệu vào bảng HR
                this.hRTableAdapter.Fill(this.baiTapWinformDataSet13.HR);
                // Thiết lập DataSource của DataGridView thành bảng HR trong dataset
                dataGridView2.DataSource = this.baiTapWinformDataSet13.HR;
            }
        }



        string username;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             username = dataGridView2.CurrentRow.Cells[0].Value.ToString();

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show(username);
            if (radioButtonSTD.Checked)
            {        User user = new User();
     

                    if (user.ApproveUser(username))
                    {
                        MessageBox.Show("Đã thêm thành công!");
                        Approve_Load(sender, e);
                    }

            }
            else if(radioButtonHR.Checked)
            {
                HR hr = new HR();

                    if (hr.ApproveHR(username))
                    {
                        MessageBox.Show("Đã thêm thành công!");
                        
                        Approve_Load(sender, e);
                    }

            }
        }


        private void buttonDelete_Click(object sender, EventArgs e)
        {
            MessageBox.Show(username);
            if (radioButtonSTD.Checked)
            {
                User user = new User();
                if (user.deleteUser(user.Username))
                    {
                        MessageBox.Show("Đã xóa thành công!");




                        Approve_Load(sender, e);
                    }
              }

            else if (radioButtonHR.Checked) {

                HR hr = new HR();
                if (hr.deleteHR(username))
                    {
                        MessageBox.Show("Đã xóa thành công!");
                        hr = null;
                        Approve_Load(sender, e);
                    }
                }

        }

        private void radioButtonSTD_CheckedChanged(object sender, EventArgs e)
        {
            Approve_Load(sender, e);
        }
    }
}
