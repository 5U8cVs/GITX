using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GITX
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = System.IO.Directory.GetCurrentDirectory();
            System.DateTime bdt = BuildDate.GetBuildDateTime(System.Reflection.Assembly.GetCallingAssembly());
            label1.Text = "GITX Build Date:  " + bdt.ToLongDateString();
        }

        private void updateRichTextBox()
        {
            richTextBox1.Clear();
            string tempDirectory = Environment.GetEnvironmentVariable("TEMP");
            string scriptFilename = tempDirectory + "\\" + "script.cmd";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(scriptFilename))
            {
                for (; ; )
                {
                    string msg = sr.ReadLine();
                    if (msg == null)
                        break;
                    richTextBox1.AppendText(msg + "\r\n");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)  // INIT
        {
            var SD = new switchDirectory();
            SD.ShowDialog();
            this.Text = System.IO.Directory.GetCurrentDirectory();
            var BL = new getBranchLabel(false);
            BL.ShowDialog();
            var branchLabel = BL.branchLabel;  // actually a commit label
            System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            if (!System.IO.Directory.Exists(Environment.GetEnvironmentVariable("TEMP")))
                System.IO.Directory.CreateDirectory(Environment.GetEnvironmentVariable("TEMP"));
            string tempDirectory = Environment.GetEnvironmentVariable("TEMP");
            string scriptFilename = tempDirectory + "\\" + "script.cmd";
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(scriptFilename))
            {
                sw.WriteLine("C:");
                sw.WriteLine("cd " + this.Text);
                sw.WriteLine("git init");
                sw.WriteLine("git add .");
                sw.WriteLine("git commit -m \"{0}\"", branchLabel);
                sw.WriteLine("pause");
            }
            updateRichTextBox();
            try
            {
                string myProgramFilesPath = tempDirectory;
                myProcess.StartInfo.FileName = myProgramFilesPath + "\\script.cmd";
                myProcess.StartInfo.Arguments = "";
                myProcess.StartInfo.Verb = "Open";
                myProcess.StartInfo.CreateNoWindow = false;
                myProcess.Start();
            }
            catch (Exception e100)
            {
                MessageBox.Show(e100.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)  // create a new branch
        {
            var SD = new switchDirectory();
            SD.ShowDialog();
            this.Text = System.IO.Directory.GetCurrentDirectory();
            var BL = new getBranchLabel(true);
            BL.ShowDialog();
            var branchLabel = BL.branchLabel;
            System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            if (!System.IO.Directory.Exists(Environment.GetEnvironmentVariable("TEMP")))
                System.IO.Directory.CreateDirectory(Environment.GetEnvironmentVariable("TEMP"));
            string tempDirectory = Environment.GetEnvironmentVariable("TEMP");
            string scriptFilename = tempDirectory + "\\" + "script.cmd";
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(scriptFilename))
            {
                sw.WriteLine("C:");
                sw.WriteLine("cd " + this.Text);
                sw.WriteLine("git checkout -b \"" + branchLabel + "\"");
                sw.WriteLine("git branch");
                sw.WriteLine("pause");
            }
            updateRichTextBox();
            try
            {
                string myProgramFilesPath = tempDirectory;
                myProcess.StartInfo.FileName = myProgramFilesPath + "\\script.cmd";
                myProcess.StartInfo.Arguments = "";
                myProcess.StartInfo.Verb = "Open";
                myProcess.StartInfo.CreateNoWindow = false;
                myProcess.Start();
            }
            catch (Exception e100)
            {
                MessageBox.Show(e100.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)  // REVERT (but retain now-empty branch)
        {
            var SD = new switchDirectory();
            SD.ShowDialog();
            this.Text = System.IO.Directory.GetCurrentDirectory();
            System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            if (!System.IO.Directory.Exists(Environment.GetEnvironmentVariable("TEMP")))
                System.IO.Directory.CreateDirectory(Environment.GetEnvironmentVariable("TEMP"));
            string tempDirectory = Environment.GetEnvironmentVariable("TEMP");
            string scriptFilename = tempDirectory + "\\" + "script.cmd";
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(scriptFilename))
            {
                sw.WriteLine("C:");
                sw.WriteLine("cd " + this.Text);
                sw.WriteLine("git checkout -f");
                sw.WriteLine("pause");
            }
            updateRichTextBox();
            try
            {
                string myProgramFilesPath = tempDirectory;
                myProcess.StartInfo.FileName = myProgramFilesPath + "\\script.cmd";
                myProcess.StartInfo.Arguments = "";
                myProcess.StartInfo.Verb = "Open";
                myProcess.StartInfo.CreateNoWindow = false;
                myProcess.Start();
            }
            catch (Exception e100)
            {
                MessageBox.Show(e100.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)  // MERGE
        {
            var SD = new switchDirectory();
            SD.ShowDialog();
            this.Text = System.IO.Directory.GetCurrentDirectory();
            var BL = new getBranchLabel(true);
            BL.ShowDialog();
            var branchLabel = BL.branchLabel;
            System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            if (!System.IO.Directory.Exists(Environment.GetEnvironmentVariable("TEMP")))
                System.IO.Directory.CreateDirectory(Environment.GetEnvironmentVariable("TEMP"));
            string tempDirectory = Environment.GetEnvironmentVariable("TEMP");
            string scriptFilename = tempDirectory + "\\" + "script.cmd";
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(scriptFilename))
            {
                sw.WriteLine("C:");
                sw.WriteLine("cd " + this.Text);
                sw.WriteLine("git checkout master");
                sw.WriteLine("git merge \"" + branchLabel + "\"");
                sw.WriteLine("pause");
            }
            updateRichTextBox();
            try
            {
                string myProgramFilesPath = tempDirectory;
                myProcess.StartInfo.FileName = myProgramFilesPath + "\\script.cmd";
                myProcess.StartInfo.Arguments = "";
                myProcess.StartInfo.Verb = "Open";
                myProcess.StartInfo.CreateNoWindow = false;
                myProcess.Start();
            }
            catch (Exception e100)
            {
                MessageBox.Show(e100.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)  // DBRANCH (like REVERT except branch is lopped off)
        {
            var SD = new switchDirectory();
            SD.ShowDialog();
            this.Text = System.IO.Directory.GetCurrentDirectory();
            var BL = new getBranchLabel(true);
            BL.ShowDialog();
            var branchLabel = BL.branchLabel;
            System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            if (!System.IO.Directory.Exists(Environment.GetEnvironmentVariable("TEMP")))
                System.IO.Directory.CreateDirectory(Environment.GetEnvironmentVariable("TEMP"));
            string tempDirectory = Environment.GetEnvironmentVariable("TEMP");
            string scriptFilename = tempDirectory + "\\" + "script.cmd";
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(scriptFilename))
            {
                sw.WriteLine("C:");
                sw.WriteLine("cd " + this.Text);
                sw.WriteLine("git add .");
                sw.WriteLine("git commit");
                sw.WriteLine("git checkout master");
                sw.WriteLine("git branch -D " + branchLabel);
                sw.WriteLine("pause");
            }
            updateRichTextBox();
            try
            {
                string myProgramFilesPath = tempDirectory;
                myProcess.StartInfo.FileName = myProgramFilesPath + "\\script.cmd";
                myProcess.StartInfo.Arguments = "";
                myProcess.StartInfo.Verb = "Open";
                myProcess.StartInfo.CreateNoWindow = false;
                myProcess.Start();
            }
            catch (Exception e100)
            {
                MessageBox.Show(e100.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)  // PUSH
        {
            var SD = new switchDirectory();
            SD.ShowDialog();
            this.Text = System.IO.Directory.GetCurrentDirectory();
            System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            if (!System.IO.Directory.Exists(Environment.GetEnvironmentVariable("TEMP")))
                System.IO.Directory.CreateDirectory(Environment.GetEnvironmentVariable("TEMP"));
            string tempDirectory = Environment.GetEnvironmentVariable("TEMP");
            string scriptFilename = tempDirectory + "\\" + "script.cmd";
            char[] pattern = { '\\' };
            string[] unlike = this.Text.Split(pattern);
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(scriptFilename))
            {
                sw.WriteLine("C:");
                sw.WriteLine("cd " + this.Text);
                sw.WriteLine("git add .");
                sw.WriteLine("git commit");
                // GITX remote add origin https://github.com/5U8cVs/memorableName.git
                sw.WriteLine("git remote add origin https://github.com/5U8cVs/" + unlike[unlike.Length - 1] + ".git");
                sw.WriteLine("git push -u origin master");
                sw.WriteLine("pause");
            }
            updateRichTextBox();
            try
            {
                string myProgramFilesPath = tempDirectory;
                myProcess.StartInfo.FileName = myProgramFilesPath + "\\script.cmd";
                myProcess.StartInfo.Arguments = "";
                myProcess.StartInfo.Verb = "Open";
                myProcess.StartInfo.CreateNoWindow = false;
                myProcess.Start();
            }
            catch (Exception e100)
            {
                MessageBox.Show(e100.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)  // NEW
        {
            this.Text = System.IO.Directory.GetCurrentDirectory();
            var BL = new getBranchLabel(true);
            BL.ShowDialog();
            var branchLabel = BL.branchLabel;  // actually a commit label
            System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            if (!System.IO.Directory.Exists(Environment.GetEnvironmentVariable("TEMP")))
                System.IO.Directory.CreateDirectory(Environment.GetEnvironmentVariable("TEMP"));
            string tempDirectory = Environment.GetEnvironmentVariable("TEMP");
            string scriptFilename = tempDirectory + "\\" + "script.cmd";
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(scriptFilename))
            {
                sw.WriteLine("C:");
                sw.WriteLine("cd " + "C:\\Sites");
                sw.WriteLine("rails new " + branchLabel);
                sw.WriteLine("cd C:\\Sites\\" + branchLabel);
                sw.WriteLine("pause");
            }
            updateRichTextBox();
            string proposedNewDirectory = "C:\\Sites\\" + branchLabel;
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(tempDirectory + "\\rememberDirectory.txt"))
            {
                sw.WriteLine(proposedNewDirectory);
            }
            try
            {
                string myProgramFilesPath = tempDirectory;
                myProcess.StartInfo.FileName = myProgramFilesPath + "\\script.cmd";
                myProcess.StartInfo.Arguments = "";
                myProcess.StartInfo.Verb = "Open";
                myProcess.StartInfo.CreateNoWindow = false;
                myProcess.Start();
            }
            catch (Exception e100)
            {
                MessageBox.Show(e100.Message);
            }
        }
    }
}
