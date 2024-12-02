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
            label1.Text = $"{sessaoUsuario.nome}";

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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.jfif;";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string novoCaminhoFoto = openFileDialog.FileName;

                    // Copiar a imagem para a pasta local do aplicativo
                    string diretorioFotos = Path.Combine(Application.StartupPath, "Fotos");
                    if (!Directory.Exists(diretorioFotos))
                    {
                        Directory.CreateDirectory(diretorioFotos);
                    }

                    // Gerar um novo nome para a imagem
                    string nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(novoCaminhoFoto);
                    string caminhoDestino = Path.Combine(diretorioFotos, nomeArquivo);

                    File.Copy(novoCaminhoFoto, caminhoDestino, true);

                    // Atualizar a PictureBox com a nova imagem
                    pictureBox1.Image = Image.FromFile(caminhoDestino);
                    pictureBox3.Image = Image.FromFile(caminhoDestino);

                    // Atualizar a foto na sessão
                    sessaoUsuario.foto = caminhoDestino;

                    // Atualizar o banco de dados
                    AtualizarFotoUsuario(caminhoDestino);
                }
            }
        }

        private void AtualizarFotoUsuario(string caminhoFoto)
        {
            using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ;"))
            {
                string query = "UPDATE prato SET foto = @foto WHERE id = @id";

                MySqlCommand comando = new MySqlCommand(query, conexao);
                comando.Parameters.AddWithValue("@foto", caminhoFoto); // Salvar caminho completo ou relativo
                comando.Parameters.AddWithValue("@id", sessaoUsuario.id);

                try
                {
                    conexao.Open();
                    int rowsAffected = comando.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Foto alterada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Erro ao atualizar foto no banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao atualizar a foto: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void alterarComida_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(sessaoUsuario.foto) && File.Exists(sessaoUsuario.foto))
            {
                // Se a foto existe, carregar na PictureBox
                pictureBox1.Image = Image.FromFile(sessaoUsuario.foto);
                pictureBox1.BorderStyle = BorderStyle.None;  // Remover borda

                pictureBox3.Image = Image.FromFile(sessaoUsuario.foto);
                pictureBox3.BorderStyle = BorderStyle.None;
            }
            else
            {
                // Caso contrário, deixar a PictureBox vazia e adicionar uma borda
                pictureBox1.Image = null;  // A PictureBox ficará sem imagem
                pictureBox1.BorderStyle = BorderStyle.Fixed3D;  // Adiciona uma borda

                pictureBox3.Image = null;
                pictureBox3.BorderStyle = BorderStyle.Fixed3D;
            }
        }
    }
}