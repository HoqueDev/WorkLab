using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkLab.Error;
using WorkLab.Model;

namespace WorkLab
{

    public partial class Form3 : Form
    {
        public static Form3 instance { get; private set; }
        public Form3()
        {
            InitializeComponent();
            instance = this;
        }
        

        private async void update() //Syncs fields on the form with the database.
        {
            var hw = await ApiWrapper.GetHomeworks(); //Calls method from the class ApiWrapper
            
            listView1.Items.Clear();
            foreach (var h in hw) //iteration
            {
                string submitted = h.Submitted ? "Yes" : "No"; 
                var item = new ListViewItem(new[] { h.Name, h.ClassName, h.TeacherName, h.Url, submitted }); //Construting new ListItem containg all entites needing for the students such as ClassName, Teacher, Homework etc...
                item.Tag = h;
                listView1.Items.Add(item);
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent); //AutoReizes properties, allows users to customise the list to their liking.
        }
        
        //submit homework
        private async void bunifuThinButton3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1) //ensures that student has an entity to where they will send homework too.
            {
                var item = listView1.SelectedItems[0]; 
                var hw = (Homework)item.Tag;

                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == DialogResult.OK)
                {                 
                    string url = await ApiWrapper.UploadFile(ofd.FileName); //url contains name of file, easy referencing when looking for it manually
                    await ApiWrapper.SubmitHomework(hw.Id, url);
                }
            }
            else if (listView1.SelectedItems.Count == 0) //error handling
            {
                MessageBox.Show("Please select an item from the list!", "No item selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select only one item!", "Multiple items selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        //download homework
        private async void bunifuThinButton2_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count == 1) //ensures user has selected a field from the listview to Download.
            {
                var item = listView1.SelectedItems[0];
                var hw = (Homework)item.Tag; //parsing listview tag to variable.
                

                SaveFileDialog sfd = new SaveFileDialog();
                if(sfd.ShowDialog() == DialogResult.OK)
                {
                    using(var client = new WebClient()) //webclient to stream download of file.
                    {
                        await client.DownloadFileTaskAsync(new Uri(hw.Url), sfd.FileName);
                        
                    }
                }
            }
            else if(listView1.SelectedItems.Count == 0) //error handling
            {
                MessageBox.Show("Please select an item from the list!", "No item selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select only one item!", "Multiple items selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        
        //sync button
        private void bunifuTileButton3_Click(object sender, EventArgs e)
        {
            update();
        }

        //sign out
        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            Hide();
            Form1.instance.Show();
        }

        private void bunifuCards1_VisibleChanged(object sender, EventArgs e)
        {
            if(Visible)
            {
                update();
            }
        }

        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
