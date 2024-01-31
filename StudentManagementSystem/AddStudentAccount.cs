using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace StudentManagementSystem
{
    public partial class AddStudentAccount : Form 
    {
        public AddStudentAccount()
        {
            InitializeComponent();
            populateStudent();
        }

        private void clearControl()
        {
            txtUsername.Text = "";
            txtUsername.Text = "";
            txtConfirmPassword.Text = "";
        }

        string connectionString = "Data Source=DESKTOP-QCVV1U7;Initial Catalog=studentMgtSystem;Integrated Security=True";


        private void populateStudent()
        {
            try {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT stud_id FROM student";
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cbxStudentID.Items.Add(reader["stud_id"].ToString());
                            }
                        }
                    }

                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occur:" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Get values from text boxes
                    string stud_id = cbxStudentID.SelectedItem.ToString();
                    string username = txtUsername.Text;
                    string password = HashPassword(txtPassword.Text);
                    string confirmPassword = HashPassword(txtConfirmPassword.Text);

                    // Check if passwords match
                    if (password != confirmPassword)
                    {
                        MessageBox.Show("Passwords do not match. Please confirm your password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Stop execution if passwords do not match
                    }

                    string insertQuery = "INSERT INTO studentAccount(stud_id, username, password) VALUES (@stud_id, @username, @password)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@stud_id", stud_id);
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        // Execute the query
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Student Account Has Been Added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    StudentLogin studentlogin = new StudentLogin();
                    this.Hide();
                    studentlogin.Show();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, for example, display an error message
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentForm sf = new StudentForm();
            this.Hide();
            sf.Show();
        }
    }
}
