using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QLSV
{
    
    internal class HR
    {
        MY_DB mydb = new MY_DB();
        public bool ApproveHR(string username)
        {
            string status = "Da xac thuc";

            SqlCommand sql = new SqlCommand("UPDATE [hr] SET status = @status WHERE username = @username", mydb.getConnection);

            mydb.openConnection();
            sql.Parameters.AddWithValue("@status", status);
            sql.Parameters.AddWithValue("@username", username);
            int rowsAffected = sql.ExecuteNonQuery();
            mydb.closeConnection();
            return rowsAffected > 0;

        }
        public bool updateHR(string id,string fname, string lname, MemoryStream picture)
        {


            SqlCommand command = new SqlCommand("UPDATE hr SET fname = @fn, lname = @ln,picture = @pic WHERE username = @id", mydb.getConnection);
            mydb.openConnection();
            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            command.Parameters.Add("@fn", SqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", SqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@pic", SqlDbType.Image).Value = picture.ToArray();

            if (command.ExecuteNonQuery() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool deleteHR(string username)
        {
            mydb.closeConnection();

            mydb.openConnection();
            SqlCommand command = new SqlCommand("DELETE FROM hr WHERE username = @username", mydb.getConnection);
            command.Parameters.AddWithValue("@username", username);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public DataTable getHRById(Int32 userid)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("Select * from hr where id = @id", mydb.getConnection);
            cmd.Parameters.AddWithValue("@id", userid);
            sqlDataAdapter.SelectCommand = cmd;
            sqlDataAdapter.Fill(dt);
            return dt;
        }
        public DataTable getHRs(SqlCommand command)
        {
            command.Connection = mydb.getConnection;
            mydb.closeConnection();
            mydb.openConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable TABLE = new DataTable();
            adapter.Fill(TABLE);
            return TABLE;

        }
        public bool insertHR(string fname,string lname,string user,string pass,MemoryStream picture)
        {
            SqlCommand cmd = new SqlCommand("insert into hr (fname,lname,username,password,picture) values (@fn,@ln,@un,@pw,@img)", mydb.getConnection);
            cmd.Parameters.AddWithValue("@fn", fname);
            cmd.Parameters.AddWithValue("@ln", lname);
            cmd.Parameters.AddWithValue("@un", user);
            cmd.Parameters.AddWithValue("@pw", pass);
            cmd.Parameters.Add("@img", SqlDbType.Image).Value = picture.ToArray();
            mydb.openConnection();

            if(cmd.ExecuteNonQuery()==1)
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }


        }
        public bool usernameExist(string user)
        {
            string query = "select * from hr where username = @usn";
            SqlCommand cmd = new SqlCommand(query,mydb.getConnection);
            cmd.Parameters.AddWithValue ("@usn", user);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            adapter.SelectCommand = cmd;
            adapter.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                return true;
            }
            return false;


        }
        public bool LoginHr(string username, string password)
        {
            // Chuỗi kết nối đến cơ sở dữ liệu của bạn


            // Tạo kết nối đến cơ sở dữ liệu

                
                // Tạo truy vấn SQL
                string query = "SELECT COUNT(*) FROM Hr WHERE Username = @Username AND Password = @Password and Status = @stt";

                // Tạo đối tượng SqlCommand để thực thi truy vấn
                using (SqlCommand command = new SqlCommand(query, mydb.getConnection))
                {
                mydb.openConnection();
                    // Thêm tham số vào truy vấn để tránh tình trạng SQL injection
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@stt", "Da xac thuc");

                // Thực thi truy vấn và trả về số lượng hàng tương ứng
                int count = (int)command.ExecuteScalar();

                    // Đóng kết nối
                    mydb.closeConnection();

                    // Nếu số lượng hàng tương ứng là 1, tức là có người dùng có tên người dùng và mật khẩu tương ứng
                    if (count == 1)
                    {
                        return true; // Đăng nhập thành công
                    }
                    else
                    {
                        return false; // Đăng nhập thất bại
                    }
                }
            }
        }
    }

