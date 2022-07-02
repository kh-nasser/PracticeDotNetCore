using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
                    CompereDuplicateInTwoPath();
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
                    CompereDuplicateInTwoPath();
                }
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

        private void Form1_Load(object sender, EventArgs e)
        {
            ((Control)this.tabPageSingleDirectory).Enabled = false;
        }

        private void btnSourcePath3_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtPath3.Text = fbd.SelectedPath;
                    CompereDuplicateInSinglePath();
                }
            }
        }
        private void CompereDuplicateInSinglePath()
        {
            var result = Directory.GetFiles(txtPath3.Text)
                 .Select(f =>
                 {
                     using (var fs = new FileStream(f, FileMode.Open, FileAccess.Read))
                     {
                         return new
                         {
                             FileName = f,
                             FileHash = BitConverter.ToString(SHA1.Create().ComputeHash(fs))
                         };
                     }
                 });

            richTextBoxResult.Text = string.Join(Environment.NewLine, result);

            //                .Select(f =>
            //            {
            //                // Same as above, but with:
            //                // FileHash = SHA1.Create().ComputeHash(fs)
            //            })
            //.GroupBy(f => f.FileHash, StructuralComparisons.StructuralEqualityComparer);
        }
        private void CompereDuplicateInTwoPath()
        {

            string[] entries1 = null;
            string[] entries2 = null;

            if (!string.IsNullOrEmpty(txtPath1.Text))
            {
                Directory.GetFiles(txtPath1.Text);
                Directory.GetDirectories(txtPath1.Text);
                entries1 = Directory.GetFileSystemEntries(txtPath1.Text, "*", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < entries1.Length; i++)
                {
                    entries1[i] = Path.GetFileName(entries1[i]);
                }
            }

            if (!string.IsNullOrEmpty(txtPath2.Text))
            {
                entries2 = Directory.GetFileSystemEntries(txtPath2.Text, "*", SearchOption.TopDirectoryOnly);
                for (int i = 0; i < entries2.Length; i++)
                {
                    entries2[i] = Path.GetFileName(entries2[i]);
                }
            }

            if (entries1 != null && entries2 != null)
            {
                //var result = entries1.Where(x => entries2.Contains(x)).ToList();
                var result = entries1.Intersect(entries2).ToList();
                //result.Aggregate((s1, s2) => s1 + "," + s2);
                richTextBoxResult.Text = string.Join(Environment.NewLine, result.ToArray());
            }
        }
    }
}
