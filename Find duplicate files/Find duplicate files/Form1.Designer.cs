namespace Find_duplicate_files
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageTwoDirectory = new System.Windows.Forms.TabPage();
            this.richTextBoxResult = new System.Windows.Forms.RichTextBox();
            this.txtPath2 = new System.Windows.Forms.TextBox();
            this.txtPath1 = new System.Windows.Forms.TextBox();
            this.btnSourcePath1 = new System.Windows.Forms.Button();
            this.btnSourcePath2 = new System.Windows.Forms.Button();
            this.tabPageSingleDirectory = new System.Windows.Forms.TabPage();
            this.richTextBoxResult2 = new System.Windows.Forms.RichTextBox();
            this.txtPath3 = new System.Windows.Forms.TextBox();
            this.btnSourcePath3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPageTwoDirectory.SuspendLayout();
            this.tabPageSingleDirectory.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageTwoDirectory);
            this.tabControl1.Controls.Add(this.tabPageSingleDirectory);
            this.tabControl1.Location = new System.Drawing.Point(12, 9);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(780, 982);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageTwoDirectory
            // 
            this.tabPageTwoDirectory.Controls.Add(this.label1);
            this.tabPageTwoDirectory.Controls.Add(this.richTextBoxResult);
            this.tabPageTwoDirectory.Controls.Add(this.txtPath2);
            this.tabPageTwoDirectory.Controls.Add(this.txtPath1);
            this.tabPageTwoDirectory.Controls.Add(this.btnSourcePath1);
            this.tabPageTwoDirectory.Controls.Add(this.btnSourcePath2);
            this.tabPageTwoDirectory.Location = new System.Drawing.Point(4, 29);
            this.tabPageTwoDirectory.Name = "tabPageTwoDirectory";
            this.tabPageTwoDirectory.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTwoDirectory.Size = new System.Drawing.Size(772, 949);
            this.tabPageTwoDirectory.TabIndex = 0;
            this.tabPageTwoDirectory.Text = "Two Directory";
            this.tabPageTwoDirectory.UseVisualStyleBackColor = true;
            // 
            // richTextBoxResult
            // 
            this.richTextBoxResult.Location = new System.Drawing.Point(19, 185);
            this.richTextBoxResult.Name = "richTextBoxResult";
            this.richTextBoxResult.Size = new System.Drawing.Size(739, 742);
            this.richTextBoxResult.TabIndex = 11;
            this.richTextBoxResult.Text = "";
            // 
            // txtPath2
            // 
            this.txtPath2.Enabled = false;
            this.txtPath2.Location = new System.Drawing.Point(224, 124);
            this.txtPath2.Name = "txtPath2";
            this.txtPath2.Size = new System.Drawing.Size(534, 26);
            this.txtPath2.TabIndex = 10;
            // 
            // txtPath1
            // 
            this.txtPath1.Enabled = false;
            this.txtPath1.Location = new System.Drawing.Point(224, 49);
            this.txtPath1.Name = "txtPath1";
            this.txtPath1.Size = new System.Drawing.Size(534, 26);
            this.txtPath1.TabIndex = 9;
            // 
            // btnSourcePath1
            // 
            this.btnSourcePath1.Location = new System.Drawing.Point(19, 49);
            this.btnSourcePath1.Name = "btnSourcePath1";
            this.btnSourcePath1.Size = new System.Drawing.Size(186, 55);
            this.btnSourcePath1.TabIndex = 8;
            this.btnSourcePath1.Text = "Source Path";
            this.btnSourcePath1.UseVisualStyleBackColor = true;
            this.btnSourcePath1.Click += new System.EventHandler(this.btnSourcePath1_Click);
            // 
            // btnSourcePath2
            // 
            this.btnSourcePath2.Location = new System.Drawing.Point(19, 124);
            this.btnSourcePath2.Name = "btnSourcePath2";
            this.btnSourcePath2.Size = new System.Drawing.Size(186, 55);
            this.btnSourcePath2.TabIndex = 7;
            this.btnSourcePath2.Text = "Source Path";
            this.btnSourcePath2.UseVisualStyleBackColor = true;
            this.btnSourcePath2.Click += new System.EventHandler(this.btnSourcePath2_Click);
            // 
            // tabPageSingleDirectory
            // 
            this.tabPageSingleDirectory.Controls.Add(this.label2);
            this.tabPageSingleDirectory.Controls.Add(this.richTextBoxResult2);
            this.tabPageSingleDirectory.Controls.Add(this.txtPath3);
            this.tabPageSingleDirectory.Controls.Add(this.btnSourcePath3);
            this.tabPageSingleDirectory.Location = new System.Drawing.Point(4, 29);
            this.tabPageSingleDirectory.Name = "tabPageSingleDirectory";
            this.tabPageSingleDirectory.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSingleDirectory.Size = new System.Drawing.Size(772, 949);
            this.tabPageSingleDirectory.TabIndex = 1;
            this.tabPageSingleDirectory.Text = "Single Directory";
            this.tabPageSingleDirectory.UseVisualStyleBackColor = true;
            // 
            // richTextBoxResult2
            // 
            this.richTextBoxResult2.Location = new System.Drawing.Point(17, 110);
            this.richTextBoxResult2.Name = "richTextBoxResult2";
            this.richTextBoxResult2.Size = new System.Drawing.Size(739, 816);
            this.richTextBoxResult2.TabIndex = 14;
            this.richTextBoxResult2.Text = "";
            // 
            // txtPath3
            // 
            this.txtPath3.Enabled = false;
            this.txtPath3.Location = new System.Drawing.Point(222, 52);
            this.txtPath3.Name = "txtPath3";
            this.txtPath3.Size = new System.Drawing.Size(534, 26);
            this.txtPath3.TabIndex = 13;
            // 
            // btnSourcePath3
            // 
            this.btnSourcePath3.Location = new System.Drawing.Point(17, 42);
            this.btnSourcePath3.Name = "btnSourcePath3";
            this.btnSourcePath3.Size = new System.Drawing.Size(186, 55);
            this.btnSourcePath3.TabIndex = 12;
            this.btnSourcePath3.Text = "Source Path";
            this.btnSourcePath3.UseVisualStyleBackColor = true;
            this.btnSourcePath3.Click += new System.EventHandler(this.btnSourcePath3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(392, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Find Duplicated Files and Directory base on files name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(388, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Find Duplicated Files and Directory base on file length";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 1003);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageTwoDirectory.ResumeLayout(false);
            this.tabPageTwoDirectory.PerformLayout();
            this.tabPageSingleDirectory.ResumeLayout(false);
            this.tabPageSingleDirectory.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageTwoDirectory;
        private System.Windows.Forms.RichTextBox richTextBoxResult;
        private System.Windows.Forms.TextBox txtPath2;
        private System.Windows.Forms.TextBox txtPath1;
        private System.Windows.Forms.Button btnSourcePath1;
        private System.Windows.Forms.Button btnSourcePath2;
        private System.Windows.Forms.TabPage tabPageSingleDirectory;
        private System.Windows.Forms.RichTextBox richTextBoxResult2;
        private System.Windows.Forms.TextBox txtPath3;
        private System.Windows.Forms.Button btnSourcePath3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

