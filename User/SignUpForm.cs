using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace QLSV
{
    public partial class SignUpForm : Form
    {
        private const int CountdownDurationSeconds = 60; // Thời gian đếm ngược là 60 giây
        private int remainingSeconds = CountdownDurationSeconds;
        private Timer countdownTimer;
        private bool isCounting = false;

        Random random = new Random();
        int otp;

        public SignUpForm()
        {
            InitializeComponent();
            InitializeTimer();
        }

        // Khởi tạo Timer
        private void InitializeTimer()
        {
            countdownTimer = new Timer();
            countdownTimer.Interval = 1000; // Mỗi giây
            countdownTimer.Tick += CountdownTimer_Tick;
        }

        // Xử lý sự kiện Tick của Timer
        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            remainingSeconds--;
            if (remainingSeconds <= 0)
            {
                countdownTimer.Stop();
                isCounting = false;
                buttonSendOTP.Enabled = true;
                buttonSendOTP.Text = "Send OTP";
            }
            else
            {
                buttonSendOTP.Text = $"({remainingSeconds}s)";
            }
        }

        private void ButtonSignUp_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
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
            if (TextBoxConfirmPassword.Text == "")
            {
                errorProvider3.SetError(TextBoxConfirmPassword, "Vui lòng nhập mật khẩu");
                flag = false;
            }
            if (textBoxEmail.Text == "")
            {
                errorProvider4.SetError(textBoxEmail, "Vui lòng nhập email");
                flag = false;
            }
            if (textBoxOTP.Text == "")
            {
                errorProvider5.SetError(textBoxOTP, "Vui lòng nhập otp");
                flag = false;
            }

            if (flag)
            {
                string username = TextBoxUsername.Text;
                string password = TextBoxPassword.Text;
                string confirmPassword = TextBoxConfirmPassword.Text;
                string email = textBoxEmail.Text;
                string role = "user";
                string status = "Dang cho";

                if (password == confirmPassword)
                {
                    try
                    {
                        if (otp.ToString().Equals(textBoxOTP.Text))
                        {
                            User user = new User(username, password, email, role,status);
                            user.CreateUser();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Mã OTP không chính xác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Tên đăng nhập đã tồn tại hoặc có lỗi xảy ra.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu và xác nhận mật khẩu không trùng khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSendOTP_Click(object sender, EventArgs e)
        {
            if (!isCounting)
            {
                if (textBoxEmail.Text == "")
                {
                    errorProvider4.SetError(textBoxEmail, "Vui lòng nhập Email");
                }
                else
                {
                    remainingSeconds = CountdownDurationSeconds;
                    countdownTimer.Start();
                    isCounting = true;

                    otp = random.Next(100000, 1000000);

                    string sendMailFrom = "pthocwinform@gmail.com";
                    string sendMailTo = textBoxEmail.Text;
                    string sendMailSubject = "OTP COde";
                    string sendMailBody = otp.ToString();

                    try
                    {
                        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com", 587)
                        {
                            DeliveryMethod = SmtpDeliveryMethod.Network
                        };
                        MailMessage email = new MailMessage
                        {
                            // START
                            From = new MailAddress(sendMailFrom)
                        };
                        email.To.Add(sendMailTo);
                        email.CC.Add(sendMailFrom);
                        email.Subject = sendMailSubject;
                        email.Body = sendMailBody;
                        //END
                        smtpServer.Timeout = 105000;
                        smtpServer.EnableSsl = true;
                        smtpServer.UseDefaultCredentials = false;
                        smtpServer.Credentials = new NetworkCredential(sendMailFrom, "wuoq bkqm avnu ixvf");
                        smtpServer.Send(email);

                        MessageBox.Show("OTP đã được gửi.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
