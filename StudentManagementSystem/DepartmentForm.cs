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
    public partial class DepartmentForm : Form
    {
        public DepartmentForm()
        {
            InitializeComponent();
            PopulateFacultyComboBox();
            txtTable.SelectionChanged += txtTable_SelectionChanged;
        }

        public void clearControl()
        {
            txtDepartID.Text = "";
            txtDepartName.Text = "";
            cbxFacultyID.Text = "";
        }

        string connectionString = "Data Source=DESKTOP-QCVV1U7;Initial Catalog=studentMgtSystem;Integrated Security=True";
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DepartmentForm_Load(object sender, EventArgs e)
        {

        }

        private void babkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HomePage homepage = new HomePage();
            this.Hide();
            homepage.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void txtTable_SelectionChanged(object sender, EventArgs e)
        {
            // Check if there is a selected row
            if (txtTable.SelectedRows.Count > 0)
            {
                // Assuming your DataGridView has columns named "faculty_id", "faculty_name", and "location"
                txtDepartID.Text = txtTable.SelectedRows[0].Cells["dept_id"].Value.ToString();
                txtDepartName.Text = txtTable.SelectedRows[0].Cells["deptName"].Value.ToString();
                cbxFacultyID.Text = txtTable.SelectedRows[0].Cells["faculty_id"].Value.ToString();
            }
            else
            {
                // Clear the text fields if no row is selected
                txtDepartID.Text = "";
                txtDepartName.Text = "";
                cbxFacultyID.Text = "";
            }
        }

        private void PopulateFacultyComboBox()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Example query to retrieve faculty names from the database
                    string query = "SELECT faculty_id FROM faculty";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Add each faculty name to the ComboBox
                                cbxFacultyID.Items.Add(reader["faculty_id"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try {

                //Validate input
                if(string.IsNullOrWhiteSpace(txtDepartID.Text) || string.IsNullOrWhiteSpace(txtDepartName.Text) || cbxFacultyID.SelectedItem == null){
                    MessageBox.Show("All Fields Are Required!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    try {
                        connection.Open();

                        string insertQuery = "INSERT INTO department (dept_id, deptName, faculty_id) VALUES (@dept_id, @deptName, @faculty_id)";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@dept_id", txtDepartID.Text);
                            command.Parameters.AddWithValue("@deptName", txtDepartName.Text);
                            command.Parameters.AddWithValue("@faculty_id", cbxFacultyID.SelectedItem.ToString());  

                            command.ExecuteNonQuery();
                        }
                        MessageBox.Show("New Department Has Been Added!", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearControl();
                       
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error!" +ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occur:" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try {

                //Validate input
                if(string.IsNullOrWhiteSpace(txtDepartID.Text) || string.IsNullOrWhiteSpace(txtDepartName.Text) || cbxFacultyID.SelectedItem == null){
                    MessageBox.Show("Please Record To Update!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    try {
                        connection.Open();

                        string insertQuery = "UPDATE department SET deptName = @deptName, faculty_id = @faculty_id WHERE dept_id = @dept_id";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@dept_id", txtDepartID.Text);
                            command.Parameters.AddWithValue("@deptName", txtDepartName.Text);
                            command.Parameters.AddWithValue("@faculty_id", cbxFacultyID.SelectedItem.ToString());  

                            command.ExecuteNonQuery();
                        }
                        MessageBox.Show("Department Has Been Updated!", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearControl();
                       
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error!" +ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occur:" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM department";
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                //Validate input
                if (string.IsNullOrWhiteSpace(txtDepartID.Text) || string.IsNullOrWhiteSpace(txtDepartName.Text) || cbxFacultyID.SelectedItem == null)
                {
                    MessageBox.Show("Please Record To Delete!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        string insertQuery = "DELETE FROM department WHERE dept_id = @dept_id";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@dept_id", txtDepartID.Text);
                            command.Parameters.AddWithValue("@deptName", txtDepartName.Text);
                            command.Parameters.AddWithValue("@faculty_id", cbxFacultyID.SelectedItem.ToString());

                            command.ExecuteNonQuery();
                        }
                        MessageBox.Show("Department Has Been Deleted!", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearControl();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occur:" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearControl();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT dept_id, deptName, faculty_id FROM department WHERE dept_id = @dept_id";
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@dept_id", txtSearch.Text);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtDepartID.Text = reader["dept_id"].ToString();
                                txtDepartName.Text = reader["deptName"].ToString();
                                cbxFacultyID.Text = reader["faculty_id"].ToString();
                            }
                            else
                            {
                                txtDepartID.Text = "";
                                txtDepartName.Text = "";
                                cbxFacultyID.Text = "";
                                MessageBox.Show("No matching record found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void coursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HomePage hp = new HomePage();
            this.Hide();
            hp.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoginPage lp = new LoginPage();
            this.Hide();
            lp.Show();
        }
    }
}
