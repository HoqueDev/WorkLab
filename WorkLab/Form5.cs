using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkLab
{
    public partial class Form5 : Form
    {
        
        public static Form5 instance { get; private set; }
        public Form5()
        {
            InitializeComponent();
            instance = this;
        }
        

        private async void bunifuThinButton3_Click(object sender, EventArgs e)
        {
            
            bool auth = await ApiWrapper.Authenticate(textBox1.Text, textBox2.Text);
            if (auth)
            {
                Hide();
                if (Form1.LoginType == LoginType.STUDENT)
                {
                    //show student panel
                    Form3.instance.Show();
                }
                else
                {
                    //show teacher panel
                    Form2.instance.Show();
                }
                    
            }
            else
            {
                MessageBox.Show("Invalid email or password", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        
        private void textBox1_VisibleChanged(object sender, EventArgs e)
        {
            //just for testing
            textBox1.Text = (Form1.LoginType == LoginType.STUDENT?"student":"teacher") + "@example.com";
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Close();
            Form1.instance.Show();

        }

        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
