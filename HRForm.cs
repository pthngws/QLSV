using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class HRForm : Form
    {
        public HRForm()
        {
            InitializeComponent();
        }
        HR hr = new HR();
        private void HRForm_Load(object sender, EventArgs e)
        {
            getImgAndUsername();
            comboBox1.DataSource = group.getGroups(Global.GlobalUserID);
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "id";
            comboBox2.DataSource = group.getGroups(Global.GlobalUserID);
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "id";

        }
        MY_DB mydb = new MY_DB();
        string contactID;
        public void getImgAndUsername()
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select * from hr where username = @us", mydb.getConnection);
                cmd.Parameters.AddWithValue("@us", Global.GlobalUserID);
            sqlDataAdapter.SelectCommand = cmd;
            sqlDataAdapter.Fill(dt);
            if(dt.Rows.Count > 0 )
            {
                byte[] pic = (byte[])dt.Rows[0]["picture"];
                MemoryStream pictue = new MemoryStream(pic);
                pictureBox1.Image = Image.FromStream(pictue);
                label1.Text = "Welcome Back, " + dt.Rows[0]["username"].ToString();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddContactForm addContactForm = new AddContactForm();
            addContactForm.ShowDialog();
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            SelectContact selectContact = new SelectContact();
            selectContact.ShowDialog();
            contactID = selectContact.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxID.Text = contactID;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string groupname = textBoxGroupName.Text;
            string groupid = textBoxGroupID.Text;
            Group group = new Group();
            if(group.insertGroup(groupid, groupname,Global.GlobalUserID))
            {
              
                MessageBox.Show("Thanh cong");
                HRForm_Load(sender, e);

            }    


        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            EditContact editContact = new EditContact();
            editContact.ShowDialog();

        }
        Contact contact = new Contact();
        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (contact.deleteContact(textBoxID.Text))
            {
                MessageBox.Show("ThanhCOng");
            } 
                
        }
        Group group = new Group();
        private void button8_Click(object sender, EventArgs e)
        {
            string newname = textBox3.Text;
            if (group.updateGroup((string)comboBox1.SelectedValue, newname))
            {
                MessageBox.Show("Thanh cong");
                HRForm_Load(sender, e);
            } 
                
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if(group.deleteGroup((string)comboBox1.SelectedValue))
            {
                MessageBox.Show("Thanh cong");
                HRForm_Load(sender, e);

            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonShow_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBoxID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBoxGroupID_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxGroupName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
