using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class StaticForm : Form
    {
        public StaticForm()
        {
            InitializeComponent();
        }

        // Định nghĩa màu sắc
        Color panTotalColor;
        Color panMaleColor;
        Color panFemaleColor;

        // Xử lý sự kiện chuột
        private void LabelTotal_MouseEnter(object sender, EventArgs e)
        {
            LabelTotal.ForeColor = Color.White;
            PanelTotal.BackColor = Color.White;
        }

        private void LabelTotal_MouseLeave(object sender, EventArgs e)
        {
            LabelTotal.ForeColor = Color.White;
            PanelTotal.BackColor = panTotalColor;
        }

        private void LabelMale_MouseEnter(object sender, EventArgs e)
        {
            LabelMale.ForeColor = panTotalColor;
            PanelMale.BackColor = panTotalColor;
        }

        private void LabelMale_MouseLeave(object sender, EventArgs e)
        {
            LabelMale.ForeColor = Color.White;
            PanelMale.BackColor = panMaleColor;
        }

        private void StaticForm_Load(object sender, EventArgs e)
        {
            // Lấy màu của các panel
            panTotalColor = PanelTotal.BackColor;
            panMaleColor = PanelMale.BackColor;
            panFemaleColor = PanelFemale.BackColor;

            // Hiển thị giá trị
            STUDENT student = new STUDENT();
            double total = Convert.ToDouble(student.totalStudent());
            double totalMale = Convert.ToDouble(student.totalMaleStudent());
            double totalFemale = Convert.ToDouble(student.totalFemaleStudent());

            // Tính phần trăm học sinh nam và nữ
            double maleStudentsPercentage = (totalMale * (100 / total));
            double femaleStudentsPercentage = (totalFemale * (100 / total));

            LabelTotal.Text = ("Tổng số học sinh: " + total.ToString());
            LabelMale.Text = ("Nam: " + maleStudentsPercentage.ToString("0.00") + "%");
            LabelFemale.Text = ("Nữ: " + femaleStudentsPercentage.ToString("0.00") + "%");
        }
    }
}
