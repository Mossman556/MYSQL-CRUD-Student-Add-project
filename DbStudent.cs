using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MYSQL_CRUD_Student_Add_project
{
    internal class DbStudent
    {
        public static MySqlConnection GetConnection()
        {
            string sql = "datasource=localhost;port=3306;username=root;password=;database=student";
            MySqlConnection conn = new MySqlConnection(sql);
            try
            {
                conn.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MySQL Connect \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return conn;
        }

        public static void AddStudent(Student std)
        {
            string sql = "INSERT INTO student_table (Name, Reg, Class, Section) VALUES (@StudentName, @StudentReg, @StudentClass, @StudentSection)";
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@StudentName", std.Name);
            cmd.Parameters.AddWithValue("@StudentReg", std.Reg);
            cmd.Parameters.AddWithValue("@StudentClass", std.Class);
            cmd.Parameters.AddWithValue("@StudentSection", std.Section);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Added Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Please Add Student \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void UpdateStudent(Student std, string id)
        {
            string sql = "UPDATE student_table SET Name = @StudentName, Reg = @StudentReg, Class = @StudentClass, Section = @StudentSection WHERE ID = @StudentID";
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@StudentName", std.Name);
            cmd.Parameters.AddWithValue("@StudentReg", std.Reg);
            cmd.Parameters.AddWithValue("@StudentClass", std.Class);
            cmd.Parameters.AddWithValue("@StudentSection", std.Section);
            cmd.Parameters.AddWithValue("@StudentID", id);

            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Updated Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Student not updated \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void DeleteStudent(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                MySqlCommand cmd = new MySqlCommand("DELETE FROM student_table WHERE ID = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }


        public static void DisplayAndSearch(string query, DataGridView dgv)
        {
            if (dgv == null)
            {
                throw new ArgumentNullException(nameof(dgv), "The DataGridView control cannot be null.");
            }

            string sql = query;
            MySqlConnection conn = GetConnection();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataTable tbl = new DataTable();
            adp.Fill(tbl);

            Console.WriteLine("Number of columns in DataGridView: " + dgv.ColumnCount); // Add this line

            // Set the # column to display the ID number
            dgv.Columns["Column1"].HeaderText = "#";
            dgv.Columns["Column1"].DataPropertyName = "ID";

            // Display only the desired columns
            dgv.DataSource = tbl.DefaultView.ToTable(false, "ID", "Name", "Reg", "Class", "Section");

            conn.Close();
        }



    }
}
