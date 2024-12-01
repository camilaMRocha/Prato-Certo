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

        // Método assíncrono renomeado
        private async Task AtualizarPratoAsyncComImagem(int idPrato, string nome, string descricao, string preco, Image foto)
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ;"))
                {
                    await conexao.OpenAsync();

                    string query = "UPDATE prato SET nome = @nome, descricao = @descricao, preco = @preco, foto = @foto WHERE id = @idPrato";

                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@nome", nome);
                        comando.Parameters.AddWithValue("@descricao", descricao);
                        comando.Parameters.AddWithValue("@preco", preco);

                        if (foto != null)
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                foto.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                comando.Parameters.AddWithValue("@foto", ms.ToArray());
                            }
                        }
                        else
                        {
                            comando.Parameters.AddWithValue("@foto", DBNull.Value);
                        }

                        comando.Parameters.AddWithValue("@idPrato", idPrato);

                        await comando.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar prato: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private async void button7_Click(object sender, EventArgs e)
        {
            string nomeAtualizado = textBox2.Text;
            string descricaoAtualizada = textBox3.Text;
            string precoAtualizado = textBox4.Text;
            Image fotoAtualizada = pictureBox3.Image;

            // Validação básica para garantir que os campos não estão vazios
            if (string.IsNullOrEmpty(nomeAtualizado) || string.IsNullOrEmpty(descricaoAtualizada) || string.IsNullOrEmpty(precoAtualizado))
            {
                MessageBox.Show("Por favor, preencha todos os campos antes de atualizar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Chama o método assíncrono renomeado
            await AtualizarPratoAsyncComImagem(idPrato, nomeAtualizado, descricaoAtualizada, precoAtualizado, fotoAtualizada);

            MessageBox.Show("Prato atualizado com sucesso!");

            // Redireciona para a página inicial
            homePage home = new homePage();
            home.Show();

            // Fecha o formulário de alteração
            this.Close();
        }

        private async Task AtualizarPratoAsync(int idPrato, string nome, string descricao, string preco, Image foto)
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ;"))
                {
                    await conexao.OpenAsync();

                    string query = "UPDATE prato SET nome = @nome, descricao = @descricao, preco = @preco, foto = @foto WHERE id = @idPrato";

                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@nome", nome);
                        comando.Parameters.AddWithValue("@descricao", descricao);
                        comando.Parameters.AddWithValue("@preco", preco);

                        if (foto != null)
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                foto.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                comando.Parameters.AddWithValue("@foto", ms.ToArray());
                            }
                        }
                        else
                        {
                            comando.Parameters.AddWithValue("@foto", DBNull.Value);
                        }

                        comando.Parameters.AddWithValue("@idPrato", idPrato);

                        await comando.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar prato: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
          

    }
}