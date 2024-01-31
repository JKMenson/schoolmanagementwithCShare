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
    public partial class Courses : Form
    {
        public Courses()
        {
            InitializeComponent();
            populateData();
            txtTable.SelectionChanged += txtTable_SelectionChanged;
        }

        string connectionString = "Data Source=DESKTOP-QCVV1U7;Initial Catalog=studentMgtSystem;Integrated Security=True";

        
        //Clear fieds
        private void clearControl()
        {
            txtCourseID.Text = "";
            txtCourseName.Text = "";
            cbxDepartID.SelectedItem = "";
        }

        //Populate department ID
        private void populateData()
        {
            try {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT dept_id FROM department";
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            cbxDepartID.Items.Add(reader["dept_id"].ToString());
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occur:" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try {
                if (string.IsNullOrWhiteSpace(txtCourseID.Text) || string.IsNullOrWhiteSpace(txtCourseName.Text) ||cbxDepartID.SelectedItem == null)
                {
                    MessageBox.Show("All Fields Are Required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string courseID = txtCourseID.Text;
                    string courseTitle = txtCourseName.Text;
                    string departID = cbxDepartID.SelectedItem.ToString();

                    string insertQuery = "INSERT INTO course(course_id, course_name, dept_id) VALUES (@course_id, @course_name, @dept_id)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@course_id", courseID);
                        command.Parameters.AddWithValue("@course_name", courseTitle);
                        command.Parameters.AddWithValue("@dept_id", departID);

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("New course has been added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occur:" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    MessageBox.Show("Please Enter CourseID To Search", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT course_id, course_name, dept_id FROM course WHERE course_id = @course_id";
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@course_id", txtSearch.Text);

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            txtCourseID.Text = reader["course_id"].ToString();
                            txtCourseName.Text = reader["course_name"].ToString();
                            cbxDepartID.SelectedItem = reader["dept_id"].ToString();
                        }

                        else
                        {
                            txtCourseID.Text = "";
                            txtCourseName.Text = "";
                            cbxDepartID.SelectedItem = "";
                            MessageBox.Show("No Such Course Found, Please Try Again!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error ocure" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM course";
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        txtTable.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occur:" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTable_SelectionChanged(object sender, EventArgs e)
        {
            if (txtTable.SelectedRows.Count > 0)
            {
                txtCourseID.Text = txtTable.SelectedRows[0].Cells["course_id"].Value.ToString();
                txtCourseName.Text = txtTable.SelectedRows[0].Cells["course_name"].Value.ToString();
                cbxDepartID.Text = txtTable.SelectedRows[0].Cells["dept_id"].Value.ToString();
            }
            else
            {
                txtCourseID.Text = "";
                txtCourseName.Text = "";
                cbxDepartID.Text = ""; 
            }
        }

        private void txtTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCourseID.Text))
                {
                    MessageBox.Show("Enter Course ID to update", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    String updateQuery = "UPDATE course SET course_name = @course_name, dept_id = @dept_id WHERE course_id = @course_id";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@course_id", txtCourseID.Text);
                        cmd.Parameters.AddWithValue("@course_name", txtCourseName.Text);
                        cmd.Parameters.AddWithValue("@dept_id", cbxDepartID.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Course has been updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occur:" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCourseID.Text))
                {
                    MessageBox.Show("Enter course ID to delete!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string deletQuery = "DELETE FROM course WHERE course_id = @course_id";
                    using (SqlCommand cmd = new SqlCommand(deletQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@course_id", txtCourseID.Text);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Couse has been deleted!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occur:" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCourseID.Text = "";
            txtCourseName.Text = "";
            cbxDepartID.SelectedItem = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HomePage hp = new HomePage();
            this.Hide();
            hp.Show();
        }

        private void marksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HomePage hp = new HomePage();
            this.Hide();
            hp.Show();
        }

        private void marksToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HomePage hp = new HomePage();
            this.Hide();
            hp.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            AvailableCourse ac = new AvailableCourse();
            this.Hide();
            ac.Show();
        }

        private void cbxDepartID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void coursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HomePage hp = new HomePage();
            this.Hide();
            hp.Show();
        }
    }
}
