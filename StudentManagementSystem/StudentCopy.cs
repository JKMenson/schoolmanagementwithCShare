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
    public partial class StudentCopy : Form
    {
        public StudentCopy()
        {
            InitializeComponent();
        }

        string connectionString = "Data Source=DESKTOP-QCVV1U7;Initial Catalog=studentMgtSystem;Integrated Security=True";

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void Search_Click(object sender, EventArgs e)
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


        private void btnView_Click(object sender, EventArgs e)
        {
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
        }

        private void StudentCopy_Load(object sender, EventArgs e)
        {

        }

        private void marksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Marks mk = new Marks();
            this.Hide();
            mk.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void coursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void enrollmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Enrollment enl = new Enrollment();
            this.Hide();
            enl.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginPage lp = new LoginPage();
            this.Hide();
            lp.Show();
        }
    }
}
