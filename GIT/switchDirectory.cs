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
    public partial class switchDirectory : Form
    {
        private string tempDirectory;
        public switchDirectory()
        {
            InitializeComponent();
            tempDirectory = Environment.GetEnvironmentVariable("TEMP");
            textBox1.Text = System.IO.Directory.GetCurrentDirectory();
            try
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(tempDirectory + "\\rememberDirectory.txt"))
                {
                    string dname = sr.ReadLine();
                    if (dname == null)
                        throw new Exception("ugh");
                    System.IO.Directory.SetCurrentDirectory(dname);
                    textBox1.Text = System.IO.Directory.GetCurrentDirectory();
                }
            }
            catch
            { }
        }

        private void button1_Click(object sender, EventArgs e)  // GO
        {
            string proposedNewDirectory = textBox1.Text;
            if (System.IO.Directory.Exists(proposedNewDirectory))
            {
                System.IO.Directory.SetCurrentDirectory(proposedNewDirectory);
                this.Close();
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(tempDirectory + "\\rememberDirectory.txt"))
                {
                    sw.WriteLine(System.IO.Directory.GetCurrentDirectory());
                }
            }
            else
                MessageBox.Show(proposedNewDirectory + " does not exist.");
        }
    }
}
