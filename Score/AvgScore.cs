using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class AvgScore : Form
    {
        Score score = new Score();
        public AvgScore()
        {
            InitializeComponent();
        }

        private void AvgScore_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'baiTapWinformDataSet10.Score' table. You can move, or remove it, as needed.
            dataGridView1.DataSource = score.getAvgScoreByCourse();
        }



    }
}
