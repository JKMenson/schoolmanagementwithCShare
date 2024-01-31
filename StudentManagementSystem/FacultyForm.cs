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
    public partial class FacultyForm : Form
    {
        public FacultyForm()
        {
            InitializeComponent();
            txtTable.SelectionChanged += txtTable_SelectionChanged;
        }

        private void clearControl()
        {
            txtFaculty.Text = "";
            txtFname.Text = "";
            txtLocation.Text = "";
        }


        string connectionString = "Data Source=DESKTOP-QCVV1U7;Initial Catalog=studentMgtSystem;Integrated Security=True";
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void txtTable_SelectionChanged(object sender, EventArgs e)
        {
            // Check if there is a selected row
            if (txtTable.SelectedRows.Count > 0)
            {
                // Assuming your DataGridView has columns named "faculty_id", "faculty_name", and "location"
                txtFaculty.Text = txtTable.SelectedRows[0].Cells["faculty_id"].Value.ToString();
                txtFname.Text = txtTable.SelectedRows[0].Cells["faculty_name"].Value.ToString();
                txtLocation.Text = txtTable.SelectedRows[0].Cells["location"].Value.ToString();
            }
            else
            {
                // Clear the text fields if no row is selected
                txtFaculty.Text = "";
                txtFname.Text = "";
                txtLocation.Text = "";
            }
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtFaculty.Text) || string.IsNullOrWhiteSpace(txtFname.Text) || string.IsNullOrWhiteSpace(txtLocation.Text))
                {
                    MessageBox.Show("All Fields Are Required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        string insertQuery = "INSERT INTO faculty(faculty_id, faculty_name, location) VALUES (@faculty_id, @faculty_name, @location)";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@faculty_id", txtFaculty.Text);
                            command.Parameters.AddWithValue("@faculty_name", txtFname.Text);
                            command.Parameters.AddWithValue("@location", txtLocation.Text);

                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show("New Faculty has been added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearControl();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        // Ensure the connection is closed
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtFaculty.Text) || string.IsNullOrWhiteSpace(txtFname.Text) || string.IsNullOrWhiteSpace(txtLocation.Text))
                {
                    MessageBox.Show("Select Record To Update!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        string updatetQuery = "UPDATE faculty SET faculty_name = @faculty_name, location = @location WHERE faculty_id = @faculty_id";
                        using (SqlCommand command = new SqlCommand(updatetQuery, connection))
                        {
                            command.Parameters.AddWithValue("@faculty_id", txtFaculty.Text);
                            command.Parameters.AddWithValue("@faculty_name", txtFname.Text);
                            command.Parameters.AddWithValue("@location", txtLocation.Text);

                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show("Faculty has been updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearControl();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        // Ensure the connection is closed
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM faculty";
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        // Assuming your DataGridView is named txtTable
                        txtTable.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtFaculty.Text) || string.IsNullOrWhiteSpace(txtFname.Text) || string.IsNullOrWhiteSpace(txtLocation.Text))
                {
                    MessageBox.Show("Select Record To Delete!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Confirmation message
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();

                            string deleteQuery = "DELETE FROM faculty WHERE faculty_id = @faculty_id";
                            using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                            {
                                command.Parameters.AddWithValue("@faculty_id", txtFaculty.Text);

                                command.ExecuteNonQuery();
                            }

                            MessageBox.Show("Faculty has been deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clearControl();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            // Ensure the connection is closed
                            if (connection.State == ConnectionState.Open)
                            {
                                connection.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clearControl();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    MessageBox.Show("Please enter a faculty ID for searching.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT faculty_id, faculty_name, location FROM faculty WHERE faculty_id = @faculty_id";
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        // Assuming "faculty_id" is the parameter name in your query
                        command.Parameters.AddWithValue("@faculty_id", txtSearch.Text);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate text fields with the retrieved data
                                txtFaculty.Text = reader["faculty_id"].ToString();
                                txtFname.Text = reader["faculty_name"].ToString();
                                txtLocation.Text = reader["location"].ToString();
                            }
                            else
                            {
                                // Clear text fields if no matching record found
                                txtFaculty.Text = "";
                                txtFname.Text = "";
                                txtLocation.Text = "";
                                MessageBox.Show("No matching record found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HomePage homepage = new HomePage();
            this.Hide();
            homepage.Show();
        }

        private void txtTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            LoginPage lp = new LoginPage();
            this.Hide();
            lp.Show();
        }
    }
}