using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
           InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // code that i want to exe when user click login btn
            if(txtUserId.Text=="admin" && txtPassword.Text == "admin123")
            {
                MessageBox.Show("Success");
            }
            else
            {
                MessageBox.Show("Fail");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // code that i want to exe when user click clear btn
            txtUserId.Clear();
            txtPassword.Clear();
        }

        
    }
}
