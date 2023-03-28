using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MYSQL_CRUD_Student_Add_project
{
    public partial class FormStudent : Form
    {
        private readonly FormStudentInfo _parent;
        public FormStudent(FormStudentInfo parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate input
            if (txtName.Text.Trim().Length < 3)
            {
                MessageBox.Show("Please enter a valid student name (minimum 3 characters).");
                return;
            }

            if (txtReg.Text.Trim().Length < 1)
            {
                MessageBox.Show("Please enter a valid student registration number (minimum 1 character).");
                return;
            }

            if (txtClass.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter a valid student class (minimum 1 character).");
                return;
            }

            if (txtSection.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter a valid student section (minimum 1 character).");
                return;
            }

            try
            {
                // Add student to database
                Student std = new Student(txtName.Text.Trim(), txtReg.Text.Trim(), txtClass.Text.Trim(), txtSection.Text.Trim());
                DbStudent.AddStudent(std);
                

                // Refresh data in parent form
                _parent.Display();

                // Clear input fields
                txtName.Clear();
                txtReg.Clear();
                txtClass.Clear();
                txtSection.Clear();

                // Hide current form and show parent form
                this.Hide();
                _parent.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while adding the student: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
