using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;



namespace MYSQL_CRUD_Student_Add_project
{

    public partial class EditStudentForm : Form
    {

        private FormStudentInfo _formStudentInfo;
        public EditStudentForm(int studentID, FormStudentInfo formStudentInfo)
        {
            InitializeComponent();
            LoadStudentData(studentID);
            _formStudentInfo = formStudentInfo;
        }




        private void LoadStudentData(int studentID)
        {
            string sql = "SELECT ID, Name, Reg, Class, Section FROM student_table WHERE ID = @StudentID";
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@StudentID", studentID);

            try
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtStudentID.Text = reader["ID"].ToString();
                    txtName.Text = reader["Name"].ToString();
                    txtReg.Text = reader["Reg"].ToString();
                    txtClass.Text = reader["Class"].ToString();
                    txtSection.Text = reader["Section"].ToString();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error loading student data \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }
       





        private void UpdateStudentRecord()
        {
            string id = txtStudentID.Text;
            string name = txtName.Text;
            string reg = txtReg.Text;
            string className = txtClass.Text;
            string section = txtSection.Text;

            string sql = "UPDATE student_table SET Name = @Name, Reg = @Reg, Class = @Class, Section = @Section WHERE ID = @StudentID";
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@StudentID", id);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Reg", reg);
            cmd.Parameters.AddWithValue("@Class", className);
            cmd.Parameters.AddWithValue("@Section", section);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Record Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Student Record not Updated \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
               
                
            }
        }

        private MySqlConnection GetConnection()
        {
            string connectionString = "datasource=localhost;port=3306;username=root;password=;database=student";
            return new MySqlConnection(connectionString);
        }




        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateStudentRecord();
            _formStudentInfo.Show();
            this.Close();
        }

        internal class DataGridViewCellEventHandler
        {
            private Action<object, DataGridViewCellEventArgs> dataGridView_CellContentClick;

            public DataGridViewCellEventHandler(Action<object, DataGridViewCellEventArgs> dataGridView_CellContentClick)
            {
                this.dataGridView_CellContentClick = dataGridView_CellContentClick;
            }
        }
    }
}
