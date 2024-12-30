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
    public partial class OrderForm : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\Documents\dbMS.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        public OrderForm()
        {
            InitializeComponent();
            LoardOrder();
        }

        public void LoardOrder()
        {
            int i = 0;
            dgvOrder.Rows.Clear();
            cm = new SqlCommand("SELECT orderId,odate, O.pId, P.pname, O.CId ,C.Cname, qty, price, total  FROM tbOrder AS O JOIN tbCustomer AS C ON O.CId=C.CId JOIN tbProduct AS P ON O.pId=P.pId WHERE CONCAT(orderId,odate, O.pId, P.pname, O.CId ,C.Cname, qty, price, total) LIKE '%"+txtSearch.Text+"%' ", con);
            con.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {

                i++;
                dgvOrder.Rows.Add(i, dr[0].ToString(),Convert.ToDateTime( dr[1].ToString() ).ToString("dd/MM/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString());

            }
            dr.Close();
            con.Close();


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OrderModuleForm moduleForm = new OrderModuleForm();           
            moduleForm.ShowDialog();
            LoardOrder();
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvOrder.Columns[e.ColumnIndex].Name;
            

            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this user?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cm = new SqlCommand("DELETE FROM tbOrder WHERE orderId LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", con);
                    cm.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record has been successfully Deleted");

                    cm = new SqlCommand("UPDATE tbProduct SET pqty=(pqty+@pqty)  WHERE pId LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[3].Value.ToString() + "'  ", con);
                    cm.Parameters.AddWithValue("@pqty", Convert.ToInt16(dgvOrder.Rows[e.RowIndex].Cells[5].Value.ToString()));

                    con.Open();
                    cm.ExecuteNonQuery();
                    con.Close();

                }

            }
            LoardOrder();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoardOrder();

        }
    }
}
