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
    public partial class homePage : Form
    {

        public homePage()
        {
            InitializeComponent();

            MySqlConnection conexao = new MySqlConnection();
            conexao.ConnectionString = "SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ; ";
            conexao.Open(); 
            
            label1.Text = $"Bem-vindo(a), {sessaoUsuario.nome}!";

        }

        private void Form_Load(object sender, EventArgs e)
        {

        }


        private void ClienteHomePage_Load(object sender, EventArgs e)
        {
           
        }


        private void FormShow()
        {

        }

        private void homePage_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(sessaoUsuario.foto) && File.Exists(sessaoUsuario.foto))
            {
                // Se a foto existe, carregar na PictureBox
                pictureBox1.Image = Image.FromFile(sessaoUsuario.foto);
                pictureBox1.BorderStyle = BorderStyle.None;  // Remover borda
            }
            else
            {
                // Caso contrário, deixar a PictureBox vazia e adicionar uma borda
                pictureBox1.Image = null;  // A PictureBox ficará sem imagem
                pictureBox1.BorderStyle = BorderStyle.Fixed3D;  // Adiciona uma borda
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            this.panel1.BackColor = Color.FromArgb(134, 8, 26);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            perfilUsuario perfUsu = new perfilUsuario();
            perfUsu.Show();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nomePrato = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(nomePrato))
            {
                MessageBox.Show("Por favor, insira o nome do prato.");
                return;
            }
            MySqlConnection conexao = new MySqlConnection();
            conexao.ConnectionString = "SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ; ";
          
            {
                try
                {
                    conexao.Open();

                    string query = @"SELECT prato.*, restaurante.nome AS NomeRestaurante
                                    FROM prato 
                                 JOIN restaurante  ON prato.fk_restaurante_id = restaurante.id
                                   WHERE prato.nome = @NomePrato;";

                    using (MySqlCommand command = new MySqlCommand(query, conexao))
                    {
                        command.Parameters.AddWithValue("@NomePrato", nomePrato);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Extrair todos os campos
                                string nomePratoResultado = reader["nome"].ToString();
                                string preco = $"R$ {Convert.ToDecimal(reader["preco"]).ToString("F2")}";
                                string descricao = reader["descricao"].ToString();
                                string mediaNota = reader["media_nota"].ToString();
                                string nomeRestaurante = reader["NomeRestaurante"].ToString();

                                // Carregar imagem
                                Image foto = null;
                                if (reader["foto"] != DBNull.Value)
                                {
                                    string caminhoFoto = reader["foto"].ToString(); // Obtenha o caminho da imagem
                                    if (File.Exists(caminhoFoto)) // Verifique se o arquivo existe
                                    {
                                        foto = Image.FromFile(caminhoFoto); // Carregue a imagem do arquivo
                                    }
                                    else
                                    {
                                        MessageBox.Show("Arquivo de imagem não encontrado: " + caminhoFoto);
                                    }

                                    // Abrir o próximo formulário com os dados
                                    comentarioComida comUsu = new comentarioComida(
                           nomePratoResultado,
                            preco,
                            descricao,
                            mediaNota,
                            nomeRestaurante,
                            foto
                        );
                                    comUsu.Show();
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Prato não encontrado.");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao buscar dados: {ex.Message}");
                }
            }
        }

            private void panel2_Paint(object sender, PaintEventArgs e)
        {
            this.panel2.BackColor = Color.FromArgb(134, 8, 26);
        }
    }
}
