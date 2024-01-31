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
    public partial class Marks : Form
    {
        public Marks()
        {
            InitializeComponent();
        }

        string connectionString = "Data Source=DESKTOP-QCVV1U7;Initial Catalog=studentMgtSystem;Integrated Security=True";

        private void marksToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
        }

        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TeacherDashboard td = new TeacherDashboard();
            this.Hide();
            td.Show();
        }

        private void marksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Enrollment enl = new Enrollment();
            this.Hide();
            enl.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCourseID.Text) || string.IsNullOrWhiteSpace(txtLecturerID.Text) || string.IsNullOrWhiteSpace(txtStudentID.Text) || string.IsNullOrWhiteSpace(txtMarks.Text))
                {
                    MessageBox.Show("Please fill out required fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string studentID = txtStudentID.Text;
                    string lecturerID = txtLecturerID.Text;
                    string courseID = txtCourseID.Text;
                    string courseTitle = txtCourseTitle.Text;
                    string marks = txtMarks.Text;

                    string insertQuery = "INSERT INTO marks (stud_id, course_id, course_name, stud_mark, lec_id, date) VALUES (@stud_id, @course_id, @course_name, @stud_mark, @lec_id, @date)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@stud_id", studentID);
                        cmd.Parameters.AddWithValue("@course_id", courseID);
                        cmd.Parameters.AddWithValue("@course_name", courseTitle);
                        cmd.Parameters.AddWithValue("@stud_mark", marks);
                        cmd.Parameters.AddWithValue("@lec_id", lecturerID);
                        cmd.Parameters.AddWithValue("@date", txtDate.Value);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Mark has been added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occur: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    MessageBox.Show("Enter course ID to search", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string selectQuery = "SELECT * FROM enrollment WHERE course_id = @course_id";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@course_id", txtSearch.Text);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                           
                            txtCourseID.Text = reader["course_id"].ToString();
                            txtCourseTitle.Text = reader["course_name"].ToString();
                           
                        }
                        else
                        {
                            txtCourseID.Text = "";
                            txtCourseTitle.Text = "";
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occur:" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
