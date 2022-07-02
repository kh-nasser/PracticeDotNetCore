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
            this.btnSourcePath2 = new System.Windows.Forms.Button();
            this.btnSourcePath1 = new System.Windows.Forms.Button();
            this.txtPath1 = new System.Windows.Forms.TextBox();
            this.txtPath2 = new System.Windows.Forms.TextBox();
            this.richTextBoxResult = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnSourcePath2
            // 
            this.btnSourcePath2.Location = new System.Drawing.Point(28, 87);
            this.btnSourcePath2.Name = "btnSourcePath2";
            this.btnSourcePath2.Size = new System.Drawing.Size(189, 55);
            this.btnSourcePath2.TabIndex = 1;
            this.btnSourcePath2.Text = "Source Path";
            this.btnSourcePath2.UseVisualStyleBackColor = true;
            this.btnSourcePath2.Click += new System.EventHandler(this.btnSourcePath2_Click);
            // 
            // btnSourcePath1
            // 
            this.btnSourcePath1.Location = new System.Drawing.Point(28, 12);
            this.btnSourcePath1.Name = "btnSourcePath1";
            this.btnSourcePath1.Size = new System.Drawing.Size(189, 55);
            this.btnSourcePath1.TabIndex = 2;
            this.btnSourcePath1.Text = "Source Path";
            this.btnSourcePath1.UseVisualStyleBackColor = true;
            this.btnSourcePath1.Click += new System.EventHandler(this.btnSourcePath1_Click);
            // 
            // txtPath1
            // 
            this.txtPath1.Enabled = false;
            this.txtPath1.Location = new System.Drawing.Point(233, 12);
            this.txtPath1.Name = "txtPath1";
            this.txtPath1.Size = new System.Drawing.Size(537, 26);
            this.txtPath1.TabIndex = 3;
            // 
            // txtPath2
            // 
            this.txtPath2.Enabled = false;
            this.txtPath2.Location = new System.Drawing.Point(233, 87);
            this.txtPath2.Name = "txtPath2";
            this.txtPath2.Size = new System.Drawing.Size(537, 26);
            this.txtPath2.TabIndex = 4;
            // 
            // richTextBoxResult
            // 
            this.richTextBoxResult.Location = new System.Drawing.Point(28, 162);
            this.richTextBoxResult.Name = "richTextBoxResult";
            this.richTextBoxResult.Size = new System.Drawing.Size(742, 276);
            this.richTextBoxResult.TabIndex = 6;
            this.richTextBoxResult.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.richTextBoxResult);
            this.Controls.Add(this.txtPath2);
            this.Controls.Add(this.txtPath1);
            this.Controls.Add(this.btnSourcePath1);
            this.Controls.Add(this.btnSourcePath2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.Button btnSourcePath2;
        private System.Windows.Forms.Button btnSourcePath1;
        private System.Windows.Forms.TextBox txtPath1;
        private System.Windows.Forms.TextBox txtPath2;
        private System.Windows.Forms.RichTextBox richTextBoxResult;
    }
}

