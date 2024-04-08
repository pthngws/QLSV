using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;

namespace QLSV
{
    class STUDENT
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
        public List<string> selectedCourseStudent(string id)
        {
            SqlCommand cmd = new SqlCommand("select selected_course from std where id =@id",mydb.getConnection);
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
            SqlCommand command = new SqlCommand("update std set selected_course = @course where id =@id", mydb.getConnection);
            command.Parameters.AddWithValue("@course", course);
            command.Parameters.AddWithValue("@id", id);
            mydb.closeConnection();
            mydb.openConnection();
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        //  function to insert a new student
        public bool insertStudent(int Id, string fname, string lname, DateTime bdate, string gender, string phone, string address, MemoryStream picture)
        {
            // Check if the Id already exists
            if (studentIdExists(Id))
            {
                // Id already exists, handle the error or return false 
                return false;
            }

            // Id does not exist, proceed with the insertion
            SqlCommand command = new SqlCommand("INSERT INTO std (id, fname, lname, bdate, gender, phone, address, picture)" +
                " VALUES (@id,@fn, @ln, @bdt, @gdr, @phn, @adrs, @pic)", mydb.getConnection);

            command.Parameters.Add("@id", SqlDbType.Int).Value = Id;
            command.Parameters.Add("@fn", SqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", SqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bdt", SqlDbType.DateTime).Value = bdate;
            command.Parameters.Add("@gdr", SqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@phn", SqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adrs", SqlDbType.VarChar).Value = address;
            command.Parameters.Add("@pic", SqlDbType.Image).Value = picture.ToArray();

            mydb.openConnection();

            if ((command.ExecuteNonQuery() == 1))
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

        // Helper method to check if the student Id already exists
        private bool studentIdExists(int studentId)
        {
            mydb.openConnection();
            SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM std WHERE id = @id", mydb.getConnection);
            checkCommand.Parameters.Add("@id", SqlDbType.Int).Value = studentId;

            int count = (int)checkCommand.ExecuteScalar();
            mydb.closeConnection();

            return count > 0;
        }

        public DataTable getStudents(SqlCommand command)
        {
            command.Connection = mydb.getConnection;
            mydb.closeConnection();
            mydb.openConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable TABLE = new DataTable();
            adapter.Fill(TABLE);
            return TABLE;

        }
        public bool updateStudent(SqlCommand command)
        {
            mydb.getConnection.Close();
                command.Connection = mydb.getConnection;
                mydb.getConnection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
        }
        public bool deleteStudent(int studentID)
        {
            mydb.getConnection.Close();
            mydb.getConnection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM std WHERE id = @StudentID", mydb.getConnection);
                command.Parameters.AddWithValue("@StudentID", studentID);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
           
        }
        string exeCount(string query)
        {
            SqlCommand command = new SqlCommand(query, mydb.getConnection);
            mydb.openConnection();
            String count = command.ExecuteScalar().ToString();
            mydb.closeConnection();
            return count;
        }
        public string totalStudent()
        {
            return exeCount("Select count(*) from std");
        }
        public string totalMaleStudent()
        {
            return exeCount("Select count(*) from std where gender ='male'");
        }
        public string totalFemaleStudent()
        {
            return exeCount("Select count(*) from std where gender ='female'");
        }
    }
}
