using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV
{
    internal class Group
    {
        MY_DB mydb= new MY_DB();
        public bool insertGroup(string id,string name,string uid)
        {
            SqlCommand cmd = new SqlCommand("Insert into [Group] (id,name,userid) values (@id,@name,@uid)",mydb.getConnection);
            cmd.Parameters.Add("@id",SqlDbType.VarChar).Value = id;
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
            cmd.Parameters.Add("@uid", SqlDbType.VarChar).Value = uid;
            mydb.openConnection();
            if (cmd.ExecuteNonQuery() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool updateGroup(string id,string name)
        {
            SqlCommand cmd = new SqlCommand("Update [group] set name=@name where id = @id", mydb.getConnection);
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
            cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
            mydb.openConnection();
            if (cmd.ExecuteNonQuery() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool deleteGroup(string id)
        {
            SqlCommand cmd = new SqlCommand("delete from [group] where id = @id", mydb.getConnection);
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
            mydb.openConnection();
            if (cmd.ExecuteNonQuery() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable getGroups(string uid)
        {
            SqlCommand cmd = new SqlCommand("Select * from [group] where userid = @uid",mydb.getConnection);
            cmd.Parameters.Add("@uid", SqlDbType.VarChar).Value = uid;
            mydb.openConnection();
            SqlDataAdapter mydataAdapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            mydataAdapter.Fill(table);
            return table;
        }
    }
}
