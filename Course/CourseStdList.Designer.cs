namespace QLSV
{
    partial class CourseStdList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCourseID = new System.Windows.Forms.TextBox();
            this.CourseID = new System.Windows.Forms.Label();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.stdBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stdBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.baiTapWinformDataSet11 = new QLSV.BaiTapWinformDataSet11();
            this.stdTableAdapter = new QLSV.BaiTapWinformDataSet11TableAdapters.stdTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.stdBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stdBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.baiTapWinformDataSet11)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Cyan;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(81, 64);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 21);
            this.label1.TabIndex = 13;
            this.label1.Text = "Semester:";
            // 
            // textBoxCourseID
            // 
            this.textBoxCourseID.Enabled = false;
            this.textBoxCourseID.Location = new System.Drawing.Point(167, 12);
            this.textBoxCourseID.Margin = new System.Windows.Forms.Padding(5);
            this.textBoxCourseID.Multiline = true;
            this.textBoxCourseID.Name = "textBoxCourseID";
            this.textBoxCourseID.Size = new System.Drawing.Size(176, 37);
            this.textBoxCourseID.TabIndex = 12;
            // 
            // CourseID
            // 
            this.CourseID.AutoSize = true;
            this.CourseID.BackColor = System.Drawing.Color.Cyan;
            this.CourseID.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CourseID.Location = new System.Drawing.Point(42, 28);
            this.CourseID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.CourseID.Name = "CourseID";
            this.CourseID.Size = new System.Drawing.Size(129, 21);
            this.CourseID.TabIndex = 11;
            this.CourseID.Text = "Course Name:";
            // 
            // buttonPrint
            // 
            this.buttonPrint.BackColor = System.Drawing.Color.Salmon;
            this.buttonPrint.Location = new System.Drawing.Point(787, 9);
            this.buttonPrint.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(200, 59);
            this.buttonPrint.TabIndex = 14;
            this.buttonPrint.Text = "Print";
            this.buttonPrint.UseVisualStyleBackColor = false;
            // 
            // stdBindingSource
            // 
            this.stdBindingSource.DataMember = "std";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(167, 54);
            this.textBox1.Margin = new System.Windows.Forms.Padding(5);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(56, 37);
            this.textBox1.TabIndex = 17;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dataGridView1.DataSource = this.stdBindingSource1;
            this.dataGridView1.Location = new System.Drawing.Point(32, 122);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(941, 399);
            this.dataGridView1.TabIndex = 18;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn1.HeaderText = "id";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "fname";
            this.dataGridViewTextBoxColumn2.HeaderText = "fname";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "lname";
            this.dataGridViewTextBoxColumn3.HeaderText = "lname";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "bdate";
            this.dataGridViewTextBoxColumn4.HeaderText = "bdate";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // stdBindingSource1
            // 
            this.stdBindingSource1.DataMember = "std";
            this.stdBindingSource1.DataSource = this.baiTapWinformDataSet11;
            // 
            // baiTapWinformDataSet11
            // 
            this.baiTapWinformDataSet11.DataSetName = "BaiTapWinformDataSet11";
            this.baiTapWinformDataSet11.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // stdTableAdapter
            // 
            this.stdTableAdapter.ClearBeforeFill = true;
            // 
            // CourseStdList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cyan;
            this.ClientSize = new System.Drawing.Size(1000, 559);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonPrint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxCourseID);
            this.Controls.Add(this.CourseID);
            this.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CourseStdList";
            this.Text = "CourseStdList";
            this.Load += new System.EventHandler(this.CourseStdList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.stdBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stdBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.baiTapWinformDataSet11)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxCourseID;
        private System.Windows.Forms.Label CourseID;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.BindingSource stdBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bdateDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private BaiTapWinformDataSet11 baiTapWinformDataSet11;
        private System.Windows.Forms.BindingSource stdBindingSource1;
        private BaiTapWinformDataSet11TableAdapters.stdTableAdapter stdTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}