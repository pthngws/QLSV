using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace QLSV
{
    internal class Course
    {
        MY_DB mydb = new MY_DB();

        public string Id { get; set; }
        public string Label { get; set; }
        public int? Period { get; set; }
        public string Description { get; set; }
        public string Semester { get; set; }


        //  function to insert a new student
        public bool courseIdExists(string id)
        {
            mydb.openConnection();
            SqlCommand sqlCommand = new SqlCommand("select count(*) from course where id = @id", mydb.getConnection);

            sqlCommand.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
            int count = (int)sqlCommand.ExecuteScalar();
            mydb.closeConnection();
            return count > 0;

        }
        public bool insertCourse(string Id, string label, int period, string description, string semester)
        {
            // Check if the Id already exists
            if (courseIdExists(Id))
            {
                // Id already exists, handle the error or return false 
                return false;
            }

            // Id does not exist, proceed with the insertion
            SqlCommand command = new SqlCommand("INSERT INTO course (id,label,period,description,semester)" +
                " VALUES (@id,@label,@period,@description,@semester)", mydb.getConnection);

            command.Parameters.Add("@id", SqlDbType.VarChar).Value = Id;
            command.Parameters.Add("@label", SqlDbType.VarChar).Value = label;
            command.Parameters.Add("@description", SqlDbType.VarChar).Value = description;
            command.Parameters.Add("@period", SqlDbType.Int).Value = period;
            command.Parameters.Add("@semester", SqlDbType.VarChar).Value = semester;

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
        public DataTable getCourse(SqlCommand command)
        {
            command.Connection = mydb.getConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable TABLE = new DataTable();
            adapter.Fill(TABLE);
            return TABLE;

        }

        // Helper method to check if the student Id already exists
        public DataTable getAllCourses()
        {
            DataTable coursesDataTable = new DataTable();

            string query = "SELECT * FROM Course"; // Thay thế 'Courses' bằng tên bảng thực tế trong cơ sở dữ liệu của bạn

            using (SqlCommand command = new SqlCommand(query, mydb.getConnection))
            {
                mydb.getConnection.Close();
                mydb.getConnection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Tạo các cột trong DataTable tương ứng với cột trong kết quả truy vấn
                coursesDataTable.Columns.Add("Id", typeof(string));
                coursesDataTable.Columns.Add("Label", typeof(string));
                coursesDataTable.Columns.Add("period", typeof(int));
                coursesDataTable.Columns.Add("description", typeof(string));
                coursesDataTable.Columns.Add("semester", typeof(string));


                while (reader.Read())
                {
                    // Tạo một hàng mới cho mỗi bản ghi và thêm vào DataTable
                    DataRow row = coursesDataTable.NewRow();
                    row["Id"] = reader["id"].ToString();
                    row["Label"] = reader["Label"].ToString();
                    row["Period"] = Convert.ToInt32(reader["Period"]);
                    row["Description"] = reader["Description"].ToString();
                    row["semester"] = reader["semester"].ToString();
                    coursesDataTable.Rows.Add(row);
                }

                reader.Close();
            }

            return coursesDataTable;
        }
        public DataTable getAllCoursesBySemester(string semester)
        {
            DataTable coursesDataTable = new DataTable();

            string query = "SELECT * FROM Course WHERE semester = @semester";

            using (SqlCommand command = new SqlCommand(query, mydb.getConnection))
            {
                mydb.getConnection.Close();
                mydb.getConnection.Open();
                command.Parameters.AddWithValue("@semester", semester);
                SqlDataReader reader = command.ExecuteReader();

                // Add columns to the DataTable with correct names and types
                coursesDataTable.Columns.Add("Id", typeof(string));
                coursesDataTable.Columns.Add("Label", typeof(string));
                coursesDataTable.Columns.Add("Period", typeof(int)); // Corrected column name
                coursesDataTable.Columns.Add("Description", typeof(string)); // Corrected column name
                coursesDataTable.Columns.Add("Semester", typeof(string)); // Corrected column name

                while (reader.Read())
                {
                    // Create a new row for each record and add it to the DataTable
                    DataRow row = coursesDataTable.NewRow();
                    row["Id"] = reader["id"].ToString();
                    row["Label"] = reader["Label"].ToString();
                    row["Period"] = Convert.ToInt32(reader["Period"]); // Corrected column name
                    row["Description"] = reader["Description"].ToString(); // Corrected column name
                    row["Semester"] = reader["semester"].ToString(); // Corrected column name
                    coursesDataTable.Rows.Add(row);
                }

                reader.Close();
            }

            return coursesDataTable;
        }



        public DataTable getCourseById(string Id)
        {

            SqlCommand command = new SqlCommand("select * from course where id=@id");
            command.Parameters.Add("id", SqlDbType.VarChar).Value = Id;
            command.Connection = mydb.getConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable TABLE = new DataTable();
            adapter.Fill(TABLE);
            return TABLE;

        }

        public bool updateCourse(string courseId, string label, int period, string description, string semester)
        {
            SqlCommand command = new SqlCommand("Update course set  label=@label,period=@period, description = @description,semester = @semester where id =@id", mydb.getConnection);
            mydb.getConnection.Close();
            mydb.getConnection.Open();
            command.Parameters.AddWithValue("@ID", courseId);
            command.Parameters.AddWithValue("@label", label);
            command.Parameters.AddWithValue("@period", period);
            command.Parameters.AddWithValue("@description", description);
            command.Parameters.AddWithValue("@semester", semester);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
        public bool deleteCourse(string courseID)
        {
            mydb.getConnection.Close();
            mydb.getConnection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM Course WHERE id = @courseID", mydb.getConnection);
            command.Parameters.AddWithValue("@CourseID", courseID);

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
        public string totalCourse()
        {
            return exeCount("Select count(*) from Course");
        }
    }
}
