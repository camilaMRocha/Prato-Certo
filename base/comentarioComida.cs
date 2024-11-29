using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace pratocerto
{
    public partial class comentarioComida : Form
    {
        public comentarioComida(
            string nomePrato,
            string preco,
            string descricao,
            string mediaNota,
            string nomeRestaurante,
            Image foto)
        {
            InitializeComponent();
            MySqlConnection conexao = new MySqlConnection();
            conexao.ConnectionString = "SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ; ";
            conexao.Open();

            label1.Text = $"{sessaoUsuario.nome}!";
            label7.Text = nomePrato;
            label3.Text = preco;
            label4.Text = descricao;
            label5.Text = mediaNota;
            label6.Text = nomeRestaurante;

            if (foto != null)
            {
                pictureBox3.Image = foto;
            }
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
            this.panel8.BackColor = Color.FromArgb(178, 48, 67);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            this.panel3.BackColor = Color.FromArgb(178, 48, 67);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            this.panel2.BackColor = Color.FromArgb(134, 8, 26);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            this.panel1.BackColor = Color.FromArgb(134, 8, 26);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void nomeComida_Click(object sender, EventArgs e)
        {

        }

        private void preco_Click(object sender, EventArgs e)
        {

        }

        private void restaurante_Click(object sender, EventArgs e)
        {

        }

        private void local_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void descricao_Click(object sender, EventArgs e)
        {

        }
    }
}
