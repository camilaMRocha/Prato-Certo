﻿using System;
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
    public partial class perfilUsuario : Form
    {
        public perfilUsuario()
        {
            InitializeComponent();

            MySqlConnection conexao = new MySqlConnection();
            conexao.ConnectionString = "SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ; ";
            conexao.Open();

            label1.Text = $"{sessaoUsuario.nome}";
            label2.Text = $"{sessaoUsuario.nome}";

            label1.Text = $"{sessaoUsuario.nome}";
            textBox2.Text = sessaoUsuario.nome;
            textBox3.Text = sessaoUsuario.email;
            textBox4.Text = sessaoUsuario.senha;
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
          
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            //this.panel5.BackColor = Color.FromArgb(178, 48, 67);
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {
           
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

        private void perfilUsuario_Load(object sender, EventArgs e)
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
                string query = "UPDATE cliente SET foto = @foto WHERE id = @id";

                MySqlCommand comando = new MySqlCommand(query, conexao);
                comando.Parameters.AddWithValue("@foto", caminhoFoto);
                comando.Parameters.AddWithValue("@id", sessaoUsuario.id);

                try
                {
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Foto alterada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao atualizar a foto: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string novoNome = textBox2.Text;

            if (string.IsNullOrWhiteSpace(novoNome))
            {
                MessageBox.Show("Por favor, insirá um novo nome válido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ; "))
            {
                string atualizar = "UPDATE cliente SET nome = @novoNome WHERE id = @idUsuario";

                MySqlCommand comando = new MySqlCommand(atualizar, conexao);
                comando.Parameters.AddWithValue("@novoNome", novoNome);
                comando.Parameters.AddWithValue("@idUsuario", sessaoUsuario.id);

                try
                {
                    conexao.Open();
                    int linhasAfetadas = comando.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                    {
                        sessaoUsuario.nome = novoNome;
                        MessageBox.Show("Nome foi alterado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        label1.Text = $"{sessaoUsuario.nome}";
                        label2.Text = $"{sessaoUsuario.nome}";
                    }
                    else
                    {
                        MessageBox.Show("Nenhuma alteração foi realizada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Errp ao alterar o nome.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string novoEmail = textBox3.Text;

            if (string.IsNullOrWhiteSpace(novoEmail))
            {
                MessageBox.Show("Por favor, insirá um novo e-mail válido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ; "))
            {
                string atualizar = "UPDATE cliente SET email = @novoEmail WHERE id = @idUsuario";

                MySqlCommand comando = new MySqlCommand(atualizar, conexao);
                comando.Parameters.AddWithValue("@novoEmail", novoEmail);
                comando.Parameters.AddWithValue("@idUsuario", sessaoUsuario.id);

                try
                {
                    conexao.Open();
                    int linhasAfetadas = comando.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                    {
                        sessaoUsuario.email = novoEmail;
                        MessageBox.Show("E-mail foi alterado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Nenhuma alteração foi realizada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Errp ao alterar o e-mail.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string novoSenha = textBox4.Text;

            if (string.IsNullOrWhiteSpace(novoSenha))
            {
                MessageBox.Show("Por favor, insirá um novo senha válido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ; "))
            {
                string atualizar = "UPDATE cliente SET senha = @novoSenha WHERE id = @idUsuario";

                MySqlCommand comando = new MySqlCommand(atualizar, conexao);
                comando.Parameters.AddWithValue("@novoSenha", novoSenha);
                comando.Parameters.AddWithValue("@idUsuario", sessaoUsuario.id);

                try
                {
                    conexao.Open();
                    int linhasAfetadas = comando.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                    {
                        sessaoUsuario.senha = novoSenha;
                        MessageBox.Show("Senha foi alterado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Nenhuma alteração foi realizada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Errp ao alterar a senha.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Tem certeza de que deseja deletar este cliente?",
                                        "Confirmação",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD=;"))
                {
                    try
                    {
                        conexao.Open();

                        // Comando SQL para deletar cliente com base no ID
                        string queryDeletar = "DELETE FROM cliente WHERE id = @IdCliente";

                        using (MySqlCommand comando = new MySqlCommand(queryDeletar, conexao))
                        {
                            // Substitua pelo ID do cliente que deseja deletar
                            comando.Parameters.AddWithValue("@IdCliente", sessaoUsuario.id);

                            int linhasAfetadas = comando.ExecuteNonQuery();

                            if (linhasAfetadas > 0)
                            {
                                MessageBox.Show("Cliente deletado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Opcional: Redirecionar ou atualizar a interface
                                this.Close(); // Fecha o formulário atual
                            }
                            else
                            {
                                MessageBox.Show("Nenhum cliente foi encontrado para deletar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao tentar deletar cliente: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
