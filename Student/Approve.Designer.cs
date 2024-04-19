namespace QLSV
{
    partial class Approve
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
            this.approveBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.baiTapWinformDataSet1 = new QLSV.BaiTapWinformDataSet1();
            this.approveTableAdapter = new QLSV.BaiTapWinformDataSet1TableAdapters.ApproveTableAdapter();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.hRBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.baiTapWinformDataSet13 = new QLSV.BaiTapWinformDataSet13();
            this.loginBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.baiTapWinformDataSet6 = new QLSV.BaiTapWinformDataSet6();
            this.log_inTableAdapter = new QLSV.BaiTapWinformDataSet6TableAdapters.log_inTableAdapter();
            this.radioButtonHR = new System.Windows.Forms.RadioButton();
            this.radioButtonSTD = new System.Windows.Forms.RadioButton();
            this.hRTableAdapter = new QLSV.BaiTapWinformDataSet13TableAdapters.HRTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.approveBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.baiTapWinformDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hRBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.baiTapWinformDataSet13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.baiTapWinformDataSet6)).BeginInit();
            this.SuspendLayout();
            // 
            // approveBindingSource
            // 
            this.approveBindingSource.DataMember = "Approve";
            this.approveBindingSource.DataSource = this.baiTapWinformDataSet1;
            // 
            // baiTapWinformDataSet1
            // 
            this.baiTapWinformDataSet1.DataSetName = "BaiTapWinformDataSet1";
            this.baiTapWinformDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // approveTableAdapter
            // 
            this.approveTableAdapter.ClearBeforeFill = true;
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.buttonAdd.Location = new System.Drawing.Point(63, 521);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(179, 55);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.buttonDelete.Location = new System.Drawing.Point(323, 521);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(179, 55);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(12, 88);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(558, 420);
            this.dataGridView2.TabIndex = 3;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // hRBindingSource
            // 
            this.hRBindingSource.DataMember = "HR";
            this.hRBindingSource.DataSource = this.baiTapWinformDataSet13;
            // 
            // baiTapWinformDataSet13
            // 
            this.baiTapWinformDataSet13.DataSetName = "BaiTapWinformDataSet13";
            this.baiTapWinformDataSet13.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // loginBindingSource
            // 
            this.loginBindingSource.DataMember = "log_in";
            this.loginBindingSource.DataSource = this.baiTapWinformDataSet6;
            // 
            // baiTapWinformDataSet6
            // 
            this.baiTapWinformDataSet6.DataSetName = "BaiTapWinformDataSet6";
            this.baiTapWinformDataSet6.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // log_inTableAdapter
            // 
            this.log_inTableAdapter.ClearBeforeFill = true;
            // 
            // radioButtonHR
            // 
            this.radioButtonHR.AutoSize = true;
            this.radioButtonHR.Location = new System.Drawing.Point(341, 28);
            this.radioButtonHR.Name = "radioButtonHR";
            this.radioButtonHR.Size = new System.Drawing.Size(53, 25);
            this.radioButtonHR.TabIndex = 10;
            this.radioButtonHR.Text = "HR";
            this.radioButtonHR.UseVisualStyleBackColor = true;
            // 
            // radioButtonSTD
            // 
            this.radioButtonSTD.AutoSize = true;
            this.radioButtonSTD.Checked = true;
            this.radioButtonSTD.Location = new System.Drawing.Point(181, 28);
            this.radioButtonSTD.Name = "radioButtonSTD";
            this.radioButtonSTD.Size = new System.Drawing.Size(96, 25);
            this.radioButtonSTD.TabIndex = 9;
            this.radioButtonSTD.TabStop = true;
            this.radioButtonSTD.Text = "Student";
            this.radioButtonSTD.UseVisualStyleBackColor = true;
            this.radioButtonSTD.CheckedChanged += new System.EventHandler(this.radioButtonSTD_CheckedChanged);
            // 
            // hRTableAdapter
            // 
            this.hRTableAdapter.ClearBeforeFill = true;
            // 
            // Approve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cyan;
            this.ClientSize = new System.Drawing.Size(582, 612);
            this.Controls.Add(this.radioButtonHR);
            this.Controls.Add(this.radioButtonSTD);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAdd);
            this.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Approve";
            this.Text = "Approve";
            this.Load += new System.EventHandler(this.Approve_Load);
            ((System.ComponentModel.ISupportInitialize)(this.approveBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.baiTapWinformDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hRBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.baiTapWinformDataSet13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.baiTapWinformDataSet6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private BaiTapWinformDataSet1 baiTapWinformDataSet1;
        private System.Windows.Forms.BindingSource approveBindingSource;
        private BaiTapWinformDataSet1TableAdapters.ApproveTableAdapter approveTableAdapter;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.DataGridView dataGridView2;
        private BaiTapWinformDataSet6 baiTapWinformDataSet6;
        private System.Windows.Forms.BindingSource loginBindingSource;
        private BaiTapWinformDataSet6TableAdapters.log_inTableAdapter log_inTableAdapter;
        private System.Windows.Forms.RadioButton radioButtonHR;
        private System.Windows.Forms.RadioButton radioButtonSTD;
        private BaiTapWinformDataSet13 baiTapWinformDataSet13;
        private System.Windows.Forms.BindingSource hRBindingSource;
        private BaiTapWinformDataSet13TableAdapters.HRTableAdapter hRTableAdapter;
    }
}