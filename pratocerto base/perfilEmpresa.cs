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
    public partial class perfilEmpresa : Form
    {
        public perfilEmpresa()
        {
            InitializeComponent();
        }

        private void perfilEmpresa_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            this.panel1.BackColor = Color.FromArgb(134, 8, 26);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            this.panel2.BackColor = Color.FromArgb(134, 8, 26);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            homePage home = new homePage();
            home.Show();
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            this.panel3.BackColor = Color.FromArgb(178, 48, 67);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            this.panel6.BackColor = Color.FromArgb(178, 48, 67);
        }

        private void panel5_Paint_1(object sender, PaintEventArgs e)
        {
            
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            comentarComida visuCom = new comentarComida();
            visuCom.Show();
            this.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            comentarComida visuCom = new comentarComida();
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

        private void button17_Click(object sender, EventArgs e)
        {
            adicionarComida adiCom = new adicionarComida();
            adiCom.Show();
            this.Close();
        }
    }
}
