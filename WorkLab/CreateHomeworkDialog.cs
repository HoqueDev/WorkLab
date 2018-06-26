using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkLab.Model;

namespace WorkLab
{
    public partial class CreateHomeworkDialog : Form
    {
        private Class classObj;
        public CreateHomeworkDialog(Class c) //initalising public but temporary class
        {
            InitializeComponent();
            classObj = c;
        }

        private async void okButton_Click(object sender, EventArgs e)
        {       
                    
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void selectFileButton_Click(object sender, EventArgs e)
        {
        }

        private void CreateHomeworkDialog_Load(object sender, EventArgs e)
        {

        }

        private async void bunifuThinButton3_Click(object sender, EventArgs e)
        {
            string name = nameTextBox.Text;
            string path = fileTextBox.Text;

            string error = "";

            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (name.Contains(item))
                {
                    MessageBox.Show("Please enter a name without special characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

          


            //simple validation to verify against name - prevents clustering when adding larger classes
            if (name.Length < 3)
            {
                error = "Name is too short.";
            }
            else if (!File.Exists(path))
            {
                error = "Specified file does not exist.";
            }

            if (error != "")
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //debugging, shows user error
                return;
            }

            try
            {
                string url = await ApiWrapper.UploadFile(path);

                if (!await ApiWrapper.CreateHomework(classObj.Id, name, url)) //attempts to create homework
                {
                    MessageBox.Show("Failed to create homework.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //throws error if anything failed
                }
                else
                {
                    //homework was created
                    Close();
                }
            }
            catch
            {
                MessageBox.Show("It seem's the file is open, close it and try again..."); //likely scanerio when attaching files.
            }
            
        }

        private void bunifuThinButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bunifuThinButton2_Click(object sender, EventArgs e)
        {

           
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton2_Click_1(object sender, EventArgs e)
        {
            //Using OpenFileDialog to allow teacher to navigate through disk to attach a file.
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                fileTextBox.Text = ofd.FileName;
            }
        }

        private void fileTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
