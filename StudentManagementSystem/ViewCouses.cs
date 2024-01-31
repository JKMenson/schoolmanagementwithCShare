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
    public partial class ViewCouses : Form
    {
        public ViewCouses()
        {
            InitializeComponent();
        }

        string connectionString = "Data Source=DESKTOP-QCVV1U7;Initial Catalog=studentMgtSystem;Integrated Security=True";

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TeacherDashboard td = new TeacherDashboard();
            this.Hide();
            td.Show();
        }

        private void button2_Click(object sender, EventArgs e)
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

                        txtTable.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurs: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void coursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TeacherDashboard td = new TeacherDashboard();
            this.Hide();
            td.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoginPage lp = new LoginPage();
            this.Close();
            lp.Show();
        }
    }
}
