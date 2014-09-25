namespace PostXML
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbx_Request = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbx_Link = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbx_Response = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbx_FileName = new System.Windows.Forms.TextBox();
            this.btn_ChoiceFile = new System.Windows.Forms.Button();
            this.btn_SoapPost = new System.Windows.Forms.Button();
            this.btn_Post = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbx_Request);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbx_Link);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(845, 474);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Request";
            // 
            // tbx_Request
            // 
            this.tbx_Request.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_Request.Location = new System.Drawing.Point(3, 62);
            this.tbx_Request.Multiline = true;
            this.tbx_Request.Name = "tbx_Request";
            this.tbx_Request.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbx_Request.Size = new System.Drawing.Size(839, 409);
            this.tbx_Request.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(3, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Request";
            // 
            // tbx_Link
            // 
            this.tbx_Link.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbx_Link.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbx_Link.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbx_Link.Location = new System.Drawing.Point(3, 29);
            this.tbx_Link.Name = "tbx_Link";
            this.tbx_Link.Size = new System.Drawing.Size(839, 20);
            this.tbx_Link.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Link";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbx_Response);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 283);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(845, 191);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Response";
            // 
            // tbx_Response
            // 
            this.tbx_Response.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbx_Response.Location = new System.Drawing.Point(3, 16);
            this.tbx_Response.Multiline = true;
            this.tbx_Response.Name = "tbx_Response";
            this.tbx_Response.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbx_Response.Size = new System.Drawing.Size(839, 172);
            this.tbx_Response.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbx_FileName);
            this.groupBox3.Controls.Add(this.btn_ChoiceFile);
            this.groupBox3.Controls.Add(this.btn_SoapPost);
            this.groupBox3.Controls.Add(this.btn_Post);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 234);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.groupBox3.Size = new System.Drawing.Size(845, 49);
            this.groupBox3.TabIndex = 2;
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
            // btn_SoapPost
            // 
            this.btn_SoapPost.Location = new System.Drawing.Point(727, 16);
            this.btn_SoapPost.Name = "btn_SoapPost";
            this.btn_SoapPost.Size = new System.Drawing.Size(75, 23);
            this.btn_SoapPost.TabIndex = 0;
            this.btn_SoapPost.Text = "GenTime";
            this.btn_SoapPost.UseVisualStyleBackColor = true;
            this.btn_SoapPost.Click += new System.EventHandler(this.btn_SoapPost_Click);
            // 
            // btn_Post
            // 
            this.btn_Post.Location = new System.Drawing.Point(646, 15);
            this.btn_Post.Name = "btn_Post";
            this.btn_Post.Size = new System.Drawing.Size(75, 23);
            this.btn_Post.TabIndex = 0;
            this.btn_Post.Text = "Post";
            this.btn_Post.UseVisualStyleBackColor = true;
            this.btn_Post.Click += new System.EventHandler(this.btn_Post_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 231);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(845, 3);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 474);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "HTTP POST XML";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbx_Request;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbx_Link;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbx_Response;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_Post;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Button btn_ChoiceFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox tbx_FileName;
        private System.Windows.Forms.Button btn_SoapPost;

    }
}

