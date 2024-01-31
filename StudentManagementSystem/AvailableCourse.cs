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
    public partial class AvailableCourse : Form
    {
        public AvailableCourse()
        {
            InitializeComponent();
            populateCourseID();
            txtTable.SelectionChanged += txtTable_selectionChanged;
        }

        string connectionString = "Data Source=DESKTOP-QCVV1U7;Initial Catalog=studentMgtSystem;Integrated Security=True";

        private void clearControl()
        {
            txtCourseName.Text = "";
            cbxCourseID.Text = "";
            cbxLevel.Text = "";
        }

        private void txtTable_selectionChanged(object sender, EventArgs e)
        {
            if (txtTable.SelectedRows.Count > 0)
            {
                cbxCourseID.Text = txtTable.SelectedRows[0].Cells["course_id"].Value.ToString();
                txtCourseName.Text = txtTable.SelectedRows[0].Cells["course_name"].Value.ToString();
                cbxLevel.Text = txtTable.SelectedRows[0].Cells["level"].Value.ToString();
            }
            else
            {
                cbxCourseID.Text = "";
                txtCourseName.Text = "";
                cbxLevel.Text = "";
            }
        }

        private void populateCourseID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string selectQuery = "SELECT course_id FROM course";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            cbxCourseID.Items.Add(reader["course_id"].ToString());
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurs: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string selectQuery = "SELECT * FROM course";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        txtTable2.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCourseName.Text) || cbxCourseID.SelectedItem == null || cbxLevel.SelectedItem == null)
                {
                    MessageBox.Show("All fields are required!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string courseID = cbxCourseID.SelectedItem.ToString();
                    string courseName = txtCourseName.Text;
                    string level = cbxLevel.SelectedItem.ToString();

                    string insertQuery = "INSERT INTO availableCourse(course_id, course_name, level) VALUES (@course_id, @course_name, @level)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@course_id", courseID);
                        cmd.Parameters.AddWithValue("@course_name", courseName);
                        cmd.Parameters.AddWithValue("@level", level);

                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("New course has been added to available courses", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurs: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Courses cou = new Courses();
            this.Hide();
            cou.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string selectQuery = "SELECT course_id, course_name, level FROM availableCourse";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        txtTable.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void coursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HomePage hp = new HomePage();
            this.Hide();
            hp.Show();
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoginPage lp = new LoginPage();
            this.Hide();
            lp.Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCourseName.Text) || cbxLevel.SelectedItem == null || cbxCourseID.SelectedItem == null)
                {
                    return;
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string updateQuery = "UPDATE availableCourse SET course_name = @course_name, level = @level WHERE course_id = @course_id";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@course_id", cbxCourseID.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@course_name", txtCourseName.Text);
                        cmd.Parameters.AddWithValue("@level", cbxLevel.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Record has been updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    MessageBox.Show("Enter course ID for searching", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string selectQuery = "SELECT * FROM availableCourse WHERE course_id = @course_id";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@course_id", txtSearch.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cbxCourseID.Text = reader["course_id"].ToString();
                                txtCourseName.Text = reader["course_name"].ToString();
                                cbxLevel.Text = reader["level"].ToString();
                            }

                            txtCourseName.Text = "";
                            cbxLevel.Text = "";
                            cbxCourseID.Text = "";
                            MessageBox.Show("No matching record found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCourseName.Text) || cbxCourseID.SelectedItem == null || cbxLevel.SelectedItem == null)
                {
                    MessageBox.Show("Fill out required fields", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string deleteQuery = "DELETE FROM availableCourse WHERE course_id = @course_id";
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@course_id", cbxCourseID.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Record has been deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
