using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QLSV
{
    internal class Contact
    {
        MY_DB mydb = new MY_DB();
        static List<string> StringToList(string input, char delimiter)
        {
            // Tạo một danh sách mới để lưu trữ các phần tử
            List<string> resultList = new List<string>();

            // Chia chuỗi thành các phần tử, sử dụng ký tự phân cách
            string[] elements = input.Split(delimiter);

            // Thêm các phần tử vào danh sách kết quả
            foreach (string element in elements)
            {
                resultList.Add(element.Trim()); // Trim() để loại bỏ khoảng trắng nếu có
            }

            return resultList;
        }
        public List<string> selectedCourse(string id)
        {
            SqlCommand cmd = new SqlCommand("select selected_course from contact where id =@id", mydb.getConnection);
            cmd.Parameters.AddWithValue("@id", id);
            mydb.closeConnection();
            mydb.openConnection();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string course = dr["selected_course"].ToString();
                return StringToList(course, ',');

            }
            return null;

        }
        public bool updateSelectedCourse(string id, string course)
        {
            SqlCommand command = new SqlCommand("update contact set selected_course = @course where id =@id", mydb.getConnection);
            command.Parameters.AddWithValue("@course", course);
            command.Parameters.AddWithValue("@id", id);
            mydb.closeConnection();
            mydb.openConnection();
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public bool insertContact(string id,string fn,string ln, string phone, string addres,string email,string uid,string groupid,MemoryStream picture)
        {
            SqlCommand cmd = new SqlCommand("insert into contact (id,fname,lname,groupid,phone,email,address,picture,userid) values (@id,@fn,@ln,@groupid,@phone,@email,@addr,@pic,@uid)", mydb.getConnection);
            mydb.openConnection();
            cmd.Parameters.Add("@fn",SqlDbType.VarChar).Value = fn;
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
            cmd.Parameters.Add("@ln", SqlDbType.VarChar).Value = ln;
            cmd.Parameters.Add("@groupid", SqlDbType.VarChar).Value = groupid;
            cmd.Parameters.Add("@phone", SqlDbType.VarChar).Value = phone;
            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
            cmd.Parameters.Add("@addr", SqlDbType.VarChar).Value = addres;
            cmd.Parameters.Add("@pic", SqlDbType.Image).Value = picture.ToArray();
            cmd.Parameters.Add("@uid", SqlDbType.VarChar).Value = uid;
            if (cmd.ExecuteNonQuery() == 1)
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
        // Update tương tự
        public bool updateContact(string contactId, string fname, string lname, string phone, string address, string email, string groupId, MemoryStream picture)
        {


                    SqlCommand command = new SqlCommand("UPDATE contact SET fname = @fn, lname = @ln, groupid = @gid, phone = @phn, email = @mail, address = @adrs, picture = @pic WHERE id = @id", mydb.getConnection);
            mydb.openConnection();
            command.Parameters.Add("@id", SqlDbType.Int).Value = contactId;
                    command.Parameters.Add("@fn", SqlDbType.VarChar).Value = fname;
                    command.Parameters.Add("@ln", SqlDbType.VarChar).Value = lname;
                    command.Parameters.Add("@gid", SqlDbType.VarChar).Value = groupId;
                    command.Parameters.Add("@phn", SqlDbType.VarChar).Value = phone;
                    command.Parameters.Add("@mail", SqlDbType.VarChar).Value = email;
                    command.Parameters.Add("@adrs", SqlDbType.VarChar).Value = address;
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
        public bool deleteContact(string contactId)
        {
            SqlCommand command = new SqlCommand("Delete from contact where id = @id", mydb.getConnection);
            command.Parameters.Add("@id",SqlDbType.VarChar).Value = contactId;
            mydb.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // Nạp dữ liệu
        public DataTable SelectContactList(SqlCommand command)
        {
            command.Connection = mydb.getConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public DataTable GetContactById(string contactId)
        {
            SqlCommand command = new SqlCommand("SELECT id, fname, lname, groupid, phone, email, address, picture, userid FROM contact WHERE id = @id", mydb.getConnection);
            command.Parameters.Add("@id", SqlDbType.VarChar).Value = contactId;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public DataTable GetContact(SqlCommand command)
        {


            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

    }
}



