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
                string fullname = dt.Rows[0]["fname"].ToString() +" " +dt.Rows[0]["lname"].ToString();
                byte[] pic = (byte[])dt.Rows[0]["picture"];
                MemoryStream pictue = new MemoryStream(pic);
                pictureBox1.Image = Image.FromStream(pictue);
                label1.Text = "Welcome Back, " +fullname;
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
            errorProvider1.Clear();
            errorProvider2.Clear();
            Group group = new Group();
            // Lấy giá trị từ các ô textbox
            string groupname = textBoxGroupName.Text;
            string groupid = textBoxGroupID.Text;

            // Kiểm tra nếu trường dữ liệu groupname là rỗng
            if (string.IsNullOrEmpty(groupname))
            {
                // Thiết lập thông báo lỗi cho ErrorProvider
                errorProvider1.SetError(textBoxGroupName, "Group name is required.");
                // Dừng lại và không tiếp tục xử lý
                return;
            }

            // Kiểm tra nếu trường dữ liệu groupid là rỗng
            else if (string.IsNullOrEmpty(groupid))
            {
                // Thiết lập thông báo lỗi cho ErrorProvider
                errorProvider2.SetError(textBoxGroupID, "Group ID is required.");
                // Dừng lại và không tiếp tục xử lý
                return;
            }
    
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
            if(textBoxID.Text=="")
            {
                MessageBox.Show("Vui long chon ID");
            }
            else if (contact.deleteContact(textBoxID.Text))
            {
                MessageBox.Show("ThanhCOng");
            } 
                
        }
        Group group = new Group();
        private void button8_Click(object sender, EventArgs e)
        {
            errorProvider3.Clear();
            string newname = textBox3.Text;
            if(string.IsNullOrEmpty(newname))
            {
                errorProvider3.SetError(textBox3, "Vui long nhap");
                return;
            }
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
                HRForm_Load(sender,e);
                textBoxGroupID.Text = string.Empty;
                textBox3.Text = string.Empty;
                textBoxGroupName.Text = string.Empty;
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
            HRShowFull hRShowFull = new HRShowFull();   
            hRShowFull.ShowDialog();
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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HRForm_Load(sender, e);
            textBoxGroupName.Text = string.Empty;
            textBoxID.Text = string.Empty;
           textBoxGroupID.Text = string.Empty;
            textBox3.Text = string.Empty;
     
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EditIFHR editIFHR = new EditIFHR(); 
            editIFHR.ShowDialog();
            HRForm_Load(sender, e);
        }
    }
}
