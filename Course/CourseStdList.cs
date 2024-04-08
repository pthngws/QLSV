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
        string id;
        public CourseStdList()
        {
            InitializeComponent();
        }
        public CourseStdList(string name,string semester, string id)
        {
            InitializeComponent();
            this.name = name;
            this.semester = semester;
            this.id = id;
        }

        private void CourseStdList_Load(object sender, EventArgs e)
        {

            // TODO: This line of code loads data into the 'baiTapWinformDataSet9.std' table. You can move, or remove it, as needed.
            textBoxCourseID.Text = name;
            textBox1.Text = semester;
            SqlCommand cmd = new SqlCommand("SELECT * FROM std WHERE selected_course LIKE '%' + @id + '%'");
            cmd.Parameters.AddWithValue("@id", id);
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

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            printDialog1 = new PrintDialog();
            printDocument1.DocumentName = " Print Document";
            printDialog1.Document = printDocument1;
            printDialog1.AllowSelection = true;
            printDialog1.AllowSomePages = true;
            if (printDialog1.ShowDialog() == DialogResult.OK) { printDocument1.Print(); }
        }
    }
}
