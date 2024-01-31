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

namespace StudentManagementSystem
{
    public partial class JoinCourse : Form
    {
        public JoinCourse()
        {
            InitializeComponent();
        }

        string connectionString = "Data Source=DESKTOP-QCVV1U7;Initial Catalog=studentMgtSystem;Integrated Security=True";

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentDashboard sd = new StudentDashboard();
            this.Hide();
            sd.Show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    MessageBox.Show("Enter course ID to search available for enrollment", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string selectQuery = "SELECT course_id, course_name FROM availableCourse WHERE course_id = @course_id";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@course_id", txtSearch.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtCourseID.Text = reader["course_id"].ToString();
                                txtCouseName.Text = reader["course_name"].ToString();
                            }
                            else
                            {
                                txtCourseID.Text = "";
                                txtCouseName.Text = "";
                                MessageBox.Show("No matching record found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(txtSearch.Text))
                    {
                        MessageBox.Show("Enter student ID to search and then enroll", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        string insertQuery = "INSERT INTO enrollment (stud_id, course_id, course_name, date) VALUES (@stud_id, @course_id, @course_name, @date)";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@stud_id", txtStudentID.Text);
                            cmd.Parameters.AddWithValue("@course_id", txtCourseID.Text);
                            cmd.Parameters.AddWithValue("@course_name", txtCouseName.Text);
                            cmd.Parameters.AddWithValue("@date", txtDate.Value);

                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("You have successfully enrolled into '" + txtCouseName.Text + "'", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string selectQuery = "SELECT * FROM enrollment";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        txtTable.AutoGenerateColumns = false;
                        txtTable.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error Occur:" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
