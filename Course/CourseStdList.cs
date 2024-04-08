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
    public partial class CourseStdList : Form
    {
        string name;
        string semester;
        public CourseStdList()
        {
            InitializeComponent();
        }
        public CourseStdList(string name,string semester)
        {
            InitializeComponent();
            this.name = name;
            this.semester = semester;
        }

        private void CourseStdList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'baiTapWinformDataSet11.std' table. You can move, or remove it, as needed.
            this.stdTableAdapter.Fill(this.baiTapWinformDataSet11.std);
            // TODO: This line of code loads data into the 'baiTapWinformDataSet9.std' table. You can move, or remove it, as needed.
            textBoxCourseID.Text = name;
            textBox1.Text = semester;
            SqlCommand cmd = new SqlCommand("SELECT * FROM std WHERE selected_course LIKE '%' + @name + '%'");
            cmd.Parameters.AddWithValue("@name", name);
            fillGrid(cmd);
        }
        STUDENT student = new STUDENT();
        public void fillGrid(SqlCommand command)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 80;
            dataGridView1.DataSource = student.getStudents(command);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
        }
    }
}
