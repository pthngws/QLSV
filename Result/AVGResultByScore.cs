using Microsoft.ReportingServices.Diagnostics.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace QLSV
{
    public partial class AVGResultBySCore : Form
    {
        MY_DB mydb = new MY_DB();
        public AVGResultBySCore()
        {
            InitializeComponent();
        }
        static bool ContainsNumber(string input)
        {
            foreach (char c in input)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            SqlCommand cmd;

            // Kiểm tra xem chuỗi đầu vào có chứa số không
            if (ContainsNumber(textBoxSearch.Text))
            {
                // Nếu có chứa số, thực hiện truy vấn với điều kiện là id
                cmd = new SqlCommand("SELECT id, fname, lname FROM std WHERE id = @searchTerm", mydb.getConnection);
                // Chuyển đổi chuỗi thành số nguyên trước khi thêm vào tham số
                int searchTermId = Convert.ToInt32(textBoxSearch.Text);
                cmd.Parameters.AddWithValue("@searchTerm", searchTermId);
            }
            else
            {
                // Nếu không chứa số, thực hiện truy vấn với điều kiện là tên (tìm kiếm tương tự)
                cmd = new SqlCommand("SELECT id, fname, lname FROM std WHERE CONCAT(fname, ' ', lname) LIKE '%' + @searchTerm + '%'", mydb.getConnection);
                cmd.Parameters.AddWithValue("@searchTerm", textBoxSearch.Text);
            }

            // Tạo DataTable mới
            DataTable dataTable = new DataTable();
            // Thêm cột id vào DataTable
            dataTable.Columns.Add("id", typeof(int));
            dataTable.Columns.Add("fname", typeof(string));
            dataTable.Columns.Add("lname", typeof(string));

            mydb.openConnection();
            SqlDataReader reader = cmd.ExecuteReader();

            // Lọc dữ liệu từ SqlDataReader dựa trên searchTerm
            while (reader.Read())
            {
                // Tạo một hàng mới và thêm vào DataTable
                DataRow row = dataTable.NewRow();
                row["id"] = reader.GetInt32(0);
                row["fname"] = reader.GetString(1);
                row["lname"] = reader.GetString(2);
                dataTable.Rows.Add(row);
            }
            mydb.closeConnection();

            // Mở lại kết nối để thực hiện các truy vấn tiếp theo
            mydb.openConnection();

            // Thêm cột điểm vào DataTable
            SqlCommand cmdScore = new SqlCommand("SELECT studentid, courseid, student_score FROM score", mydb.getConnection);
            SqlDataReader readerScore = cmdScore.ExecuteReader();

            while (readerScore.Read())
            {
                int studentId = readerScore.GetInt32(0);
                string courseId = readerScore.GetString(1);
                double scoreValue = readerScore.GetDouble(2);

                // Tìm hàng tương ứng với studentId
                DataRow[] rows = dataTable.Select($"id = '{studentId}'");
                if (rows.Length > 0)
                {
                    // Kiểm tra xem cột có tồn tại trong DataTable không
                    if (dataTable.Columns.Contains(courseId))
                    {
                        // Thêm điểm vào cột tương ứng
                        rows[0][courseId] = scoreValue;
                    }
                    else
                    {
                        // Tạo một cột mới với courseId và thêm điểm vào cột đó
                        dataTable.Columns.Add(courseId, typeof(double));
                        rows[0][courseId] = scoreValue;
                    }
                }
               
            }
            readerScore.Close();

            dataTable.Columns.Add("AverageScore", typeof(double));
            dataTable.Columns.Add("Result", typeof(string));

            // Tính điểm trung bình và đánh giá kết quả
            foreach (DataRow row in dataTable.Rows)
            {
                double sumScores = 0;
                int countScores = 0;

                // Tính tổng điểm của từng sinh viên
                foreach (DataColumn column in dataTable.Columns)
                {
                    // Kiểm tra nếu tên cột là course id (nghĩa là đây là cột điểm)
                    if (column.ColumnName != "id" && column.ColumnName != "fname" && column.ColumnName != "lname" && column.ColumnName != "AverageScore" && column.ColumnName != "Result")
                    {
                        if (row[column] != DBNull.Value)
                        {
                            sumScores += Convert.ToDouble(row[column]);
                            countScores++;
                        }
                    }
                }

                // Tính điểm trung bình
                double averageScore = countScores > 0 ? sumScores / countScores : 0;
                row["AverageScore"] = averageScore;

                // Đánh giá kết quả
                row["Result"] = averageScore >= 5 ? "Pass" : "Fail";
            }

            mydb.closeConnection();

            dataGridView1.DataSource = dataTable;
        }


        /*        private void buttonSearch_Click(object sender, EventArgs e)
                {
                    dataGridView1.DataSource = null;
                    string fname ="";
                    string lname ="";
                    int mssv;
                    List<string> list = new List<string>();
                    List<string> columname = new List<string>();
                    SqlCommand cmd = new SqlCommand("SELECT id, fname, lname, selected_course FROM std WHERE CONCAT(fname, ' ', lname, ' ', address) LIKE @searchTerm", mydb.getConnection);
                    cmd.Parameters.AddWithValue("@searchTerm", textBoxSearch.Text);

                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("id", typeof(int));
                    dataTable.Columns.Add("fname", typeof(string));
                    dataTable.Columns.Add("lname", typeof(string));
                    columname.Add("MSSV");
                    columname.Add("Họ");
                    columname.Add("Tên");

                    mydb.openConnection();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DataRow row = dataTable.NewRow();


                      mssv = reader.GetInt32(0);
                      fname = reader.GetString(1);
                       lname= reader.GetString(2);
                        string selectedCourses = reader.GetString(3);
                        list = StringToList(selectedCourses, ',');
                        // Lấy tên của các khóa học và thêm vào danh sách tên cột
                    }
                    reader.Close();
                    mydb.closeConnection();
                    foreach (string course in list)
                    {
                        dataTable.Columns.Add(course, typeof(string));
                        string courseName = "";
                        SqlCommand command = new SqlCommand("SELECT label FROM Course WHERE ID = @CourseID", mydb.getConnection);
                        command.Parameters.AddWithValue("@CourseID", course);
                        mydb.openConnection();
                        SqlDataReader reader1 = command.ExecuteReader();
                        while (reader1.Read())
                        {
                            courseName = reader1["label"].ToString();
                            columname.Add(courseName);
                        }
                        reader1.Close(); // Đóng reader sau khi đọc xong
                    }

                    // Đóng kết nối và đóng đối tượng đọc dữ liệu
                    mydb.openConnection();
                    SqlCommand cmdScore = new SqlCommand("SELECT studentid, courseid, student_score FROM score WHERE studentid = @studentid", mydb.getConnection);
                    cmdScore.Parameters.AddWithValue("@studentid", id);
                    SqlDataReader readerScore = cmdScore.ExecuteReader();

                    // Đọc dữ liệu từ SqlDataReader và điền vào DataTable
                    while (readerScore.Read())
                    {
                        string courseId = readerScore.GetString(1);
                        double scoreValue = readerScore.GetDouble(2);

                        // Tìm hàng tương ứng với studentId
                        DataRow[] rows = dataTable.Select($"id = '{id}'");
                        if (rows.Length > 0)
                        {
                            // Thêm điểm vào cột tương ứng
                            rows[0][courseId] = scoreValue;
                        }
                        else
                        {
                            // Tạo một hàng mới
                            DataRow newRow = dataTable.NewRow();
                            newRow["id"] = id;
                            newRow["fname"] = fname;
                            newRow["lname"] = lname;
                            newRow[courseId] = scoreValue;
                            dataTable.Rows.Add(newRow);
                        }
                    }
                    dataTable.Columns.Add("AVG");
                    columname.Add("Điểm trung bình");
                    dataTable.Columns.Add("Result");
                    columname.Add("Kết quả");
                    foreach (DataRow row in dataTable.Rows)
                    {
                        double sumScores = 0;
                        int countScores = 0;

                        // Tính tổng điểm của từng sinh viên
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            // Kiểm tra nếu tên cột là course id (nghĩa là đây là cột điểm)
                            if (column.ColumnName != "id" && column.ColumnName != "fname" && column.ColumnName != "lname" && column.ColumnName != "AVG" && column.ColumnName != "Result")
                            {
                                if (row[column] != DBNull.Value)
                                {
                                    sumScores += Convert.ToDouble(row[column]);
                                    countScores++;
                                }
                            }
                        }

                        // Tính điểm trung bình
                        double averageScore = countScores > 0 ? sumScores / countScores : 0;
                        row["AVG"] = averageScore;

                        // Đánh giá kết quả
                        row["Result"] = averageScore >= 5 ? "Pass" : "Fail";
                    }

                    readerScore.Close();
                    mydb.closeConnection();

                    // Gán DataTable vào DataGridView
                    dataGridView1.DataSource = dataTable;
                    if (dataGridView1.Columns.Count == columname.Count)
                    {
                        // Duyệt qua từng cột trong dataGridView1 và đặt tên mới từ columname
                        for (int i = 0; i < dataGridView1.Columns.Count; i++)
                        {
                            dataGridView1.Columns[i].HeaderText = columname[i];
                        }
                    }
                }*/



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

        private void AVGResultBySCore_Load(object sender, EventArgs e)
        {
            List<String> nameColum = new List<String>();
            SqlCommand cmd = new SqlCommand("SELECT id, fname, lname FROM std", mydb.getConnection);
            mydb.openConnection();
            SqlDataReader reader = cmd.ExecuteReader();

            // Tạo DataTable mới
            DataTable dataTable = new DataTable();

            // Thêm cột id vào DataTable
            dataTable.Columns.Add("id", typeof(int));
            nameColum.Add("MSSV");
            // Thêm cột fname vào DataTable
            dataTable.Columns.Add("fname", typeof(string));
            nameColum.Add("Họ");
            // Thêm cột lname vào DataTable
            dataTable.Columns.Add("lname", typeof(string));
            nameColum.Add("Tên");
            // Đọc dữ liệu từ SqlDataReader và thêm vào DataTable
            while (reader.Read())
            {
                // Tạo một hàng mới
                DataRow row = dataTable.NewRow();

                // Thêm dữ liệu từ SqlDataReader vào hàng
                row["id"] = reader.GetInt32(0); // Đọc giá trị ở cột index 0 (id)
                row["fname"] = reader.GetString(1); // Đọc giá trị ở cột index 1 (fname)
                row["lname"] = reader.GetString(2); // Đọc giá trị ở cột index 2 (lname)

                // Thêm hàng vào DataTable
                dataTable.Rows.Add(row);
            }
            mydb.closeConnection();

            // Mở lại kết nối để thực hiện các truy vấn tiếp theo
            mydb.openConnection();

            SqlCommand cmd1 = new SqlCommand("SELECT id,label FROM course", mydb.getConnection);
            SqlDataReader reader1 = cmd1.ExecuteReader();

            // Đọc dữ liệu từ SqlDataReader và thêm vào DataTable
            while (reader1.Read())
            {
                // Lấy giá trị id từ SqlDataReader
                string courseId = reader1.GetString(0);
                string label = reader1.GetString(1);
                nameColum.Add(label);
                // Thêm giá trị id vào DataTable
                dataTable.Columns.Add(courseId);
            }

            reader1.Close();

            SqlCommand cmdScore = new SqlCommand("SELECT studentid, courseid, student_score FROM score", mydb.getConnection);
            SqlDataReader readerScore = cmdScore.ExecuteReader();

            // Thêm cột điểm vào DataTable
            // Thêm cột điểm vào DataTable
            while (readerScore.Read())
            {
                string courseId = readerScore.GetString(1);

                // Kiểm tra xem cột đã tồn tại trong DataTable chưa
                if (!dataTable.Columns.Contains(courseId))
                {
                    // Nếu chưa tồn tại, thêm cột mới
                    dataTable.Columns.Add(courseId, typeof(double)); // Giả sử student_score có kiểu dữ liệu là double
                }
            }


            readerScore.Close();

            // Mở lại kết nối để thực hiện đọc dữ liệu từ bảng score
            mydb.openConnection();

            readerScore = cmdScore.ExecuteReader();

            // Đọc dữ liệu từ SqlDataReader và thêm vào DataTable
            while (readerScore.Read())
            {
                int studentId = readerScore.GetInt32(0);
                string courseId = readerScore.GetString(1);
                double scoreValue = readerScore.GetDouble(2); // Giả sử student_score có kiểu dữ liệu là double

                // Tìm hàng tương ứng với studentId
                DataRow[] rows = dataTable.Select($"id = '{studentId}'");
                if (rows.Length > 0)
                {
                    // Thêm điểm vào cột tương ứng
                    rows[0][courseId] = scoreValue;
                }
                else
                {
                    // Tạo một hàng mới
                    DataRow newRow = dataTable.NewRow();
                    newRow["id"] = studentId;
                    newRow[courseId] = scoreValue;
                    dataTable.Rows.Add(newRow);
                }
            }
            dataTable.Columns.Add("AverageScore", typeof(double));
            nameColum.Add("Điểm trung bình");
            dataTable.Columns.Add("Result", typeof(string));
            nameColum.Add("Kết quả");

            // Tính điểm trung bình và đánh giá kết quả
            foreach (DataRow row in dataTable.Rows)
            {
                double sumScores = 0;
                int countScores = 0;

                // Tính tổng điểm của từng sinh viên
                foreach (DataColumn column in dataTable.Columns)
                {
                    // Kiểm tra nếu tên cột là course id (nghĩa là đây là cột điểm)
                    if (column.ColumnName != "id" && column.ColumnName != "fname" && column.ColumnName != "lname" && column.ColumnName != "AverageScore" && column.ColumnName != "Result")
                    {
                        if (row[column] != DBNull.Value)
                        {
                            sumScores += Convert.ToDouble(row[column]);
                            countScores++;
                        }
                    }
                }

                // Tính điểm trung bình
                double averageScore = countScores > 0 ? sumScores / countScores : 0;
                row["AverageScore"] = averageScore;

                // Đánh giá kết quả
                row["Result"] = averageScore >= 5 ? "Pass" : "Fail";



                readerScore.Close();
                mydb.closeConnection();

                dataGridView1.DataSource = dataTable;
                if (dataGridView1.Columns.Count == nameColum.Count)
                {
                    // Duyệt qua từng cột trong dataGridView1 và đặt tên mới từ nameColum
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].HeaderText = nameColum[i];
                    }
                }
            }
        }

    }
}