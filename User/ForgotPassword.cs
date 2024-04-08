using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace QLSV
{
    public partial class ForgotPassword : Form
    {
        private const int CountdownDurationSeconds = 30; // Thời gian đếm ngược là 60 giây
        private int remainingSeconds = CountdownDurationSeconds;
        private Timer countdownTimer;
        private bool isCounting = false;

        Random random = new Random();
        int otp;

        public ForgotPassword()
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

        private void buttonSendOTP_Click(object sender, EventArgs e)
        {
            if (!isCounting)
            {
                if (textBoxEmail.Text == "")
                {
                    errorProvider1.SetError(textBoxEmail, "Vui lòng nhập Email");
                }
                else
                {
                    remainingSeconds = CountdownDurationSeconds;
                    countdownTimer.Start();
                    isCounting = true;

                    otp = random.Next(100000, 1000000);

                    string sendMailFrom = "pthocwinform@gmail.com";
                    string sendMailTo = textBoxEmail.Text;
                    string sendMailSubject = "OTP Code";
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
                        smtpServer.Timeout = 10000;
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

                    // Đặt lại text và trạng thái của nút gửi OTP
                    buttonSendOTP.Enabled = false;
                    buttonSendOTP.Text = $" ({remainingSeconds}s)";
                }
            }
        }

        private void buttonVerify_Click(object sender, EventArgs e)
        {
            if (textBoxOTP.Text == otp.ToString())
            {
                ResetPassword reset = new ResetPassword(textBoxEmail.Text);
                reset.Show();
            }
        }
    }
}
