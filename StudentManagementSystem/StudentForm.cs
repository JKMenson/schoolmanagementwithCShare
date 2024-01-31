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
    public partial class StudentForm : Form
    {
        public StudentForm()
        {
            InitializeComponent();
            populateDepartment();
            clearControl();
            txtTable.SelectionChanged += txtTable_SelectionChanged;
        }

        string connectionString = "Data Source=DESKTOP-QCVV1U7;Initial Catalog=studentMgtSystem;Integrated Security=True";

        private void txtTable_SelectionChanged(object sender, EventArgs e)
        {
            if (txtTable.SelectedRows.Count > 0)
            {
                txtStudentID.Text = txtTable.SelectedRows[0].Cells["stud_id"].Value.ToString();
                txtFname.Text = txtTable.SelectedRows[0].Cells["first_name"].Value.ToString();
                txtLname.Text = txtTable.SelectedRows[0].Cells["last_name"].Value.ToString();
                cbxDepartment.Text = txtTable.SelectedRows[0].Cells["dept_id"].Value.ToString();
                txtTelephone.Text = txtTable.SelectedRows[0].Cells["phone"].Value.ToString();
                txtEmail.Text = txtTable.SelectedRows[0].Cells["email"].Value.ToString();
                txtDate.Text = txtTable.SelectedRows[0].Cells["date"].Value.ToString();
                txtAdyear.Text = txtTable.SelectedRows[0].Cells["academic_year"].Value.ToString();
                cbxLevel.Text = txtTable.SelectedRows[0].Cells["level"].Value.ToString();
            }
            else
            {
                txtStudentID.Text = "";
                txtFname.Text = "";
                txtLname.Text = "";
                cbxDepartment.Text = "";
                txtTelephone.Text = "";
                txtEmail.Text = "";
                txtDate.Text = "";
                txtAdyear.Text = "";
                cbxLevel.Text = "";
            }
        }


        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HomePage homepage = new HomePage();
            this.Hide();
            homepage.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void populateDepartment()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string selectQuery = "SELECT dept_id FROM department";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            cbxDepartment.Items.Add(reader["dept_id"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
              MessageBox.Show("An Error Occur" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clearControl()
        {
            txtStudentID.Text = "";
            txtFname.Text = "";
            txtLname.Text = "";
            txtTelephone.Text = "";
            txtEmail.Text = "";
            cbxDepartment.Text = "";
            txtAdyear.Text = "";
            cbxLevel.Text = "";
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtStudentID.Text) || string.IsNullOrWhiteSpace(txtFname.Text) || string.IsNullOrWhiteSpace(txtLname.Text) || string.IsNullOrWhiteSpace(txtDate.Text) || cbxDepartment.SelectedItem == null)
                {
                    MessageBox.Show("Please Fill Out Required Fields!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string studentID = txtStudentID.Text;
                    string first_name = txtFname.Text;
                    string last_name = txtLname.Text;
                    string phone = txtTelephone.Text;
                    string email = txtEmail.Text;
                    string dept_id = cbxDepartment.SelectedItem.ToString();
                    string academic = txtAdyear.Text;
                    string level = cbxLevel.SelectedItem.ToString();

                    string insertQuery = "INSERT INTO student (stud_id, first_name, last_name, dept_id, phone, email, date, academic_year, level ) VALUES (@stud_id, @first_name, @last_name, @dept_id, @phone, @email, @date, @academic_year, @level)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@stud_id", studentID);
                        cmd.Parameters.AddWithValue("@first_name", first_name);
                        cmd.Parameters.AddWithValue("@last_name", last_name);
                        cmd.Parameters.AddWithValue("@dept_id", dept_id);
                        cmd.Parameters.AddWithValue("@phone", phone);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@date", txtDate.Value);
                        cmd.Parameters.AddWithValue("academic_year", academic);
                        cmd.Parameters.AddWithValue("@level", level);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("New student has been added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearControl();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtStudentID.Text) || string.IsNullOrWhiteSpace(txtFname.Text) || string.IsNullOrWhiteSpace(txtLname.Text) || string.IsNullOrWhiteSpace(txtTelephone.Text) || cbxDepartment.SelectedItem == null)
                {
                    MessageBox.Show("Seach record to be updated!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string updateQuery = "UPDATE student SET first_name = @first_name, last_name = @last_name, dept_id = @dept_id, phone = @phone, email = @email, date = @date, academic_year = @academic_year, level = @level WHERE stud_id = @stud_id";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@stud_id", txtStudentID.Text);
                        cmd.Parameters.AddWithValue("@first_name", txtFname.Text);
                        cmd.Parameters.AddWithValue("@last_name", txtLname.Text);
                        cmd.Parameters.AddWithValue("@dept_id", cbxDepartment.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@phone", txtTelephone.Text);
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@date", txtDate.Value);
                        cmd.Parameters.AddWithValue("academic_year", txtAdyear.Text);
                        cmd.Parameters.AddWithValue("@level", cbxLevel.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Student record has been updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error Occur!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtStudentID.Text) || string.IsNullOrWhiteSpace(txtFname.Text) || string.IsNullOrWhiteSpace(txtLname.Text) || string.IsNullOrWhiteSpace(txtTelephone.Text) || cbxDepartment.SelectedItem == null)
                {
                    MessageBox.Show("Seach record to be deleted!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string updateQuery = "DELETE FROM student WHERE stud_id = @stud_id";
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@stud_id", txtStudentID.Text);
                        cmd.Parameters.AddWithValue("@first_name", txtFname.Text);
                        cmd.Parameters.AddWithValue("@last_name", txtLname.Text);
                        cmd.Parameters.AddWithValue("@dept_id", cbxDepartment.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@phone", txtTelephone.Text);
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@date", txtDate.Value);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Student record has been deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error Occur!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    MessageBox.Show("Enter student for searching", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string selectQuery = "SELECT * FROM student WHERE stud_id = @stud_id";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@stud_id", txtSearch.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                txtStudentID.Text = reader["stud_id"].ToString();
                                txtFname.Text = reader["first_name"].ToString();
                                txtLname.Text = reader["last_name"].ToString();
                                cbxDepartment.Text = reader["dept_id"].ToString();
                                txtTelephone.Text = reader["phone"].ToString();
                                txtEmail.Text = reader["email"].ToString();
                                txtDate.Text = reader["date"].ToString();
                                txtAdyear.Text = reader["academic_year"].ToString();
                                cbxLevel.Text = reader["level"].ToString();
                            }
                            else
                            {
                                txtStudentID.Text = "";
                                txtFname.Text = "";
                                txtLname.Text = "";
                                cbxDepartment.Text = "";
                                txtTelephone.Text = "";
                                txtEmail.Text = "";
                                txtDate.Text = "";
                                txtAdyear.Text = "";
                                cbxLevel.Text = "";
                                MessageBox.Show("No matching record found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("An Error Occur" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string selectQuery = "SELECT * FROM student";
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
                MessageBox.Show("Error:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addStudentAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click_2(object sender, EventArgs e)
        {
            LoginPage lp = new LoginPage();
            this.Hide();
            lp.Show();
        }

        private void accountToolStripMenuItem_Click_3(object sender, EventArgs e)
        {
            AddStudentAccount ad = new AddStudentAccount();
            this.Hide();
            ad.Show();
        }

        private void coursesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            HomePage hp = new HomePage();
            this.Hide();
            hp.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            LoginPage hp = new LoginPage();
            this.Hide();
            hp.Show();
        }
        
    }
}
