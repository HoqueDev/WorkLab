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
    public partial class CreateClassDialog : Form
    {
        public string Name { get; private set; } //This publicly available, but privately set. All classes must be able to read this value.

        public CreateClassDialog()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Name = nameTextBox.Text; //Class name is parsed, this will get 'getted' by the API later.
            Close();
        }

        private void bunifuThinButton3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Name = nameTextBox.Text; //Class name is parsed, this will get 'getted' by the API later.
            Close();
        }

        private void bunifuThinButton1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
