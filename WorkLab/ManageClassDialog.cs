using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkLab.Model;

namespace WorkLab
{
    public partial class ManageClassDialog : Form
    {

        private AutoCompleteStringCollection emailAutoComplete = new AutoCompleteStringCollection(); //AutoCompletes users text, saves user from fully entering email.
        private Class classObj; //Created a Private Class Object, using Object Orientated Programming to its full extenet.
        private List<User> users;

       
        public ManageClassDialog(Class c)
        {
            InitializeComponent();
            classObj = c; //Initialize the private Class

            nameTextBox.Text = c.Name;

            if(c.Students != null)
            {
                foreach (var s in c.Students) //Iterate through each student
                {
                    var item = new ListViewItem(new[] { s.Name, s.Email }); //Populates DataGridView in the ManageClassDialog
                    item.Tag = s;
                    listView1.Items.Add(item);
                }
            }
            
            //AutoCompletes Text
            emailTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            emailTextBox.AutoCompleteCustomSource = emailAutoComplete;
            emailTextBox.AutoCompleteMode = AutoCompleteMode.Suggest;

            loadUsers(); //Calls loadUsers upon completion.
        }


        private async void loadUsers()
        {
            users = await ApiWrapper.GetUsers(); //Using API to fetch the students
            foreach(var u in users)
            {
                emailAutoComplete.Add(u.Email); //Autocompletes the email address
            } 
        }
        

        private async void addButton_Click(object sender, EventArgs e)
        {
           
        }

        //Removing students from a Class.
        private async void removeButton_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem i in listView1.SelectedItems) //Remove the selected item from the list.
            {
                if (i.Tag is User) { 
                    User u = (User)i.Tag;
                    if (await ApiWrapper.RemoveStudentFromClass(classObj.Id, u.Id)) //Update the database, so change has been applied serverside.
                    {
                        listView1.Items.Remove(i);
                    }
                }
            }
        }

        //Saving the class name
        private async void saveNameButton_Click(object sender, EventArgs e)
        {
            string name = nameTextBox.Text;

            if(name.Length < 3) //Verification on class name, ensures teacher enters an appropiate name.
            {
                MessageBox.Show("Name is too short.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                await ApiWrapper.UpdateClass(classObj.Id, name); //If criteria is met, update will also apply to the backend too
            }
        }

        //
        private async void bunifuThinButton2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem i in listView1.SelectedItems) //Remove the selected item from the list.
            {
                if (i.Tag is User)
                {
                    User u = (User)i.Tag;
                    if (await ApiWrapper.RemoveStudentFromClass(classObj.Id, u.Id)) //If criteria is met, update will also apply to the backend too
                    {
                        listView1.Items.Remove(i); //Remove that selected Tagged student.
                    }
                }
            }
        }

        private async void bunifuThinButton1_Click(object sender, EventArgs e)
        {
            string name = nameTextBox.Text;

            if (name.Length < 3) //Verification on class name, ensures teacher enters an appropiate name.
            {
                MessageBox.Show("Name is too short.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                await ApiWrapper.UpdateClass(classObj.Id, name);
            }
        }

        private async void bunifuThinButton3_Click(object sender, EventArgs e)
        {
            string email = emailTextBox.Text; //Privately declared varible to store Student's email.
            try
            {
                //find the user with the entered email address
                User u = users.First(x => x.Email == email);
                if (await ApiWrapper.AddStudentToClass(classObj.Id, u.Id))
                {
                    var item = new ListViewItem(new[] { u.Name, u.Email });
                    item.Tag = u;
                    listView1.Items.Add(item); //Adds the selected student to the class.
                } 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message); //Error logging, helps in terms of debugging
            }
        }

        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
