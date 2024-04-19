using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
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
    public partial class SelectContact : Form
    {
        public SelectContact()
        {
            InitializeComponent();
        }

        private void SelectContact_Load(object sender, EventArgs e)
        {


            Contact contact = new Contact();
            SqlCommand command = new SqlCommand("SELECT  id ,  fname  as'first name',  lname  as 'last name',  groupid  as'group id' FROM  contact  WHERE  userid  = @uid");
            command.Parameters.Add("@uid", SqlDbType.VarChar).Value = Global.GlobalUserID;
            dataGridView1.DataSource = contact.SelectContactList(command);
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Close();
        }
    }

}
