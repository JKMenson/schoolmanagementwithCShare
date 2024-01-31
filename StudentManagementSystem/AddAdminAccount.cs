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
    public partial class AddAdminAccount : Form
    {
        public AddAdminAccount()
        {
            InitializeComponent();
            populateCombox();
        }

        string connectionString = "Data Source=DESKTOP-QCVV1U7;Initial Catalog=studentMgtSystem;Integrated Security=True";


        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }


        private void populateCombox()
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT first_name FROM administrator";
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cbxName.Items.Add(reader["first_name"].ToString());

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
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
                    string adminType = cbxAdminType.SelectedItem.ToString();
                    string adminName = cbxName.SelectedItem.ToString();
                    string username = txtUsername.Text;
                    string password = HashPassword(txtPassword.Text);
                    string confirmPassword = HashPassword(txtConfirmPassword.Text);
                    

                    // Check if passwords match
                    if (password != confirmPassword)
                    {
                        MessageBox.Show("Passwords do not match. Please confirm your password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Stop execution if passwords do not match
                    }

                    string insertQuery = "INSERT INTO adminAccount(admin_type, adminName, username, password) VALUES (@admin_type, @adminName, @username, @password)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        // Add parameters
                        command.Parameters.AddWithValue("@admin_type", adminType);
                        command.Parameters.AddWithValue("@adminName", adminName);
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);


                        // Execute the query
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Admin Account Has Been Added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AdminLogin adminlogin = new AdminLogin();
                    this.Hide();
                    adminlogin.Show();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, for example, display an error message
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxAdminID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminForm af = new AdminForm();
            this.Hide();
            af.Show();
        }
    }
}
