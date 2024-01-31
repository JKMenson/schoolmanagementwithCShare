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
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
            
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


        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (cbxAdminType.SelectedItem != null)
                {
                    string selectedAdminType = cbxAdminType.SelectedItem.ToString();

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();


                        string username = txtUsername.Text;
                        string password = HashPassword(txtPassword.Text);

                        string selectQuery = "SELECT COUNT(*) FROM adminAccount WHERE username = @username AND password = @password AND admin_type = @admin_type";

                        using (SqlCommand command = new SqlCommand(selectQuery, connection))
                        {
                            command.Parameters.AddWithValue("@username", username);
                            command.Parameters.AddWithValue("@password", password);
                            command.Parameters.AddWithValue("@admin_type", selectedAdminType);

                            int count = (int)command.ExecuteScalar();

                            if (count > 0)
                            {
                                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (selectedAdminType == "Admin")
                                {
                                    HomePage homepage = new HomePage();
                                    this.Hide();
                                    homepage.Show();
                                }
                                else if (selectedAdminType == "User")
                                {
                                    TeacherDashboard td = new TeacherDashboard();
                                    this.Hide();
                                    td.Show();
                                }
                                // Perform actions after successful login
                            }
                            else
                            {
                                MessageBox.Show("Invalid username, password, or admin type. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select an admin type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception, for example, display an error message
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void cbxShowPassword_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
