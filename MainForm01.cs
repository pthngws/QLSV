
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{ 
    public partial class MainForm01 : Form
    {
        public MainForm01()
        {
            InitializeComponent();
        }
        User user = new User();
        public MainForm01(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void addNewStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddStudentForm addStdF = new AddStudentForm();
            addStdF.Show(this);
        }

        private void rToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentListForm studentListForm = new StudentListForm();
            studentListForm.Show();
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SearchNameForm searchNameForm = new SearchNameForm(user);
                searchNameForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm01_Load(object sender, EventArgs e)
        {
            if (user.Role != "admin")
            {
                addNewStudentToolStripMenuItem.Visible = false;
                rToolStripMenuItem.Visible = false;
                approveToolStripMenuItem.Visible = false;
                aDMINToolStripMenuItem.Visible = false;
                manageToolStripMenuItem.Visible = false;
            }
        }

        private void approveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Approve approve = new Approve();
            approve.Show();
        }

        private void staticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StaticForm staticForm = new StaticForm();
            staticForm.Show();
        }

        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manage manage = new Manage();
            manage.Show();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Print print = new Print();
            print.Show();
        }

        private void addCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCourse addCourse = new AddCourse();
            addCourse.Show();
        }

        private void removeCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveCourse removeCourse = new RemoveCourse();
            removeCourse.Show();
        }

        private void editCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditCourse editCourse = new EditCourse();
            editCourse.Show();
        }

        private void manageCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageCourse manageCourse = new ManageCourse();
            manageCourse.Show();
        }

        private void printToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PrintCourse printCourse = new PrintCourse();
            printCourse.Show();
        }

        private void addScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddScoreForm addScoreForm = new AddScoreForm();
            addScoreForm.Show();
        }

        private void manageScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageScore manageScore    = new ManageScore();
            manageScore.Show();
        }

        private void removeScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveScore removeScore = new RemoveScore();
            removeScore.Show();
        }

        private void avgScoreByCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AvgScore avgScore = new AvgScore();
            avgScore.Show();
        }

        private void printToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            PrintScore print = new PrintScore();
            print.Show();
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportForm report = new ReportForm();
            report.Show();
        }

        private void reportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ReportCrsForm report = new ReportCrsForm();
            report.Show();
        }

        private void reportToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ReportScoreFrm reportCrsForm = new ReportScoreFrm();
            reportCrsForm.Show();
        }

        private void aVGRESULTBYSCOREToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AVGResultBySCore aVG = new AVGResultBySCore();
            aVG.Show();
        }

        private void sTATICRESULTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StaticResult staticResult = new StaticResult();
            staticResult.Show();
        }

        private void sCOREOFSTUDENTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScoreOfStudent scoreOfStudent = new ScoreOfStudent();
            scoreOfStudent.Show();
        }
    }
}
