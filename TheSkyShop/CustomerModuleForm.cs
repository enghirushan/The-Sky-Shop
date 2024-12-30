using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_M
{
    public partial class CustomerModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        public CustomerModuleForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                

                if (MessageBox.Show("Are you sure you want to save this Customer?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // User clicked "Yes"
                    cm = new SqlCommand("INSERT INTO tbCustomer(Cname,Cphone)VALUES(@Cname,@Cphone)", con);
                    // Execute the SQL command to save the user record to the database
                    cm.Parameters.AddWithValue("@Cname", txtCName.Text);
                    cm.Parameters.AddWithValue("@Cphone", txtCPhone.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User has been successfll saved");
                    Clear();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {
            txtCName.Clear();
            txtCPhone.Clear();
           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void CloseB_Click(object sender, EventArgs e)
        {
            this.Dispose();  
        }

        private void lblCID_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
           try
           { 

            if (MessageBox.Show("Are you sure you want to Update this Customer?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // User clicked "Yes"
                cm = new SqlCommand("UPDATE tbCustomer SET Cname=@Cname,Cphone=@Cphone WHERE CId LIKE '" + lblCID.Text + "'  ", con);
                // Execute the SQL command to save the user record to the database

                cm.Parameters.AddWithValue("@Cname", txtCName.Text);
                cm.Parameters.AddWithValue("@Cphone", txtCPhone.Text);
                 
                con.Open();
                cm.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Customer has been successfll Updated");
                this.Dispose();
            }

           }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CustomerModuleForm_Load(object sender, EventArgs e)
        {

        }
    }
}
