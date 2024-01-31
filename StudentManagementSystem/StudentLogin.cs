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
    public partial class StudentLogin : Form
    {
        public StudentLogin()
        {
            InitializeComponent();
        }

        string connectionString = "Data Source=DESKTOP-QCVV1U7;Initial Catalog=studentMgtSystem;Integrated Security=True";

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

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
                if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("All fields are required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                string username = txtUsername.Text;
                string password = HashPassword(txtPassword.Text);

                string selectQuery = "SELECT COUNT(*) FROM studentAccount WHERE username = @username AND password = @password";
                using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("You have login successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        StudentDashboard sd = new StudentDashboard();
                        this.Hide();
                        sd.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username, password, or admin type. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
               }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occur: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
