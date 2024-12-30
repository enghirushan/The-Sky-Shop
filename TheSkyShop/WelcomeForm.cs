using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_M
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
            timer1.Start();
        }

        int startpoint =0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint += 2;

            progressBar1.Value = startpoint;
            if (progressBar1.Value == 100)
            {
                progressBar1.Value = 0;
                timer1.Stop();
                Login login = new Login();
                this.Hide();
                login.ShowDialog();
            }

        }
    }
}
