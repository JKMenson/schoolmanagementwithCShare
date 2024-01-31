using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginPage_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void facultyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FacultyForm facultyform = new FacultyForm();
            this.Hide();
            facultyform.Show();
            
        }

        private void studentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            StudentForm studentform = new StudentForm();
            this.Hide();
            studentform.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DepartmentForm departform = new DepartmentForm();
            this.Hide();
            departform.Show();
        }

        private void courseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lecturerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminLogin adlogin = new AdminLogin();
            this.Hide();
            adlogin.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StudentLogin stlogin = new StudentLogin();
            this.Hide();
            stlogin.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoginPage_Load_1(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
