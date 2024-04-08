using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class AddCourse : Form
    {
        public AddCourse()
        {
            InitializeComponent();
        }

        private void AddCourse_Load(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
          
                Course course = new Course();
            string label = textBoxLabel.Text.Trim();
          
            string description = textBoxDescription.Text.Trim();
            string semester = comboBox1.Text;

                //  sv tu 10-100,  co the thay doi

                if (verif())
                {
                int period = Convert.ToInt32(textBoxPeriod.Text.Trim());
                string courseID = textBoxCourseID.Text.Trim();

                    if (course.insertCourse(courseID,label,period,description,semester))
                    {
                        MessageBox.Show("New Course Added", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("CourseID Existed", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Empty Fields", "Add Course", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }


            //  chuc nang kiem tra du lieu input
            bool verif()
            {
                errorProvider1.Clear();
                errorProvider2.Clear();
                errorProvider3.Clear();
                errorProvider4.Clear();

                bool flag = true;

                if (textBoxCourseID.Text.Trim() == "")
                {
                    errorProvider1.SetError(textBoxCourseID, "Không được để trống");
                    flag = false;
                }

                if (textBoxLabel.Text.Trim() == "")
                {
                    errorProvider2.SetError(textBoxLabel, "Không được để trống");
                    flag = false;
                }

                if (textBoxPeriod.Text.Trim() == "")
                {
                    errorProvider3.SetError(textBoxPeriod, "Không được để trống");
                    flag = false;
                }
                else if (!IsNumeric(textBoxPeriod.Text))
                {
                    errorProvider3.SetError(textBoxPeriod, "Họ không được chứa chữ");
                    flag = false;
                }
                else if(Convert.ToInt32(textBoxPeriod.Text)<10)
            {
                errorProvider3.SetError(textBoxPeriod, "Phải lớn hơn 10");
                flag = false;
            }    

                if (textBoxDescription.Text.Trim() == "")
                {
                    errorProvider4.SetError(textBoxDescription, "Không được để trống");
                    flag = false;
                }

               

                return flag;
            }

            bool ContainsNumbers(string input)
            {
                return input.Any(char.IsDigit);
            }

            bool IsNumeric(string input)
            {
                return int.TryParse(input, out _);
            }


        }
    }

