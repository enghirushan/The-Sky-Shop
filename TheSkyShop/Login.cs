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
    public partial class Login : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public Login()
        {
            InitializeComponent();
        }

        

        private void LOGO_Load(object sender, EventArgs e)
        {

        }

       

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxpassw.Checked==false)
            {
                txtpassw.UseSystemPasswordChar = true;

            }
            else
            {
                txtpassw.UseSystemPasswordChar = false;
            }


        }

        

        private void clear_Click(object sender, EventArgs e)
        {
            txtName.Text = ""; 
            txtpassw.Text = "";
        }

        private void CloseB_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Exit Application ","confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                Application.Exit();
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                cm=new SqlCommand("SELECT * FROM tbUser WHERE User_Name=@User_Name AND Password=@Password",con);
                cm.Parameters.AddWithValue("User_Name", txtName.Text);
                cm.Parameters.AddWithValue("Password", txtpassw.Text);
                con.Open();
                dr= cm.ExecuteReader();
                dr.Read();
                if(dr.HasRows)
                {
                    MessageBox.Show("Welcome " + dr["Full_Name"].ToString()+"|","ACCESS GRANTED", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    MainForm main = new MainForm();
                    this.Hide();
                    main.ShowDialog();
                    
                }
                else
                {
                    MessageBox.Show("Invalid user name or password! ","ACCESS DENITED ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
               



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }

        private void label6_Click_1(object sender, EventArgs e)
        {
            
        }
    }
}
