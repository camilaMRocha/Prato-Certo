using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pratocerto
{
    public partial class homePageEmpresa : Form
    {
        public homePageEmpresa()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            this.panel1.BackColor = Color.FromArgb(134, 8, 26);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            this.panel2.BackColor = Color.FromArgb(134, 8, 26);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            perfilEmpresa perEmp = new perfilEmpresa();
            perEmp.Show();
            this.Close();
        }
    }
}
