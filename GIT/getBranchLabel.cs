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
    public partial class getBranchLabel : Form
    {
        public bool isBranchLabel = true;
        public string branchLabel = "";
        public getBranchLabel(bool isbl)
        {
            InitializeComponent();
            isBranchLabel = isbl;
            if (isBranchLabel)
                this.Text = "Please enter branch label.";
            else
                this.Text = "Please enter commit label.";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            branchLabel = textBox1.Text;
            foreach (char c in branchLabel)
                if (c == ' ')
                {
                    MessageBox.Show("Spaces in label not allowed.");
                    return;
                }
            if (branchLabel.Length > 0)
            {
                this.Close();
            }
        }
    }
}
