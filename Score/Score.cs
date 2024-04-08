using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV
{

    public class Score
    {
        MY_DB MY_DB = new MY_DB();
        public bool insertOrUpdateScore(string studentid, string courseid, float scorevalue, string description)
        {
            // Kiểm tra xem cặp studentid và courseid đã tồn tại chưa
            SqlCommand selectCommand = new SqlCommand("SELECT COUNT(*) FROM score WHERE studentid = @sid AND courseid = @cid", MY_DB.getConnection);
            selectCommand.Parameters.AddWithValue("@sid", studentid);
            selectCommand.Parameters.AddWithValue("@cid", courseid);

            MY_DB.openConnection();
            int existingCount = (int)selectCommand.ExecuteScalar();
            MY_DB.closeConnection();

            if (existingCount > 0)
            {
                // Nếu đã tồn tại, thực hiện cập nhật điểm
                SqlCommand updateCommand = new SqlCommand("UPDATE score SET student_score = @scr, description = @descr WHERE studentid = @sid AND courseid = @cid", MY_DB.getConnection);
                updateCommand.Parameters.AddWithValue("@sid", studentid);
                updateCommand.Parameters.AddWithValue("@cid", courseid);
                updateCommand.Parameters.AddWithValue("@scr", scorevalue);
                updateCommand.Parameters.AddWithValue("@descr", description);

                MY_DB.openConnection();
                if (updateCommand.ExecuteNonQuery() == 1)
                {
                    MY_DB.closeConnection();
                    return true;
                }
                MY_DB.closeConnection();
                return false;
            }
            else
            {
                // Nếu chưa tồn tại, thực hiện thêm mới điểm
                SqlCommand insertCommand = new SqlCommand("INSERT INTO score (studentid, courseid, student_score, description) VALUES (@sid, @cid, @scr, @descr)", MY_DB.getConnection);
                insertCommand.Parameters.AddWithValue("@sid", studentid);
                insertCommand.Parameters.AddWithValue("@cid", courseid);
                insertCommand.Parameters.AddWithValue("@scr", scorevalue);
                insertCommand.Parameters.AddWithValue("@descr", description);

                MY_DB.openConnection();
                if (insertCommand.ExecuteNonQuery() == 1)
                {
                    MY_DB.closeConnection();
                    return true;
                }
                MY_DB.closeConnection();
                return false;
            }
        }

        public bool studentScoreExist(string studentid, string courseid)
        {
            SqlCommand command = new SqlCommand("select * from score where student_id = @sid and courseid = @cid", MY_DB.getConnection);
            command.Parameters.Add("@sid", SqlDbType.VarChar).Value = studentid;
            command.Parameters.Add("@cid", SqlDbType.VarChar).Value = courseid;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if ((dt.Rows.Count == 0))
                return false;
            return true;
        }
        public DataTable getAllScore()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = MY_DB.getConnection;
            command.CommandText = "select * from score";

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            MY_DB.closeConnection();
            return dataTable;

        }
        public DataTable getAvgScoreByCourse()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = MY_DB.getConnection;
            command.CommandText = "select courseID, avg(student_score) as AverageGrade from score group by courseID";

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            return dataTable;

        }
        public bool deleteScore(string studentid, string courseid)
        {
            SqlCommand command = new SqlCommand("Delete from score where studentid = @sid and courseid =@cid", MY_DB.getConnection);
            command.Parameters.AddWithValue("@sid", SqlDbType.VarChar).Value = studentid;
            command.Parameters.AddWithValue("@cid", SqlDbType.VarChar).Value = courseid;
            MY_DB.openConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                return true;

            }
            return false;
        }

    }
}
