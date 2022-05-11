using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form6 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
        public Form6()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }
        public DataSet GetEmps()
        {
            da = new SqlDataAdapter("select * from Emp", con);
            // apply PK contrainst to the col which is in Dataset table.
            // Id -> Pk in the DB same apply PK to Id col which is in the DataSet
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            // commandbuilder track dataset table & generate sql query that will be pass to the 
            // dataadapter object
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "emp");// emp is a name given to DataTable which is in DataSet
            return ds;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            ds = GetEmps();
            //created a new row to add record. row have same structure as table
            DataRow row = ds.Tables["emp"].NewRow();
            // added data to the row
            row["Name"] = txtName.Text;
            row["Salary"] = txtSalary.Text;
            // attach row to the emp table
            ds.Tables["emp"].Rows.Add(row);
            // reflect the changes from DataSet to Database
           int res= da.Update(ds.Tables["emp"]);
            if(res==1)
                MessageBox.Show("Record saved");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ds = GetEmps();
            // Find() method only work with PK col in the dataset
            DataRow row=ds.Tables["emp"].Rows.Find(Convert.ToInt32(txtId.Text));
            if (row != null)
            {
                txtName.Text = row["Name"].ToString();
                txtSalary.Text = row["Salary"].ToString();
            }
            else
            {
                MessageBox.Show("Record not found");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            double sal = Convert.ToDouble(txtSalary.Text);
            if (string.IsNullOrEmpty(txtName.Text) && sal>0)
            {
                MessageBox.Show("Enter name or salary should be greater than 0");
            }
            else
            {


                ds = GetEmps();
                // Find() method only work with PK col in the dataset
                DataRow row = ds.Tables["emp"].Rows.Find(Convert.ToInt32(txtId.Text));
                if (row != null)
                {
                    row["Name"] = txtName.Text;
                    row["Salary"] = txtSalary.Text;
                    int res = da.Update(ds.Tables["emp"]);
                    if (res == 1)
                        MessageBox.Show("record updated");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ds = GetEmps();
            // Find() method only work with PK col in the dataset
            DataRow row = ds.Tables["emp"].Rows.Find(Convert.ToInt32(txtId.Text));
            if (row != null)
            {
                ds.Tables["emp"].Rows.Remove(row);
                int res = da.Update(ds.Tables["emp"]);
                if (res == 1)
                    MessageBox.Show("record deleted");
            }
            else
            {
                MessageBox.Show("Record not found");
            }
        }
    }
}
