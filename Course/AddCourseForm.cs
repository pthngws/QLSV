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
            if (!string.IsNullOrEmpty(semester))
            {
                listBox1.DataSource = course.getAllCoursesBySemester(semester);
                listBox1.ValueMember = "id";
                listBox1.DisplayMember = "id";
                listBox1.SelectedItem = null;
            }

            listBox2.DataSource = list;
            listBox2.SelectedItem = null;
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
                    list.Add(select);
                    // Loại bỏ khoảng trắng và các chuỗi null
                    list = RemoveWhitespace(list);
                    // Cập nhật DataSource của ListBox2
                    listBox2.DataSource = null;
                    listBox2.DataSource = list;
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
