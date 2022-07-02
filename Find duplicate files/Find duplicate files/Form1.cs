using Microsoft.WindowsAPICodePack.Dialogs;
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

namespace Find_duplicate_files
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSourcePath1_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtPath1.Text = fbd.SelectedPath;
                    Compere();
                }
            }
        }

        private void btnSourcePath2_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtPath2.Text = fbd.SelectedPath;
                    Compere();

                }
            }
        }

        private void Compere()
        {

            string[] entries1 = null;
            string[] entries2 = null;

            if (!string.IsNullOrEmpty(txtPath1.Text))
            {
                Directory.GetFiles(txtPath1.Text);
                Directory.GetDirectories(txtPath1.Text);

                entries1 = Directory.GetFileSystemEntries(txtPath1.Text, "*", SearchOption.TopDirectoryOnly);
            }

            if (!string.IsNullOrEmpty(txtPath2.Text))
            {
                entries2 = Directory.GetFileSystemEntries(txtPath2.Text, "*", SearchOption.TopDirectoryOnly);
            }

            if(entries1 != null && entries2 != null)
            {
                //var result = entries1.Where(x => entries2.Contains(x)).ToList();
                var result = entries1.Intersect(entries2).ToList();
                //result.Aggregate((s1, s2) => s1 + "," + s2);
                richTextBoxResult.Text = string.Join(Environment.NewLine, result.ToArray());
            }
        }

        private void OpenFolderPicker()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);

                    System.Windows.Forms.MessageBox.Show("Files found: " + files.Length.ToString(), "Message");
                }
            }
        }

        private void OpenFolderPickerNew()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                MessageBox.Show("You selected: " + dialog.FileName);
            }
        }
    }
}
