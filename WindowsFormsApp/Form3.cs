using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace WindowsFormsApp
{
    public partial class Form3 : Form
    {
        FileStream fs;
        public Form3()
        {
            InitializeComponent();
        }

        private void btnCreateFolder_Click(object sender, EventArgs e)
        {
            try
            {
               
                string path = @"D:\TestFolder1";
                DirectoryInfo dir = new DirectoryInfo(path);
                if (dir.Exists)
                {
                    MessageBox.Show("Folder already exists");
                }
                else
                {
                    // Directory.CreateDirectory(path);
                    dir.Create();
                    MessageBox.Show("Folder created");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCreateFile_Click(object sender, EventArgs e)
        {
            try
            {
                
                string path = @"D:\TestFolder1\FirstFile.txt"; // text file
                FileInfo fi = new FileInfo(path);
                if (fi.Exists)
                {
                    MessageBox.Show("File already exists");
                }
                else
                {
                    fi.Create();
                    MessageBox.Show("File created");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            // Write to file code 
            
            try
            {
                int id = Convert.ToInt32(txtId.Text);
                string name = txtName.Text;
                string location = txtLocation.Text;
                fs = new FileStream(@"D:\TestFolder1\FirstFile.txt", FileMode.Create, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(id);
                bw.Write(name);
                bw.Write(location);
                bw.Close();
                MessageBox.Show("Done");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                
                fs.Close(); // free the resouce 
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                fs = new FileStream(@"D:\TestFolder1\FirstFile.txt", FileMode.Create, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                txtId.Text = br.ReadInt32().ToString();// convert binary data to int
                txtName.Text = br.ReadString();
                txtLocation.Text = br.ReadString();
                br.Close();  // close the opeation reader
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fs.Close(); // free the resouce 
            }
        }

        private void btnBinaryWrite_Click(object sender, EventArgs e)
        {
            try
            {

                // dept details accepting from the textboxes & storing in the object
                Department dept = new Department();
                dept.Id = Convert.ToInt32(txtId.Text);
                dept.Name = txtName.Text;
                dept.Location = txtLocation.Text;
                // default file extension is .dat file (data file) / binary file
                fs = new FileStream(@"D:\TestFolder1\Dept", FileMode.Create, FileAccess.Write);
                BinaryFormatter binary = new BinaryFormatter();
                binary.Serialize(fs, dept);
                MessageBox.Show("Done");

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fs.Close();
            }
        }

        private void btnBinaryRead_Click(object sender, EventArgs e)
        {
            try
            {

                // dept details accepting from the textboxes & storing in the object
                Department dept = new Department();
                // default file extension is .dat file (data file) / binary file
                fs = new FileStream(@"D:\TestFolder1\Dept", FileMode.Open, FileAccess.Read);
                BinaryFormatter binary = new BinaryFormatter();
               dept=(Department) binary.Deserialize(fs);
                txtId.Text = dept.Id.ToString();
                txtName.Text = dept.Name;
                txtLocation.Text = dept.Location;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fs.Close();
            }
        }

        private void btnXmlWrite_Click(object sender, EventArgs e)
        {
            try
            {
                Department dept = new Department();
                dept.Id = Convert.ToInt32(txtId.Text);
                dept.Name = txtName.Text;
                dept.Location = txtLocation.Text;
                // default file extension is .dat file (data file) / binary file
                fs = new FileStream(@"D:\TestFolder1\DeptXml", FileMode.Create, FileAccess.Write);
                XmlSerializer xml = new XmlSerializer(typeof(Department));
                xml.Serialize(fs, dept);
                MessageBox.Show("Done");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fs.Close();
            }
        }

        private void btnXmlRead_Click(object sender, EventArgs e)
        {
            try
            {

                // dept details accepting from the textboxes & storing in the object
                Department dept = new Department();
                // default file extension is .dat file (data file) / binary file
                fs = new FileStream(@"D:\TestFolder1\DeptXml", FileMode.Open, FileAccess.Read);
                XmlSerializer xml = new XmlSerializer(typeof(Department));
                dept =(Department) xml.Deserialize(fs);
                txtId.Text = dept.Id.ToString();
                txtName.Text = dept.Name;
                txtLocation.Text = dept.Location;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                fs.Close();
            }
        }
    }
}
