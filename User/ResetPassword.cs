using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class ResetPassword : Form
    {
        public ResetPassword()
        {
            InitializeComponent();
        }
        string email;
        public ResetPassword(string email)
        {
            InitializeComponent();
            this.email = email;
        }

        private void ResetPassword_Load(object sender, EventArgs e)
        {

        }
        public bool checkPassword(string a,string b)
        {
            if(a == null || b == null)
            {
                return false;
            }
            if(a!=b)
            {
                return false;
            }
            return true;
        }
        MY_DB mydb = new MY_DB();
        private void buttonReset_Click(object sender, EventArgs e)
        {
            string password = TextBoxPassword.Text;
            string repass = TextBoxConfirmPassword.Text;
            if(checkPassword(password,repass))
            {
                SqlCommand cmd = new SqlCommand("UPDATE log_in SET password = @password WHERE email = @email",mydb.getConnection);
                mydb.openConnection();
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@email", email); // Sử dụng giá trị email thay thế cho @email

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Mật khẩu đã được cập nhật thành công.");
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tài khoản với email đã cho.");
                }
            }
            else
            {
                MessageBox.Show("Nhập lại mật khẩu!");
            }
        }
    }
}
