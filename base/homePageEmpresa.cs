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
    public partial class homePageEmpresa : Form
    {

        private MySqlConnection conexao; // Conexão aberta e reutilizada
        private const string connectionString = "SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ;";

        public homePageEmpresa()
        {
            InitializeComponent();

            conexao = new MySqlConnection(connectionString);
            conexao.Open();

            label1.Text = $"Bem-vindo(a), {sessaoUsuario.nome}!";
            flowLayoutPanelRanking.AutoScroll = true;

        }

       


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            this.panel1.BackColor = Color.FromArgb(134, 8, 26);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            this.panel2.BackColor = Color.FromArgb(134, 8, 26);


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            perfilEmpresa perEmp = new perfilEmpresa();
            perEmp.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            perfilEmpresa perEmp = new perfilEmpresa();
            perEmp.Show();
            this.Close();
        }

        private void homePageEmpresa_Load(object sender, EventArgs e)
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

            CarregarRanking();

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            this.panel3.BackColor = Color.FromArgb(178, 48, 67);
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AdicionarPainel(MySqlDataReader leitor)
        {
            Panel painel3 = new Panel
            {
                Width = 320,
                Height = 300,
                Margin = new Padding(30), // Espaço entre painéis
                BorderStyle = BorderStyle.FixedSingle
            };

            int topOffset = 10; // Controla a posição vertical inicial

            // Nome do prato
            Label label3 = new Label
            {
                Text = $"Prato: {leitor.GetString("prato_nome")}",
                AutoSize = true,
                Font = new Font("Microsoft YaHei UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, topOffset) // Posição inicial
            };
            painel3.Controls.Add(label3);
            topOffset += label3.Height + 10; // Atualiza a posição vertical

            // Nome do restaurante
            Label label4 = new Label
            {
                Text = $"Restaurante: {leitor.GetString("restaurante_nome")}",
                AutoSize = true,
                Font = new Font("Microsoft YaHei UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, topOffset)
            };
            painel3.Controls.Add(label4);
            topOffset += label4.Height + 10;

            // Nota
            Label label5 = new Label
            {
                Text = $"Nota: {leitor.GetDecimal("media_nota")}/5",
                AutoSize = true,
                Font = new Font("Microsoft YaHei UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, topOffset)
            };
            painel3.Controls.Add(label5);
            topOffset += label5.Height + 10;

            // Preço
            Label label6 = new Label
            {
                Text = $"Preço: R$ {leitor.GetDecimal("preco"):F2}",
                AutoSize = true,
                Font = new Font("Microsoft YaHei UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, topOffset)
            };
            painel3.Controls.Add(label6);
            topOffset += label6.Height + 10;

            // Descrição
            Label label7 = new Label
            {
                Text = $"Descrição: {leitor.GetString("descricao")}",
                AutoSize = true,
                MaximumSize = new Size(280, 60), // Limita a largura e a altura do texto
                Font = new Font("Microsoft YaHei UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(10, topOffset)
            };
            painel3.Controls.Add(label7);
            topOffset += label7.Height + 20; // Espaço maior após a descrição
            PictureBox pictureBoxPrato = new PictureBox
            {
                ImageLocation = leitor.IsDBNull(leitor.GetOrdinal("foto")) ? "caminho/padrao.jpg" : leitor.GetString("foto"),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = 120,
                Height = 120,
                Location = new Point(10, topOffset)
            }; painel3.Controls.Add(pictureBoxPrato);



            // Adiciona o painel ao FlowLayoutPanel
            flowLayoutPanelRanking.Controls.Add(painel3);

        }
        private void CarregarRanking()
        {
            try
            {
                string query = @"
                    SELECT p.nome AS prato_nome, p.media_nota, p.preco, p.descricao, p.foto, r.nome AS restaurante_nome
                    FROM prato p
                    INNER JOIN restaurante r ON p.fk_restaurante_id = r.id
                    ORDER BY p.media_nota DESC
                    LIMIT 10;";

                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    using (MySqlDataReader leitor = comando.ExecuteReader())
                    {
                        flowLayoutPanelRanking.Controls.Clear(); // Limpa o conteúdo anterior

                        while (leitor.Read())
                        {
                            AdicionarPainel(leitor); // Adiciona cada prato ao FlowLayoutPanel
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar o ranking: {ex.Message}");
            }

        }

        private void homePageEmpresa_FormClosing(object sender, FormClosingEventArgs e)
        {
            conexao.Close();
        }
    }
}
