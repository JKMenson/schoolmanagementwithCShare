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
    public partial class TeacherDashboard : Form
    {
        public TeacherDashboard()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Enrollment en = new Enrollment();
            this.Hide();
            en.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Marks mk = new Marks();
            this.Hide();
            mk.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewCouses ac = new ViewCouses();
            this.Hide();
            ac.Show();
        }
    }
}
