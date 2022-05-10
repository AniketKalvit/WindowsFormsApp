using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;  // add namespace
namespace WindowsFormsApp
{
    public partial class Form4 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        ProductDAL prodDAL = new ProductDAL();

        public Form4()
        {
            InitializeComponent();
            
        }
        public void ClearAll()
        {
            txtId.Clear();
            txtName.Clear();
            txtPrice.Clear();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Product prod = new Product();
                prod.Id = Convert.ToInt32(txtId.Text);
                prod.Name = txtName.Text;
                prod.Price = Convert.ToInt32(txtPrice.Text);
                int res = prodDAL.SaveProduct(prod);
                if (res == 1)
                {
                    MessageBox.Show("Record inserted");
                    txtId.Enabled = true;
                    ClearAll();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
               Product prod= prodDAL.GetProductById(Convert.ToInt32(txtId.Text));
                txtName.Text = prod.Name;
                txtPrice.Text = prod.Price.ToString();
             }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Product prod = new Product();
                prod.Id = Convert.ToInt32(txtId.Text);
                prod.Name = txtName.Text;
                prod.Price = Convert.ToInt32(txtPrice.Text);
                int res = prodDAL.UpdateProduct(prod);
                if (res == 1)
                {
                    MessageBox.Show("Record updated");
                    ClearAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // while writing query follow the col sequence
                string qry = "delete from Product where Id=@id";
                // this configuration is to assign query & connection details to commad
                // so that qry will be executed on the given connection
                cmd = new SqlCommand(qry, con);
                // assign values to the parameter
                // no need to follow the sequence
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
              
                // open DB connection
                con.Open();
                // fire the query
                int res = cmd.ExecuteNonQuery();
                if (res == 1)
                {
                    MessageBox.Show("Record deleted");
                    ClearAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select max(Id) from Product";
                cmd = new SqlCommand(qry, con);
                con.Open();
                object obj = cmd.ExecuteScalar();
                if (obj == DBNull.Value) // when obj is null or obj does not have value
                {
                    txtId.Text = "1";
                }
                else
                {
                    int id = Convert.ToInt32(obj);
                    id++;
                    txtId.Text = id.ToString();
                }

                txtId.Enabled = false;
                txtName.Clear();
                txtPrice.Clear();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Product";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows) // existance of record in dr object
                {
                    DataTable table = new DataTable();
                    table.Load(dr);
                    dataGridView1.DataSource = table;
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txtId.Text= dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtPrice.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
    }
}
