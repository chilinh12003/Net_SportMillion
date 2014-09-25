namespace PostXML
{
    partial class Form2
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbx_FileName = new System.Windows.Forms.TextBox();
            this.btn_ChoiceFile = new System.Windows.Forms.Button();
            this.btn_Execute = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer_Update = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtbx_UpdateText = new System.Windows.Forms.RichTextBox();
            this.timer_Load = new System.Windows.Forms.Timer(this.components);
            this.timer_Load_2 = new System.Windows.Forms.Timer(this.components);
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbx_FileName);
            this.groupBox3.Controls.Add(this.btn_ChoiceFile);
            this.groupBox3.Controls.Add(this.btn_Execute);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBox3.Size = new System.Drawing.Size(775, 49);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            // 
            // tbx_FileName
            // 
            this.tbx_FileName.Location = new System.Drawing.Point(93, 17);
            this.tbx_FileName.Name = "tbx_FileName";
            this.tbx_FileName.Size = new System.Drawing.Size(547, 20);
            this.tbx_FileName.TabIndex = 2;
            // 
            // btn_ChoiceFile
            // 
            this.btn_ChoiceFile.Location = new System.Drawing.Point(12, 15);
            this.btn_ChoiceFile.Name = "btn_ChoiceFile";
            this.btn_ChoiceFile.Size = new System.Drawing.Size(75, 23);
            this.btn_ChoiceFile.TabIndex = 1;
            this.btn_ChoiceFile.Text = "Chọn file";
            this.btn_ChoiceFile.UseVisualStyleBackColor = true;
            this.btn_ChoiceFile.Click += new System.EventHandler(this.btn_ChoiceFile_Click);
            // 
            // btn_Execute
            // 
            this.btn_Execute.Location = new System.Drawing.Point(646, 15);
            this.btn_Execute.Name = "btn_Execute";
            this.btn_Execute.Size = new System.Drawing.Size(75, 23);
            this.btn_Execute.TabIndex = 0;
            this.btn_Execute.Text = "Execute";
            this.btn_Execute.UseVisualStyleBackColor = true;
            this.btn_Execute.Click += new System.EventHandler(this.btn_Execute_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // timer_Update
            // 
            this.timer_Update.Interval = 1000;
            this.timer_Update.Tick += new System.EventHandler(this.timer_Update_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtbx_UpdateText);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(775, 338);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Updating Text";
            // 
            // rtbx_UpdateText
            // 
            this.rtbx_UpdateText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbx_UpdateText.Location = new System.Drawing.Point(3, 16);
            this.rtbx_UpdateText.Name = "rtbx_UpdateText";
            this.rtbx_UpdateText.Size = new System.Drawing.Size(769, 319);
            this.rtbx_UpdateText.TabIndex = 0;
            this.rtbx_UpdateText.Text = "";
            // 
            // timer_Load
            // 
            this.timer_Load.Interval = 100;
            this.timer_Load.Tick += new System.EventHandler(this.timer_Load_Tick);

            this.timer_Load_2.Interval = 100;
            this.timer_Load_2.Tick += new System.EventHandler(this.timer_Load_Tick_2);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 387);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "Form2";
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbx_FileName;
        private System.Windows.Forms.Button btn_ChoiceFile;
        private System.Windows.Forms.Button btn_Execute;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timer_Update;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtbx_UpdateText;
        private System.Windows.Forms.Timer timer_Load;
        private System.Windows.Forms.Timer timer_Load_2;

    }
}