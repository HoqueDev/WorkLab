using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Windows.Forms;
using WorkLab.Model;

namespace WorkLab
{
    public partial class Form2 : Form
    {
        public string email; //This needs to be globally addressed, so multiple parts of the application can read from this variable.

  public static DataGridView dgv = new DataGridView();
        
        List<Class> classes;
        Class selectedClass;


        public static Form2 instance { get; private set; }
        public Form2()
        {
            InitializeComponent();
            instance = this;
        }
        
        //This is the dropDown which presents all the created/associated classes of a tracher.
        private void bunifuDropdown1_onItemSelected(object sender, EventArgs e)
        {
            Console.WriteLine("selected " + bunifuDropdown1.selectedIndex + "classes count: "+classes.Count);
            int i = bunifuDropdown1.selectedIndex; //Casted the SelectedIndex into an Int Array. 
            if(i > classes.Count)
            {
                return; 
            }
            selectedClass = classes[i]; //The selected class is set to the current level of the Array.
            update(); //Calls function to Update the dataGridView from the current level of the Array.
        }

       //Set Homework
        private void bunifuThinButton1_Click(object sender, EventArgs e)
        {
            if(selectedClass == null)
            {
                return;
            }

            var chd = new CreateHomeworkDialog(selectedClass);
            chd.ShowDialog(); 
            chd.Dispose(); //Deconstructor, free up memory.


            List<string> elist = new List<string>(); //Cast a list to store all emails from Class

            var equeue = new Queue<string>(elist); //Parse List into a Queue for appropiate pushing when Sending mail without pausing system.


            string duedate = DateTime.Now.AddDays(7).ToString("dd.MM.yy"); 

            //Sends mail, iterating over each layer in the Queue.
            foreach (var s in selectedClass.Students)
            {
                //do stuff here
                var processmail = new sMail();
                processmail.sendmail(email: s.Email, date: duedate);
            }


        }
        
        //Create a class
        private async void bunifuThinButton3_Click(object sender, EventArgs e)
        {
            var dialog = new CreateClassDialog(); //Constructor - creating new ClassDialog
            
            if(dialog.ShowDialog(this) == DialogResult.OK)
            {
                string name = dialog.Name;
                bool success = await ApiWrapper.CreateClass(name); //Parses upates on the Dialog to the API which updates the class information on the backend.
                sync();
            }
            dialog.Dispose();
        }
        
        //View Homework
        private async void bunifuThinButton2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1) 
            {
                var item = listView1.SelectedItems[0]; //Sets viewItem to 0, refrains application from crashing
                StudentHomework hw = (StudentHomework)item.Tag;

                if(hw == null) //Null is when the entity of the students homework is empty
                {
                    MessageBox.Show("Student has not yet submitted homework", "Not Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                SaveFileDialog sfd = new SaveFileDialog(); //Constructor, saveFileDialog
                if (sfd.ShowDialog() == DialogResult.OK) 
                {
                    using (var client = new WebClient()) //Constructor to create a new WebClient.
                    {
                        await client.DownloadFileTaskAsync(new Uri(hw.Url), sfd.FileName); //Downloads Homework according to when a student has subbmitted it.
                    }
                }
            }
            else if (listView1.SelectedItems.Count == 0) //Ensures teacher atleast selects an item
            {
                MessageBox.Show("Please select an item from the list!", "No item selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else //Ensures a teacher doesnt select multipe items
            {
                MessageBox.Show("Please select only one item!", "Multiple items selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Dedicated button to Sync, allows teacher to sync manually.
        private void bunifuTileButton3_Click(object sender, EventArgs e)
        {
            sync();
        }

        //Fetch and update the applicastions dropDown with all updated variables from the Backend.
        private async void sync()
        {
            if (classes != null)
            {
                classes.Clear(); //Cleares current classes so fresh list can be imported, - prevents collision of data.
            }
            bunifuDropdown1.Clear();
            classes = await ApiWrapper.GetClasses(); //Fetches all the latest classes from the WebHost

            foreach (var i in classes) 
            {

                bunifuDropdown1.AddItem(i.Name); //Adds new item down into the List.

            }

            //Verification that DropDown has successfully updated if the DropDown is set to 0.
            if (bunifuDropdown1.Items.Length > 0)
            {
                bunifuDropdown1.selectedIndex = 0;
                update(); //Calls Update, responsible for populating all devices
            }

        }

        //Once the DropDown has been updated, this causes the Lists and dataGrid to update.
        private async void update()
        {
            Console.WriteLine("update");
            Class c = await ApiWrapper.GetClass(selectedClass.Id);
            selectedClass = c;
            
            List<StudentHomework> shw = null;
            if(c.LastHomework != 0)
            { 
                Console.WriteLine("lasthw: " + c.LastHomework); 
                shw = await ApiWrapper.GetStudentHomeworks(c.LastHomework??0);  //Fetches studentHomwork, uses ApiWrapper to fetch appropiate data from Backend.
            }

            bunifuCircleProgressbar1.ResetText(); 
            bunifuCircleProgressbar1.MaxValue = c.Students.Count > 0 ? c.Students.Count : 1;
            int submitCount = 0;
            listView1.Items.Clear();
            foreach (var s in c.Students)
            {
                string submitted = "No"; 
                if(shw != null && shw.Exists(x => x.StudentId == s.Id)) //Verifies whether each student has submitted homework.
                {
                    submitted = "Yes";
                    submitCount++;
                }
                var item = new ListViewItem(new[] { s.Name, s.Email, submitted });  //Repopulate the ListView with all student information.
                item.Tag = shw.Find(X=>X.StudentId==s.Id);
                listView1.Items.Add(item);
            }

            bunifuCircleProgressbar1.Value = submitCount; //Updates the CircleProgessBar depending on how many students have submitted. 
            
            bunifuCircleProgressbar1.Refresh();
        }

        //When user wants to Sign out
        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            Hide();
            Form1.instance.Show();
        }

        //Opens new form when Teacher wants to manually edit a Class.
        private void button1_Click(object sender, EventArgs e)
        {
            var dialog = new ManageClassDialog(selectedClass);
            dialog.ShowDialog();
            dialog.Dispose();
        }

        private void bunifuCards1_VisibleChanged(object sender, EventArgs e)
        {
            if(Visible)
            {
                sync();
            }
        }

        private void bunifuCards2_Paint(object sender, PaintEventArgs e)
        {

        }

        //Edit Class
        private void bunifuThinButton1_Click_1(object sender, EventArgs e)
        {
            var dialog = new ManageClassDialog(selectedClass);
            dialog.ShowDialog();
            dialog.Dispose(); //Deconstructor, let's free up some client memory after we have opened a Dialog.
        }

        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
