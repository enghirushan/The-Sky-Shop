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
    public partial class OrderModuleForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        int qty = 0;
        public OrderModuleForm()
        {
            InitializeComponent();
            LoadCustomer();
            LoadProduct();
        }

        private void CloseB_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cm = new SqlCommand("SELECT CId , Cname FROM tbCustomer WHERE CONCAT(CId,Cname) LIKE '%" + txtSearchCust.Text+"%' ", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {

                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString());

            }
            dr.Close();
            con.Close();

        }
        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbProduct WHERE CONCAT(pId,pname, pprice, pdescription, pcategory) LIKE '%" + txtSearchProduct.Text + "%' ", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {

                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());

            }
            dr.Close();
            con.Close();

        }

        private void txtSearchCust_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Getqty();
            if (Convert.ToInt16(UDQty.Value)>qty)
            {
                MessageBox.Show("Instock quantity is not enoug!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                UDQty.Value = UDQty.Value - 1;
                return; 
            }
            if(Convert.ToInt16(UDQty.Value) > 0)
            { 
            int total = Convert.ToInt32(txtPrice.Text)*Convert.ToInt32(UDQty.Value);
            txtTotal.Text = total.ToString();
            }
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCid.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtCName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPid.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
           

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCid.Text =="")
                {
                    MessageBox.Show("Please select customer! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtPid.Text == "")
                {
                    MessageBox.Show(" Please select Product! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are you sure you want to insert this Order?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // User clicked "Yes"
                    cm = new SqlCommand("INSERT INTO tbOrder(odate,pId,CId,qty,price,total)VALUES(@odate,@pId,@CId,@qty,@price,@total)", con);
                    // Execute the SQL command to save the user record to the database
                    cm.Parameters.AddWithValue("@odate",dtOrder.Value);
                    cm.Parameters.AddWithValue("@pId", Convert.ToInt16(txtPid.Text));
                    cm.Parameters.AddWithValue("@CId", Convert.ToInt16(txtCid.Text));
                    cm.Parameters.AddWithValue("@qty", Convert.ToInt16(UDQty.Value));
                    cm.Parameters.AddWithValue("@price", Convert.ToInt16(txtPrice.Text));
                    cm.Parameters.AddWithValue("@total", Convert.ToInt16(txtTotal.Text));
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Order has been successfll inserted");
                    



                     cm = new SqlCommand("UPDATE tbProduct SET pqty=(pqty-@pqty)  WHERE pId LIKE '" + txtPid.Text + "'  ", con);           
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(UDQty.Value));
                    
                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();
                    Clear();
                    LoadProduct();



                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void Clear()
        {
            txtCid.Clear();
            txtCName.Clear();

            txtPid.Clear();
            txtPName.Clear();

            txtPrice.Clear();
            UDQty.Value = 1;

            txtTotal.Clear();
            dtOrder.Value = DateTime.Now;
            

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
             
        }

        public void Getqty()
        {
            cm = new SqlCommand("SELECT pqty FROM tbProduct WHERE pId LIKE '" + txtPid.Text + "' ", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {

                qty = Convert.ToInt32(dr[0].ToString());

            }
            dr.Close();
            con.Close();
        }

        

        private void lblOid_Click(object sender, EventArgs e)
        {

        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtPName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPid_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
