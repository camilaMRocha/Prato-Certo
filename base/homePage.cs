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
        private int pageNumber = 0;
        private const int pageSize = 10; // Quantidade de pratos por vez
        public homePage()
        {
            InitializeComponent();

            MySqlConnection conexao = new MySqlConnection();
            conexao.ConnectionString = "SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ; ";
            conexao.Open(); 
            
            label1.Text = $"Bem-vindo(a), {sessaoUsuario.nome}!";
            flowLayoutPanel1.AutoScroll = true; // Habilita o AutoScroll

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

            flowLayoutPanel1.Scroll += flowLayoutPanelResultados_Scroll; // Associa o evento de rolagem
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
            // Limpa os resultados anteriores
            flowLayoutPanel1.Controls.Clear();
            pageNumber = 0; // Reseta a página para o início

            // Chama o método para carregar os pratos
            CarregarPratos();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            this.panel2.BackColor = Color.FromArgb(134, 8, 26);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Limpa os resultados anteriores
            flowLayoutPanel1.Controls.Clear();
            pageNumber = 0; // Reseta a página para o início

            // Chama o método para carregar os pratos novamente
            CarregarPratos();
        }

        private void CarregarPratos()
        {
            string pesquisa = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(pesquisa))
            {
                MessageBox.Show("Por favor, insira um nome para pesquisar.");
                return;
            }

            // Conexão com o banco de dados
            using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ;"))
            {
                try
                {
                    conexao.Open();

                    // Consulta SQL com paginação (LIMIT e OFFSET)
                    string query = "SELECT id, nome, descricao, preco, media_nota, foto FROM prato WHERE nome LIKE @Pesquisa LIMIT @Limit OFFSET @Offset";
                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@Pesquisa", "%" + pesquisa + "%");
                        comando.Parameters.AddWithValue("@Limit", pageSize);
                        comando.Parameters.AddWithValue("@Offset", pageNumber * pageSize);

                        using (MySqlDataReader leitor = comando.ExecuteReader())
                        {
                            while (leitor.Read())
                            {
                                Panel painel3 = new Panel
                                {
                                    Width = 200,
                                    Height = 250,
                                    Margin = new Padding(10)
                                };

                                // Nome do prato
                                Label nomeLabel = new Label
                                {
                                    Text = leitor.GetString("nome"),
                                    Width = painel3.Width,
                                    TextAlign = ContentAlignment.MiddleCenter
                                };
                                painel3.Controls.Add(nomeLabel);

                                Label precoLabel = new Label
                                {
                                    Text = leitor.GetString("preco"),
                                    Width = painel3.Width,
                                    TextAlign = ContentAlignment.MiddleCenter
                                };
                                painel3.Controls.Add(nomeLabel);

                                Label descricaoLabel = new Label
                                {
                                    Text = leitor.GetString("descricao"),
                                    Width = painel3.Width,
                                    TextAlign = ContentAlignment.MiddleCenter
                                };
                                painel3.Controls.Add(nomeLabel);

                                // Imagem do prato
                                PictureBox fotoPictureBox = new PictureBox
                                {
                                    ImageLocation = leitor.IsDBNull(leitor.GetOrdinal("foto")) ? "caminho/padrao.jpg" : leitor.GetString("foto"),
                                    SizeMode = PictureBoxSizeMode.StretchImage,
                                    Width = painel3.Width,
                                    Height = 150
                                };
                                painel3.Controls.Add(fotoPictureBox);

                                // Adiciona o painel ao FlowLayoutPanel
                                flowLayoutPanel1.Controls.Add(painel3);
                            }
                        }
                    }

                    // Atualiza o número da página para a próxima
                    pageNumber++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao buscar os resultados: {ex.Message}");
                }



            }

        }
        // Evento de rolagem do painel para carregar mais itens ao rolar até o final
        private void flowLayoutPanelResultados_Scroll(object sender, ScrollEventArgs e)
        {
            if (flowLayoutPanel1.VerticalScroll.Value == flowLayoutPanel1.VerticalScroll.Maximum)
            {
                CarregarPratos(); // Carrega mais pratos
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
           
        }
    }
}
