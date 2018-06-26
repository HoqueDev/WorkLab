using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; //Importing Library to allow me to use Windows Forms, neccessary for using my Bunifi lib

namespace WorkLab
{
    public enum LoginType
    {
        STUDENT, TEACHER
    }

    public partial class Form1 : Form
    {
        //Declaring dataType for Login Type
        public static LoginType LoginType { get; private set; }
        
        public static Form1 instance { get; private set; }
        public Form1()
        {
            InitializeComponent(); //Needs to initialsie all imported components, neccessary for Bunifu
            instance = this;
        }
        
        //If LoginType from above is TEACHER then:
        private void bunifuThinButton2_Click(object sender, EventArgs e)
        {
            LoginType = LoginType.TEACHER;
            Hide(); //Hide's the current form
            Form5.instance.ShowDialog();
            
        }

        //If LoginType from above is STUDENT then:
        private void bunifuThinButton1_Click(object sender, EventArgs e)
        {
            LoginType = LoginType.STUDENT;
            Hide();
            Form5.instance.ShowDialog();
           
        }

        //Allows users to register:
        private void bunifuThinButton3_Click(object sender, EventArgs e)
        {
            var dialog = new RegisterDialog();
            dialog.ShowDialog();
            dialog.Dispose();
        }

        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

        }

        //Used a label to perform a close, much more less resource intensive than a button.
        private void label6_Click(object sender, EventArgs e)
        {
            Close(); 
        }
    }
}
