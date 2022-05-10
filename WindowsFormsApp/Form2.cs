using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace WindowsFormsApp
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
           DialogResult result= fontDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                richTextBox1.SelectionFont = fontDialog.Font; 
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog color = new ColorDialog();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.DefaultExt = ".txt";
                // meaning of * is any file name with following extension
                sd.Filter = "Text Dococument(*.txt)|*.txt | Word(*.docx) |*.docx";
                DialogResult result = sd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(sd.FileName);
                    sw.WriteLine(richTextBox1.Text);
                    sw.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog od = new OpenFileDialog();
                od.Filter = "Text Dococument(*.txt)|*.txt|Word(*.docx)|*.docx|All Files(*.*)|*.*";
                DialogResult result = od.ShowDialog();
                if (result == DialogResult.OK)
                {
                    StreamReader sr = new StreamReader(od.FileName);
                    richTextBox1.Text = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
