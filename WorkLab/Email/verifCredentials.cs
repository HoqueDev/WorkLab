using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkLab
{
    class verifCredentials
    {

        public void verifyLogin(string email, string password)
        {
            try
            {
                OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Mark\Desktop\class.accdb;
        Persist Security Info=False;");
                string cmdText = "select count(*) from UserAccounts where Email=" + "? and [Password]=?";
                OleDbCommand cmd = new OleDbCommand(cmdText, connection);
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@p1", email);
                    cmd.Parameters.AddWithValue("@p2", password);
                    int result = (int)cmd.ExecuteScalar();

                    try
                    {
                        if (result > 0)
                            proceed();

                        else
                            MessageBox.Show("Invalid credentials, try again...");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error" + ex);
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }


        }

        private void proceed()
        {
            Form5.instance.Hide();
            Form3.instance.Show();
        }


    }
    }

