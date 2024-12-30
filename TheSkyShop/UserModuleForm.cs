using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Student_M
{
    public partial class UserModuleForm : Form
    {
        SqlConnection con =new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        public UserModuleForm()
        {
            InitializeComponent();
        }

        private void CloseB_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPass.Text != txtRePass.Text)
                {
                    MessageBox.Show("Password did not match! ", "Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to save this user?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
                {
                    // User clicked "Yes"
                    cm = new SqlCommand("INSERT INTO tbUser(User_Name,Full_Name,Password,Phone_NUM)VALUES(@User_Name,@Full_Name,@Password,@Phone_NUM)", con);
                    // Execute the SQL command to save the user record to the database
                    cm.Parameters.AddWithValue("@User_Name", txtUserName.Text);
                    cm.Parameters.AddWithValue("@Full_Name", txtFullName.Text);
                    cm.Parameters.AddWithValue("@Password", txtPass.Text);
                    cm.Parameters.AddWithValue("@Phone_NUM", txtPhone.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User has been successfll saved");
                    Clear();
                }

            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnClear.Enabled = true;
            btnUpdate.Enabled = false;
        }

        public void Clear()
        {
            txtUserName.Clear();
            txtFullName.Clear();
            txtPass.Clear();
            txtRePass.Clear();
            txtPhone.Clear();

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void UserModuleForm_Load(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtPass.Text != txtRePass.Text)
                {
                    MessageBox.Show("Password did not match! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to Update this user?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // User clicked "Yes"
                    cm = new SqlCommand("UPDATE tbUser SET Full_Name=@Full_Name,Password=@Password,Phone_NUM=@Phone_NUM WHERE User_Name LIKE '"+txtUserName.Text+"'  ", con);
                    // Execute the SQL command to save the user record to the database
                     
                    cm.Parameters.AddWithValue("@Full_Name", txtFullName.Text);
                    cm.Parameters.AddWithValue("@Password", txtPass.Text);
                    cm.Parameters.AddWithValue("@Phone_NUM", txtPhone.Text);
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("User has been successfll Updated");
                    this.Dispose();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
