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
using System.IO;

namespace pratocerto
{
    public partial class alterarComida : Form
    {
        private int idPrato;
        private string nome;
        private string descricao;
        private string preco;
        private Image foto;

        public alterarComida(int id, string nomePrato, string descricaoPrato, string precoPrato, Image fotoPrato)
        {
            InitializeComponent();
            this.idPrato = id;
            this.nome = nomePrato;
            this.descricao = descricaoPrato;
            this.preco = precoPrato;
            this.foto = fotoPrato;

            // Preenche os controles com os dados recebidos
            textBox2.Text = nomePrato;
            textBox3.Text = descricaoPrato;
            textBox4.Text = precoPrato;
            pictureBox3.Image = fotoPrato;

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            this.panel3.BackColor = Color.FromArgb(89, 6, 18);
        }

        private void label4_Click(object sender, EventArgs e)
        {

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

        private void button7_Click(object sender, EventArgs e)
        {
            string nomeAtualizado = textBox2.Text;
            string descricaoAtualizada = textBox3.Text;
            string precoAtualizado = textBox4.Text;
            Image fotoAtualizada = pictureBox3.Image;

            // Atualiza o prato no banco de dados
            AtualizarPrato(idPrato, nomeAtualizado, descricaoAtualizada, precoAtualizado, fotoAtualizada);
            MessageBox.Show("Prato atualizado com sucesso!");
            homePage home = new homePage();
            home.Show();
            this.Close(); // Fecha o formulário de alteração
        }
        private void AtualizarPrato(int idPrato, string nome, string descricao, string preco, Image foto)
        {
            // Código para atualizar as informações do prato no banco de dados
            try
            {
                MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ; ");
                conexao.Open();

                string query = "UPDATE prato SET nome = @nome, descricao = @descricao, preco = @preco, foto = @foto WHERE id = @idPrato";

                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@nome", nome);
                    comando.Parameters.AddWithValue("@descricao", descricao);
                    comando.Parameters.AddWithValue("@preco", preco);

                    // Converte a imagem para o formato adequado para o banco de dados
                    if (foto != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        foto.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        comando.Parameters.AddWithValue("@foto", ms.ToArray());
                    }
                    else
                    {
                        comando.Parameters.AddWithValue("@foto", DBNull.Value);
                    }

                    comando.Parameters.AddWithValue("@idPrato", idPrato);
                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar prato: {ex.Message}");
            }
        }
    }
}
