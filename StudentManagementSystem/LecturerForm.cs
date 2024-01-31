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
    public partial class LecturerForm : Form
    {
        public LecturerForm()
        {
            InitializeComponent();
            populateCombox();
            clearControl();
            txtTable.SelectionChanged += txtTable_SelectionChanged;
        }

        string connectionString = "Data Source=DESKTOP-QCVV1U7;Initial Catalog=studentMgtSystem;Integrated Security=True";

        private void clearControl()
        {
            txtLecturerID.Text = "";
            txtFname.Text = "";
            txtLname.Text = "";
            txtTelephone.Text = "";
            txtEmail.Text = "";
            cbxDepartment.Text = "";
        }

        private void txtTable_SelectionChanged(object sender, EventArgs e)
        {
            if (txtTable.SelectedRows.Count > 0)
            {
                txtLecturerID.Text = txtTable.SelectedRows[0].Cells["lec_id"].Value.ToString();
                txtFname.Text = txtTable.SelectedRows[0].Cells["first_name"].Value.ToString();
                txtLname.Text = txtTable.SelectedRows[0].Cells["last_name"].Value.ToString();
                cbxDepartment.Text = txtTable.SelectedRows[0].Cells["dept_id"].Value.ToString();
                txtTelephone.Text = txtTable.SelectedRows[0].Cells["phone"].Value.ToString();
                txtEmail.Text = txtTable.SelectedRows[0].Cells["email"].Value.ToString();
                txtDate.Text = txtTable.SelectedRows[0].Cells["hiredDate"].Value.ToString();
            }
            else
            {
                txtLecturerID.Text = "";
                txtFname.Text = "";
                txtLname.Text = "";
                cbxDepartment.Text = "";
                txtTelephone.Text = "";
                txtEmail.Text = "";
                txtDate.Text = "";
            }
        }


        private void populateCombox()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT dept_id FROM department";
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        cbxDepartment.Items.Add(reader["dept_id"].ToString());
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try {

                if (string.IsNullOrWhiteSpace(txtLecturerID.Text) || string.IsNullOrWhiteSpace(txtFname.Text) || string.IsNullOrWhiteSpace(txtLname.Text) || cbxDepartment.SelectedItem == null)
                {
                    MessageBox.Show("Please fill out required fields!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    string lec_id = txtLecturerID.Text;
                    string first_name = txtFname.Text;
                    string last_name = txtFname.Text;
                    string dept_id = cbxDepartment.SelectedItem.ToString();
                    string phone = txtTelephone.Text;
                    string email = txtEmail.Text;
                    
                    connection.Open();

                    string insertQuery = "INSERT INTO lecturer(lec_id, first_name, last_name, dept_id, phone, email, hiredDate) VALUES (@lec_id, @first_name, @last_name, @dept_id, @phone, @email, @hiredDate)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@lec_id", lec_id);
                        command.Parameters.AddWithValue("@first_name", first_name);
                        command.Parameters.AddWithValue("@last_name", last_name);
                        command.Parameters.AddWithValue("@dept_id", dept_id);
                        command.Parameters.AddWithValue("@phone", phone);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@hiredDate", txtDate.Value);

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("New lecturer has been added!", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occur:" + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtLecturerID.Text) || string.IsNullOrWhiteSpace(txtFname.Text) || string.IsNullOrWhiteSpace(txtLname.Text) || string.IsNullOrWhiteSpace(txtTelephone.Text) || cbxDepartment.SelectedItem == null)
                {
                    MessageBox.Show("Seach record to be updated!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string updateQuery = "UPDATE lecturer SET first_name = @first_name, last_name = @last_name, dept_id = @dept_id, phone = @phone, email = @email, hiredDate = @hiredDate WHERE lec_id = @lec_id";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@lec_id", txtLecturerID.Text);
                        cmd.Parameters.AddWithValue("@first_name", txtFname.Text);
                        cmd.Parameters.AddWithValue("@last_name", txtLname.Text);
                        cmd.Parameters.AddWithValue("@dept_id", cbxDepartment.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@phone", txtTelephone.Text);
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@hiredDate", txtDate.Value);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Lecturer record has been updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error Occur!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    MessageBox.Show("Enter lecturer ID for searching!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string selectQuery = "SELECT * FROM lecturer WHERE lec_id = @lec_id";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@lec_id", txtSearch.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtLecturerID.Text = reader["lec_id"].ToString();
                                txtFname.Text = reader["first_name"].ToString();
                                txtLname.Text = reader["last_name"].ToString();
                                cbxDepartment.Text = reader["dept_id"].ToString();
                                txtTelephone.Text = reader["phone"].ToString();
                                txtEmail.Text = reader["email"].ToString();
                                txtDate.Text = reader["hiredDate"].ToString();
                            }
                            else
                            {
                                txtLecturerID.Text = "";
                                txtFname.Text = "";
                                txtLname.Text = "";
                                cbxDepartment.Text = "";
                                txtTelephone.Text = "";
                                txtEmail.Text = "";
                                txtDate.Text = "";
                            }
                            MessageBox.Show("No matching record found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occur:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string selectQuery = "SELECT * FROM lecturer";
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
                MessageBox.Show("Error Occur:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtLecturerID.Text) || string.IsNullOrWhiteSpace(txtFname.Text) || string.IsNullOrWhiteSpace(txtLname.Text) || string.IsNullOrWhiteSpace(txtTelephone.Text) || cbxDepartment.SelectedItem == null)
                {
                    MessageBox.Show("Seach record to be deleted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string updateQuery = "DELETE FROM lecturer WHERE lec_id = @lec_id";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@lec_id", txtLecturerID.Text);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Lecturer record has been deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error Occur: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearControl();

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

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void backToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            HomePage hp = new HomePage();
            this.Hide();
            hp.Show();
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
