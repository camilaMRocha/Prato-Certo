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
    public partial class comentarioComida : Form
    {
        private int pratoId;
        public comentarioComida(

            string nomePrato,
            string preco,
            string descricao,
            string mediaNota,
            string nomeRestaurante,
            Image foto,
            int pratoId)
        {
            InitializeComponent();
            this.pratoId = pratoId;
            MySqlConnection conexao = new MySqlConnection();
            conexao.ConnectionString = "SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ; ";
            conexao.Open();

            label1.Text = $"{sessaoUsuario.nome}";
            label2.Text = $"{sessaoUsuario.nome}";
            label7.Text = nomePrato;
            label3.Text = preco;
            label4.Text = descricao;
            label5.Text = mediaNota;
            label6.Text = nomeRestaurante;

            if (foto != null)
            {
                pictureBox3.Image = foto;
            }

            CarregarComentarios();
            // Preencher a ComboBox com as notas de 1 a 5
            comboBox1.Items.Clear(); // Limpa os itens (se necessário)
            for (int i = 1; i <= 5; i++)
            {
                comboBox1.Items.Add(i.ToString());
            }

            // Opcional: Deixar nenhum item selecionado inicialmente

            comboBox1.SelectedIndex = -1;
            ToolStripMenuItem facebookItem = new ToolStripMenuItem("Facebook");
            ToolStripMenuItem twitterItem = new ToolStripMenuItem("Twitter");
            ToolStripMenuItem whatsappItem = new ToolStripMenuItem("WhatsApp");
            ToolStripMenuItem instagramItem = new ToolStripMenuItem("Instagram");

            // Adicionar eventos de clique para cada item
            facebookItem.Click += new EventHandler(facebookToolStripMenuItem_Click);
            twitterItem.Click += new EventHandler(twitterToolStripMenuItem_Click);
            whatsappItem.Click += new EventHandler(whatsappToolStripMenuItem_Click);
            instagramItem.Click += new EventHandler(instagramToolStripMenuItem_Click);

            // Adicionar os itens ao ContextMenuStrip
            contextMenuStrip1.Items.Add(facebookItem);
            contextMenuStrip1.Items.Add(twitterItem);
            contextMenuStrip1.Items.Add(whatsappItem);
            contextMenuStrip1.Items.Add(instagramItem);

            // Associar o ContextMenuStrip a um controle (por exemplo, um Button)
            button3.ContextMenuStrip = contextMenuStrip1;
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
        private void CarregarComentarios()
        {

            string connectionString = "SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ;";

            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();

                    // Query para buscar os comentários
                    string query = @"SELECT 
                                avaliacao.data AS Data,
                                avaliacao.nota AS Nota,
                                avaliacao.comentario AS Comentario,
                                cliente.nome AS Cliente
                             FROM 
                                avaliacao
                             JOIN 
                                cliente ON avaliacao.fk_cliente_id = cliente.id
                             WHERE 
                                avaliacao.fk_prato_id = @PratoId
                             ORDER BY 
                                avaliacao.data DESC;";

                    using (MySqlCommand command = new MySqlCommand(query, conexao))
                    {
                        command.Parameters.AddWithValue("@PratoId", pratoId);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable tabelaComentarios = new DataTable();
                            adapter.Fill(tabelaComentarios);

                            // Configurar as colunas do DataGridView, caso ainda não existam
                            if (dataGridView1.Columns.Count == 0)
                            {
                                dataGridView1.Columns.Add("Data", "Data");
                                dataGridView1.Columns.Add("Nota", "Nota");
                                dataGridView1.Columns.Add("Comentario", "Comentário");
                                dataGridView1.Columns.Add("Cliente", "Cliente");
                            }

                            // Preencher o DataGridView com os dados
                            dataGridView1.Rows.Clear();
                            foreach (DataRow row in tabelaComentarios.Rows)
                            {
                                dataGridView1.Rows.Add(
                                    row["Data"].ToString(),
                                    row["Nota"].ToString(),
                                    row["Comentario"].ToString(),
                                    row["Cliente"].ToString()
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao carregar comentários: {ex.Message}");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Exibir o menu de compartilhamento
            button3.ContextMenuStrip.Show(button3, new Point(0, button3.Height));
        }
        private void facebookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string mensagem = GetMensagemParaCompartilhar();
            string urlFacebook = $"https://www.facebook.com/sharer/sharer.php?u={Uri.EscapeDataString("https://www.site-do-prato.com")}&quote={Uri.EscapeDataString(mensagem)}";
            System.Diagnostics.Process.Start(urlFacebook);
        }

        private void twitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string mensagem = GetMensagemParaCompartilhar();
            string urlTwitter = $"https://twitter.com/intent/tweet?text={Uri.EscapeDataString(mensagem)}&url={Uri.EscapeDataString("https://www.site-do-prato.com")}";
            System.Diagnostics.Process.Start(urlTwitter);
        }

        private void whatsappToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string mensagem = GetMensagemParaCompartilhar();
            string urlWhatsApp = $"https://api.whatsapp.com/send?text={Uri.EscapeDataString(mensagem)}";
            System.Diagnostics.Process.Start(urlWhatsApp);
        }

        private void instagramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Instagram não tem um link direto para compartilhamento via web, então, normalmente, é feito pelo app
            string mensagem = GetMensagemParaCompartilhar();
            string urlInstagram = $"https://www.instagram.com/?url={Uri.EscapeDataString("https://www.site-do-prato.com")}";
            System.Diagnostics.Process.Start(urlInstagram);
        }

        // Função para montar a mensagem
        private string GetMensagemParaCompartilhar()
        {
            string comentario = textBox2.Text;
            string nota = comboBox1.SelectedItem?.ToString();
            string nomePrato = label7.Text;
            return $"Acabei de avaliar o prato \"{nomePrato}\" com a nota {nota}! Comentário: \"{comentario}\". Confira você também!";
        }
    

    

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Por favor, insira um comentário.");
                return;
            }

            // Validar se a nota foi selecionada
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecione uma nota.");
                return;
            }

            try
            {
                string comentario = textBox2.Text.Trim();
                int nota = int.Parse(comboBox1.SelectedItem.ToString());

                string connectionString = "SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ;";
                using (MySqlConnection conexao = new MySqlConnection(connectionString))
                {
                    conexao.Open();

                    // Query para inserir o comentário e a nota no banco de dados
                    string query = @"INSERT INTO avaliacao (fk_prato_id, fk_cliente_id, comentario, nota, data) 
                             VALUES (@PratoId, @ClienteId, @Comentario, @Nota, NOW())";

                    using (MySqlCommand command = new MySqlCommand(query, conexao))
                    {
                        // Substituir parâmetros
                        command.Parameters.AddWithValue("@PratoId", pratoId);  // O ID do prato atual
                        command.Parameters.AddWithValue("@ClienteId", sessaoUsuario.id); // ID do usuário logado
                        command.Parameters.AddWithValue("@Comentario", comentario);
                        command.Parameters.AddWithValue("@Nota", nota);

                        // Executar o comando
                        int linhasAfetadas = command.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            MessageBox.Show("Comentário enviado com sucesso!");

                            // Atualizar os comentários no DataGridView
                            CarregarComentarios();

                            // Limpar os campos
                            textBox2.Clear();
                            comboBox1.SelectedIndex = -1;
                        }
                        else
                        {
                            MessageBox.Show("Erro ao enviar o comentário. Tente novamente.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao enviar o comentário: {ex.Message}");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel8_Paint_1(object sender, PaintEventArgs e)
        {
            this.panel8.BackColor = Color.FromArgb(178, 48, 67);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void comentarioComida_Load(object sender, EventArgs e)
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            homePage home = new homePage();
            home.Show();
            this.Close();
        }
    }
}
