using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class Login_Form : Form
    {
        public Login_Form()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void btt_Login_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            bool flag = true;
            if (TextBoxUsername.Text == "")
            {
                errorProvider1.SetError(TextBoxUsername, "Vui lòng nhập tên đăng nhập");
                flag = false;
            }
            if (TextBoxPassword.Text == "")
            {
                errorProvider2.SetError(TextBoxPassword, "Vui lòng nhập mật khẩu");
                flag = false;
            }
            if (flag)
            {
                User user = new User(TextBoxUsername.Text, TextBoxPassword.Text);
                if (user.LoginUser() != null)
                {
                    MainForm01 main = new MainForm01(user.LoginUser());
                    main.Show();
                    this.Hide();
                }
            }
        }

        

        private void linkLabel_SignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignUpForm form = new SignUpForm();
            form.Show();
            this.Hide();
        }

        private void btt_Cancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgotPassword forgotPassword = new ForgotPassword();   
            forgotPassword.Show();
        }
    }
}
