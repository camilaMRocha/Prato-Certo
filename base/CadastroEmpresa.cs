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
    public partial class CadastroEmpresa : Form
    {
        public CadastroEmpresa()
        {
            InitializeComponent();
        }

        private void CadastroEmpresa_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(134, 8, 26);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login log = new login();
            log.Show();
            this.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            login log = new login();
            log.Show();
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            CadastroUsuario cadUsu = new CadastroUsuario();
            cadUsu.Show();
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            login log = new login();
            log.Show();
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CadastroUsuario cadUsu = new CadastroUsuario();
            cadUsu.Show();
            this.Close();
        }
    }
}
