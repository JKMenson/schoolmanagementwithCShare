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
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
            PopulateDepart();
        }

        string connectionString = "Data Source=DESKTOP-QCVV1U7;Initial Catalog=studentMgtSystem;Integrated Security=True";

        private void facultyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FacultyForm facultyform = new FacultyForm();
            this.Hide();
            facultyform.Show();
        }

        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DepartmentForm departform = new DepartmentForm();
            this.Hide();
            departform.Show();
        }

        private void studentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            StudentForm studentform = new StudentForm();
            this.Hide();
            studentform.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StudentForm studentform = new StudentForm();
            studentform.Show();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            LecturerForm lecform = new LecturerForm();
            this.Hide();
            lecform.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FacultyForm facultyform = new FacultyForm();
            facultyform.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DepartmentForm dtf = new DepartmentForm();
            this.Hide();
            dtf.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdminForm af = new AdminForm();
            this.Hide();
            af.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AddStudentAccount addstudent = new AddStudentAccount();
            addstudent.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Courses courseform = new Courses();
            this.Hide();
            courseform.Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PopulateDepart()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string selectQuery = "SELECT dept_id FROM department";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while(reader.Read())
                        {
                            cbxDepart.Items.Add(reader["dept_id"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnViewbydept_Click(object sender, EventArgs e)
        {
            try
            {

                if (cbxDepart.SelectedItem == null || cbxLevel.SelectedItem == null)
                {
                    MessageBox.Show("Select department ID and level from the dropdown menu", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string selectQuery = "SELECT * FROM student WHERE dept_id = @dept_id AND level = @level";
                    string selectQuery2 = "SELECT COUNT(*) AS quantity FROM student WHERE dept_id = @dept_id AND level = @level";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@dept_id", cbxDepart.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@level", cbxLevel.SelectedItem.ToString());
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        txtTable.DataSource = dt;
                    }

                    using (SqlCommand cmd = new SqlCommand(selectQuery2, con))
                    {
                        cmd.Parameters.AddWithValue("@dept_id", cbxDepart.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@level", cbxLevel.SelectedItem.ToString());
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            if (cbxDepart.SelectedItem.ToString() == "ITN100" && cbxLevel.SelectedItem.ToString() == "level I")
                            {
                                txtNet.Text = reader["quantity"].ToString();
                            }
                            else if (cbxDepart.SelectedItem.ToString() == "ITN100" && cbxLevel.SelectedItem.ToString() == "level II")
                            {
                                txtNet.Text = reader["quantity"].ToString();
                            }

                            else if (cbxDepart.SelectedItem.ToString() == "ITN100" && cbxLevel.SelectedItem.ToString() == "level III")
                            {
                                txtNet.Text = reader["quantity"].ToString();
                            }

                            else if (cbxDepart.SelectedItem.ToString() == "SEN200" && cbxLevel.SelectedItem.ToString() == "level I")
                            {
                                txtSENG.Text = reader["quantity"].ToString();
                            }

                            else if (cbxDepart.SelectedItem.ToString() == "SEN200" && cbxLevel.SelectedItem.ToString() == "level II")
                            {
                                txtSENG.Text = reader["quantity"].ToString();
                            }

                            else if (cbxDepart.SelectedItem.ToString() == "SEN200" && cbxLevel.SelectedItem.ToString() == "level III")
                            {
                                txtSENG.Text = reader["quantity"].ToString();
                            }

                            else if (cbxDepart.SelectedItem.ToString() == "ISM400" && cbxLevel.SelectedItem.ToString() == "level I")
                            {
                                txtISM.Text = reader["quantity"].ToString();

                            }
                            else if (cbxDepart.SelectedItem.ToString() == "ISM400" && cbxLevel.SelectedItem.ToString() == "level II")
                            {
                                txtISM.Text = reader["quantity"].ToString();

                            }

                            else if (cbxDepart.SelectedItem.ToString() == "ISM400" && cbxLevel.SelectedItem.ToString() == "level III")
                            {
                                txtISM.Text = reader["quantity"].ToString();

                            }

                            else if (cbxDepart.SelectedItem.ToString() == "MUL300" && cbxLevel.SelectedItem.ToString() == "level I")
                            {
                                txtMULT.Text = reader["quantity"].ToString();
                            }

                            else if (cbxDepart.SelectedItem.ToString() == "MUL300" && cbxLevel.SelectedItem.ToString() == "level II")
                            {
                                txtMULT.Text = reader["quantity"].ToString();
                            }
                            else if (cbxDepart.SelectedItem.ToString() == "MUL300" && cbxLevel.SelectedItem.ToString() == "level III")
                            {
                                txtMULT.Text = reader["quantity"].ToString();
                            }
                            else
                            {
                                
                                MessageBox.Show("Not such matching record!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                    }  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbxYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
} 

