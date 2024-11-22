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
    public partial class perfilUsuario : Form
    {
        public perfilUsuario()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            this.panel2.BackColor = Color.FromArgb(134, 8, 26);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            this.panel1.BackColor = Color.FromArgb(134, 8, 26);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            homePage home = new homePage();
            home.Show();
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {
            this.panel2.BackColor = Color.FromArgb(134, 8, 26);
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            this.panel6.BackColor = Color.FromArgb(134, 8, 26);
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            this.panel5.BackColor = Color.FromArgb(178, 48, 67);
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            this.panel7.BackColor = Color.FromArgb(178, 48, 67);
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
            this.panel8.BackColor = Color.FromArgb(178, 48, 67);
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {
            this.panel9.BackColor = Color.FromArgb(178, 48, 67);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            this.panel3.BackColor = Color.FromArgb(178, 48, 67);
        }


        private void button8_Click_1(object sender, EventArgs e)
        {
            visualizarComentários visuCom = new visualizarComentários();
            visuCom.Show();
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            alterarComida altComi = new alterarComida();
            altComi.Show();
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            excluirComida excComi = new excluirComida();
            excComi.Show();
            this.Close();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            homePage home = new homePage();
            home.Show();
            this.Close();
        }
    }
}
