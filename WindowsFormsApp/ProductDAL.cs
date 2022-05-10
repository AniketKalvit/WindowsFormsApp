using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace WindowsFormsApp
{
   public class ProductDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public ProductDAL()
        {
           string constr=ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }

        public Product GetProductById(int id)
        {
            Product prod = new Product();
            string qry = "select * from Product where Id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows) // existance of record in dr object
            {
                while (dr.Read())
                {
                    prod.Id = Convert.ToInt32(dr["Id"]);
                    prod.Name = dr["Name"].ToString();// ["Name"] should match col name
                    prod.Price =Convert.ToInt32(dr["Price"]);
                }
            }
            con.Close();
            return prod;
        }
        public int SaveProduct(Product prod)
        {

            string qry = "insert into Product values(@id,@name,@price)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", prod.Id);
            cmd.Parameters.AddWithValue("@name", prod.Name);
            cmd.Parameters.AddWithValue("@price", prod.Price);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public int UpdateProduct(Product prod)
        {

            string qry = "update Product set Name=@name,Price=@price where Id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", prod.Id);
            cmd.Parameters.AddWithValue("@name", prod.Name);
            cmd.Parameters.AddWithValue("@price", prod.Price);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
    }
}
