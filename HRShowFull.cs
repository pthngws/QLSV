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
    public partial class HRShowFull : Form
    {
        Contact contac = new Contact();
        Group group = new Group();
        MY_DB mydb = new MY_DB();
        public HRShowFull()
        {
            InitializeComponent();

           
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string groupID = listBox1.SelectedValue.ToString();

            SqlCommand command = new SqlCommand("SELECT contact.id as 'ID Contact', fname as 'First Name', lname as 'Last Name', name as 'Group Name', phone as 'Phone', email as 'Email', address as Address, picture as Picture, contact.userid as 'ID User' FROM contact,[group] WHERE [group].id = contact.groupid and [contact].userid = @id and groupid = @gid", mydb.getConnection);
            command.Parameters.Add("@id", SqlDbType.VarChar).Value = Global.GlobalUserID;
            command.Parameters.Add("@gid", SqlDbType.VarChar).Value = groupID;
            dataGridView1.DataSource = contac.GetContact(command);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            HRShowFull_Load(null, null);
        }

        private void HRShowFull_Load(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("SELECT contact.id as 'ID Contact', fname as 'First Name', lname as 'Last Name', name as 'Group Name', phone as 'Phone', email as 'Email', address as Address, picture as Picture, contact.userid as 'ID User' FROM contact,[group] WHERE [group].id = contact.groupid and [contact].userid = @id", mydb.getConnection);
            command.Parameters.Add("@id", SqlDbType.VarChar).Value = Global.GlobalUserID;
            dataGridView1.DataSource = contac.GetContact(command);
            dataGridView1.RowTemplate.Height = 80;
            DataGridViewImageColumn piccol = new DataGridViewImageColumn();
            piccol = (DataGridViewImageColumn)dataGridView1.Columns[7];
            piccol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            listBox1.DataSource = group.getGroups(Global.GlobalUserID);
            listBox1.DisplayMember = "name";
            listBox1.ValueMember = "id";
            listBox1.SelectedItem = null;
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            string fullname = dataGridView1.CurrentRow.Cells[1].Value.ToString() + " " + dataGridView1.CurrentRow.Cells[2].Value.ToString();
            ContactCourse contactCourse = new ContactCourse(dataGridView1.CurrentRow.Cells[0].Value.ToString(),fullname);
            contactCourse.Show();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }
    }
}
