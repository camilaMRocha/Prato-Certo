﻿using System;
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
    public partial class excluirComida : Form
    {
        public excluirComida()
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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            this.panel3.BackColor = Color.FromArgb(89, 6, 18);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            homePage home = new homePage();
            home.Show();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
           
            perfilUsuario perUsu = new perfilUsuario();
            perUsu.Show();
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            perfilUsuario perUsu = new perfilUsuario();
            perUsu.Show();
            this.Close();
        }
    }
}
