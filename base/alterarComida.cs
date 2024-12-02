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
        private string nomePrato;
        private string descricaoPrato;
        private string precoPrato;
        public alterarComida(int idPrato, string nomePrato, string descricaoPrato, string precoPrato)
        {
            InitializeComponent();
            this.idPrato = idPrato;
            this.nomePrato = nomePrato;
            this.descricaoPrato = descricaoPrato;
            this.precoPrato = precoPrato;

            // Preencher os campos com as informações corretas do prato
            textBox2.Text = nomePrato;       // Preencher com o nome do prato
            textBox3.Text = descricaoPrato;  // Preencher com a descrição do prato
            textBox4.Text = precoPrato;      // Preencher com o preço do prato
        }



        // Método assíncrono renomeado
        private async Task AtualizarPratoAsyncComImagem()
        {
            
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
            perfilEmpresa perUsu = new perfilEmpresa();
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

            try
            {
                // Atualiza as informações do prato no banco de dados
                using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ;"))
                {
                    string query = "UPDATE prato SET nome = @nome, descricao = @descricao, preco = @preco WHERE id = @idPrato";

                    MySqlCommand comando = new MySqlCommand(query, conexao);
                    comando.Parameters.AddWithValue("@nome", nomeAtualizado);
                    comando.Parameters.AddWithValue("@descricao", descricaoAtualizada);
                    comando.Parameters.AddWithValue("@preco", precoAtualizado);
                    comando.Parameters.AddWithValue("@idPrato", idPrato); // Usando o ID do prato selecionado

                    conexao.Open();
                    int rowsAffected = comando.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Prato atualizado com sucesso!");
                        this.Close(); // Fecha o formulário após a atualização
                    }
                    else
                    {
                        MessageBox.Show("Erro ao atualizar o prato.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        private async Task AtualizarPratoAsync()
        {
            
        }
    
          

    }
}