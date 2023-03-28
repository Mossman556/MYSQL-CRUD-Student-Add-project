using MySql.Data.MySqlClient;
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
    public partial class FormStudentInfo : Form

    {
        public FormStudentInfo()
        {
            InitializeComponent();
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);


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

        


        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == Column7.Index) // Check if the delete button column is clicked
            {
                DeleteStudent(e.RowIndex);
            }

            // Check if the clicked cell is the "Edit" button column
            if (dataGridView.Columns[e.ColumnIndex].Name == "btnEditStudent")
            {
                // Get the ID of the selected student
                int studentID = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["Column1"].Value);

                // Open the EditStudentForm with the selected student's ID
                EditStudentForm editStudentForm = new EditStudentForm(studentID, this);
                this.Hide();
                Display();
                editStudentForm.FormClosed += (s, args) =>
                {
                    // Refresh the DataGridView after closing the EditStudentForm
                    // Add your method to refresh the DataGridView, e.g., RefreshDataGridView();

                    this.Show(); // Show the FormStudentInfo form again
                };
                this.Hide();
                editStudentForm.ShowDialog();
                
            }
        }

        private MySqlConnection GetConnection()
        {
            string connectionString = "datasource=localhost;port=3306;username=root;password=;database=student";
            return new MySqlConnection(connectionString);
        }

        private void SearchStudents(string searchText)
        {
            string sql = "SELECT ID, Name, Reg, Class, Section FROM student_table WHERE Name LIKE @SearchText OR Reg LIKE @SearchText OR Class LIKE @SearchText OR Section LIKE @SearchText";
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

            DataTable dataTable = new DataTable();
            try
            {
                conn.Open();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
                dataAdapter.Fill(dataTable);
                dataGridView.DataSource = dataTable;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error searching students \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                Display(); // Refresh the DataGridView if the search textbox is empty
            }
            else
            {
                SearchStudents(txtSearch.Text);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SearchStudents(txtSearch.Text);
        }



        private void FormStudentInfo_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        


        public void Display()
        {
            if (dataGridView != null)
            {
                DbStudent.DisplayAndSearch("SELECT ID, Name, Reg, Class, Section FROM student_table", dataGridView);
            }
            else
            {
                // Handle the case where the dataGridView control is null

            }
        }



        private void btnNew_Click(object sender, EventArgs e)
        {
            FormStudent form = new FormStudent(this);
            this.Hide();
            form.FormClosed += (s, args) => this.Show(); // Show the FormStudentInfo form again when the FormStudent form is closed
            form.Show();
        }

        private void DeleteStudent(int rowIndex)
        {
            int id = Convert.ToInt32(dataGridView.Rows[rowIndex].Cells["Column1"].Value);
            DialogResult result = MessageBox.Show("Are you sure you want to delete this student?", "Delete Student", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                DbStudent.DeleteStudent(id);
                Display();
                MessageBox.Show("Student deleted successfully.", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }





        private void FormStudetInfo_Shown(object sender, EventArgs e)
        {
            Display();
        }
    }
}
