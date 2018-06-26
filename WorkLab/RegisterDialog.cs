using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkLab
{
    public partial class RegisterDialog : Form
    {
        public RegisterDialog()
        {
            InitializeComponent(); //Initilatises all UI components
        }

        private async void registerButton_Click(object sender, EventArgs e)
        {
           
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void emailTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private async void bunifuThinButton1_Click(object sender, EventArgs e)
        {
            //privately declared variables, each corresponding to their details.
            string name = nameTextBox.Text;
            string email = emailTextBox.Text;
            string password = passwordTextBox.Text;
            bool isTeacher = teacherCheckBox.Checked;
            bool register = false;
            if (name != "" && email != "" && password != "") //ensures user does not leave fields blank
            {
                int eLength = email.Length;
                int pLength = password.Length;

                if (pLength < 9)
                {
                    MessageBox.Show("Please enter a stronger password", "Failed to create account", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }

                if (eLength > 32)
                {
                    MessageBox.Show("Please enter a suitable email length", "Failed to create account", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }

                bool containsInt = name.Any(char.IsDigit);

                if (containsInt == true)
                {
                    MessageBox.Show("No digits allowed in your name!", "Failed to create account", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }

                string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
                foreach (var item in specialChar)
                {
                    if (name.Contains(item))
                    {
                        MessageBox.Show("Please enter a name without special characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        register = true;
                    }
                }

                if (email.Contains('@'))
                {
                    if (email.Contains('.'))
                    {

                    }
                }
                else
                {
                    MessageBox.Show("Invalid email format detected...", "Failed to create account", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }

                if (register == true)
                {
                    if (await ApiWrapper.CreateUser(name, email, password, isTeacher)) //Creates account
                    {
                        MessageBox.Show("Your account was created", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Close();
                    }
                    else //error handling
                    {
                        MessageBox.Show("Your account could not be created", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

             
            }
            else
            {
                MessageBox.Show("Please fill out all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //Shows if they did not enter in all fields.
            }
        }

        private void bunifuThinButton2_Click(object sender, EventArgs e)
        {
            Close(); 
        }

        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
