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
    public partial class RemoveScore : Form
    {
        public RemoveScore()
        {
            InitializeComponent();
        }
        Score score = new Score();
        private void RemoveScore_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = score.getAllScore();
        }
        string stdID;
        string courseID;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
             stdID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
             courseID = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (stdID == "" || courseID == "")
            {
                MessageBox.Show("Vui lòng chọn mục cần xóa.");
                
            }
            else {
                if (score.deleteScore(stdID, courseID))
                    MessageBox.Show("Thanh cong");
                else MessageBox.Show("That bai");
            }
            RemoveScore_Load(sender, e);
        }
    }
}
