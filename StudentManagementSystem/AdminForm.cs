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
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
            txtAdminData.SelectionChanged += txtAdminData_SelectionChanged;
        }

        public void clearControl()
        {
            txtAdminID.Text = "";
            txtEmail.Text = "";
            txtFname.Text = "";
            txtLname.Text = "";
            txtTelephone.Text = "";
            cbxAdminType.SelectedItem = "";
        }

        string connectionString = "Data Source=DESKTOP-QCVV1U7;Initial Catalog=studentMgtSystem;Integrated Security=True";


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAdminData_SelectionChanged(object sender, EventArgs e)
        {
            if (txtAdminData.SelectedRows.Count > 0)
            {
                txtAdminID.Text = txtAdminData.SelectedRows[0].Cells["admin_id"].Value.ToString();
                txtFname.Text = txtAdminData.SelectedRows[0].Cells["first_name"].Value.ToString();
                txtLname.Text = txtAdminData.SelectedRows[0].Cells["last_name"].Value.ToString();
                cbxAdminType.SelectedItem = txtAdminData.SelectedRows[0].Cells["admin_type"].Value.ToString();
                txtTelephone.Text = txtAdminData.SelectedRows[0].Cells["phone"].Value.ToString();
                txtEmail.Text = txtAdminData.SelectedRows[0].Cells["email"].Value.ToString();
                txtDate.Text = txtAdminData.SelectedRows[0].Cells["date"].Value.ToString();
                
            }
            else
            {
                txtAdminID.Text = "";
                txtFname.Text = "";
                txtLname.Text = "";
                txtEmail.Text = "";
                txtTelephone.Text = "";
                txtDate.Text = "";
                cbxAdminType.SelectedItem = "";
       }
        
    }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try {

                if (string.IsNullOrWhiteSpace(txtAdminID.Text) || string.IsNullOrWhiteSpace(txtFname.Text) || string.IsNullOrWhiteSpace(txtDate.Text) || cbxAdminType.SelectedItem == null)
                {
                    MessageBox.Show("Please Fill Out Required Fields!","Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString)){
                    
                    try{

                        connection.Open();

                        string insertQuery = "INSERT INTO administrator(admin_id, first_name, last_name, admin_type, phone, email, date) VALUES (@admin_id, @first_name, @last_name, @admin_type, @phone, @email, @date)";
                        using (SqlCommand command = new SqlCommand(insertQuery, connection)){
                            
                            command.Parameters.AddWithValue("@admin_id", txtAdminID.Text);
                            command.Parameters.AddWithValue("@first_name",txtFname.Text);
                            command.Parameters.AddWithValue("@last_name",txtLname.Text);
                            command.Parameters.AddWithValue("@admin_type",cbxAdminType.SelectedItem.ToString());
                            command.Parameters.AddWithValue("@phone",txtTelephone.Text);
                            command.Parameters.AddWithValue("@email",txtEmail.Text);
                            command.Parameters.AddWithValue("@date",txtDate.Value);

                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show("New Administator has been added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearControl();
                        AddAdminAccount addadmin = new AddAdminAccount();
                        this.Hide();
                        addadmin.Show();
                    
                    
                    }catch(Exception ex){
                        MessageBox.Show("Error:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }
            
            }
            catch (Exception ex) 
            {
                MessageBox.Show("An error occur:" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void createAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddAdminAccount addadmin = new AddAdminAccount();
            this.Hide();
            addadmin.Show();
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HomePage homepage = new HomePage();
            this.Hide();
            homepage.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT * FROM administrator";
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        txtAdminData.DataSource = dataTable;


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occur:" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    MessageBox.Show("Please Enter an Admin ID for Searching!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string search = txtSearch.Text;

                    string selectQuery = "SELECT admin_id, first_name, last_name, admin_type, phone, email, date FROM administrator WHERE admin_id = @admin_id";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@admin_id", search);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Update UI elements with retrieved values
                                txtAdminID.Text = reader["admin_id"].ToString();
                                txtFname.Text = reader["first_name"].ToString();
                                txtLname.Text = reader["last_name"].ToString();
                                cbxAdminType.SelectedItem = reader["admin_type"].ToString();
                                txtTelephone.Text = reader["phone"].ToString();
                                txtEmail.Text = reader["email"].ToString();
                                txtDate.Text = reader["date"].ToString();
                            }
                            else
                            {
                                // Clear UI elements if no matching record found
                                txtAdminID.Text = "";
                                txtFname.Text = "";
                                txtLname.Text = "";
                                cbxAdminType.SelectedItem = "";
                                txtTelephone.Text = "";
                                txtEmail.Text = "";
                                txtDate.Text = "";

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

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void coursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HomePage hp = new HomePage();
            this.Show();
            hp.Show();
        }

        private void createAccountToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AddAdminAccount ad = new AddAdminAccount();
            this.Hide();
            ad.Show();
        }    
   }
}
