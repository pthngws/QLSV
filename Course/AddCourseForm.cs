using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace QLSV
{
    public partial class AddCourseForm : Form
    {
        List<string> list = new List<string>();

        public AddCourseForm()
        {
            InitializeComponent();
        }

        string id;

        public AddCourseForm(string id)
        {
            InitializeComponent();
            this.id = id;
            this.list = student.selectedCourseStudent(id);
        }

        Course course = new Course();
        STUDENT student = new STUDENT();

        private void AddCourseForm_Load(object sender, EventArgs e)
        {
            textBoxID.Text = id;
            reloadListBoxData();
        }

        void reloadListBoxData()
        {
            string semester = comboBox1.Text;

            listBox2.DataSource = list;
            listBox2.SelectedItem = null;
            DataTable filter = new DataTable();
            filter.Columns.Add("id"); // Thêm cột "id" vào DataTable filter
            if (!string.IsNullOrEmpty(semester))
            {
                DataTable dt = course.getAllCoursesBySemester(semester);

                // Kiểm tra xem DataTable có dữ liệu không
                if (dt != null && dt.Rows.Count > 0)
                {
                    // Duyệt qua mỗi dòng trong DataTable
                    foreach (DataRow row in dt.Rows)
                    {
                        // Lấy giá trị của cột "id" từ mỗi dòng
                        string idValue = row["id"].ToString();
                        if (!list.Contains(idValue))
                        {
                            // Tạo một dòng mới cho DataTable filter và thêm vào DataTable
                            DataRow newRow = filter.NewRow();
                            newRow["id"] = idValue;
                            filter.Rows.Add(newRow);
                        }
                        // Sử dụng giá trị idValue ở đây

                    }
                }
            }
            // Sau khi hoàn thành, gán DataTable filter làm nguồn dữ liệu cho listBox1
            listBox1.DataSource = filter;
            listBox1.ValueMember = "id";
            listBox1.DisplayMember = "id";
            listBox1.SelectedItem = null;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            reloadListBoxData();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                DataRowView rowView = listBox1.SelectedItem as DataRowView;
                if (rowView != null)
                {
                    string select = rowView["ID"].ToString();

                    // Kiểm tra xem mục đã tồn tại trong ListBox2 chưa
                    if (!list.Contains(select))
                    {
                        list.Add(select);

                        // Loại bỏ khoảng trắng và các chuỗi null
                        list = RemoveWhitespace(list);

                        // Cập nhật DataSource của ListBox2
                        listBox2.DataSource = null;
                        listBox2.DataSource = list;
                        reloadListBoxData();
                    }
                    else
                    {
                        // Nếu mục đã tồn tại trong ListBox2, bạn có thể hiển thị một thông báo hoặc thực hiện xử lý khác tùy thuộc vào yêu cầu của bạn
                        MessageBox.Show("Mục đã tồn tại trong danh sách.");
                    }
                }
            }
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            // Loại bỏ khoảng trắng và các chuỗi null trước khi lưu
            list = RemoveWhitespace(list);
            string selectedCourses = ListBoxItemsToString(listBox2);
            if (student.updateSelectedCourse(id, selectedCourses))
                MessageBox.Show("Thành công");
            else MessageBox.Show("Thất bại");
        }


        static string ListBoxItemsToString(ListBox listBox)
        {
            StringBuilder result = new StringBuilder();
            foreach (var item in listBox.Items)
            {
                result.Append(item.ToString());
                result.Append(", ");
            }

            if (result.Length >= 2)
            {
                result.Length -= 2;
            }

            return result.ToString();
        }
            static List<string> RemoveWhitespace(List<string> danhSach)
    {
        List<string> danhSachMoi = new List<string>();

        foreach (var item in danhSach)
        {
            // Kiểm tra nếu chuỗi không chỉ chứa khoảng trắng
            if (!string.IsNullOrWhiteSpace(item))
            {
                danhSachMoi.Add(item);
            }
        }

        return danhSachMoi;
    }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
