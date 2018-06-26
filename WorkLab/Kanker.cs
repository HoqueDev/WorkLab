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

namespace WorkLab
{
    public partial class Kanker : Form
    {
   

        String Party;
        String Username;
        String Password;

        public Kanker()
        {
            InitializeComponent();
        }

        private void bunifuThinButton1_Click(object sender, EventArgs e)
        {
            string config = @"Provider=Microsoft.ACE.OLEDB.12.0;
        Data Source=C:\Users\Maahi\Desktop\class.accdb";

            string sql = @"SELECT * FROM 12C01 WHERE Email = @EmailtoVerif";
            using (SqlConnection conn = new SqlConnection(config)
                )
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@EmailtoVerif", "maahi@mail.com");
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (reader.HasRows)
                    {
                        MessageBox.Show("True");
                    }
                    else
                    {
                        MessageBox.Show("False");
                    }
                }
            }
        }
        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
