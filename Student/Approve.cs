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
            // TODO: This line of code loads data into the 'baiTapWinformDataSet6.log_in' table. You can move, or remove it, as needed.
            this.log_inTableAdapter.Fill(this.baiTapWinformDataSet6.log_in);
            // TODO: This line of code loads data into the 'baiTapWinformDataSet1.Approve' table. You can move, or remove it, as needed.
            this.approveTableAdapter.Fill(this.baiTapWinformDataSet1.Approve);

        }

        private User user;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             string username = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            user = new User(username);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (user == null || user.Username == null)
            {
                MessageBox.Show("Vui lòng chọn user");
            }
            else
            {
                if (user.ApproveUser())
                {
                    MessageBox.Show("Đã thêm thành công!");
                    user.Username = null;
                    user.Password = null;
                    user.Email = null;
                    user.Role = null;
                    this.approveTableAdapter.Fill(this.baiTapWinformDataSet1.Approve);
                    Approve_Load(sender, e);
                }
            }
        }


        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (user == null || user.Username == null)
            {
                MessageBox.Show("Vui lòng chọn user");
            }
            else
            {
                if (user.deleteUser(user.Username))
                {
                    MessageBox.Show("Đã xóa thành công!");

                    // Clear the properties of the existing User object
                    user.Username = null;
                    user.Password = null;
                    user.Email = null;
                    user.Role = null;

                    this.approveTableAdapter.Fill(this.baiTapWinformDataSet1.Approve);
                    Approve_Load(sender, e);
                }
            }
        }
    }
}
