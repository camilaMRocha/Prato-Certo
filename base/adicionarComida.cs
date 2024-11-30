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
    public partial class adicionarComida : Form
    {
        public adicionarComida()
        {
            InitializeComponent();
            MySqlConnection conexao = new MySqlConnection();
            conexao.ConnectionString = "SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ; ";
            conexao.Open();

            label1.Text = $"{sessaoUsuario.nome}!";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            this.panel2.BackColor = Color.FromArgb(134, 8, 26);
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            this.panel5.BackColor = Color.FromArgb(89, 6, 18);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            this.panel1.BackColor = Color.FromArgb(134, 8, 26);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Supomos que a sessão do usuário contém o ID do restaurante
            int restauranteId = sessaoUsuario.id;  // O ID do restaurante que está logado

            string nome = textBox2.Text;
            string preco = textBox4.Text;

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Por favor, insira o nome do prato.", "Campo obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Por favor, insira o preço do prato.", "Campo obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string descricao = textBox3.Text;
            string fotoCaminho = null;

            if (pictureBox3.Image != null)
            {
                // Crie o diretório onde as imagens serão salvas
                string diretorioImagens = Path.Combine(Application.StartupPath, "Fotos");
                if (!Directory.Exists(diretorioImagens))
                {
                    Directory.CreateDirectory(diretorioImagens);  // Cria o diretório caso não exista
                }

                // Gerar um nome único para o arquivo (por exemplo, usando GUID)
                string nomeImagem = Guid.NewGuid().ToString() + ".png";
                fotoCaminho = Path.Combine(diretorioImagens, nomeImagem);

                // Salvar a imagem no diretório
                pictureBox3.Image.Save(fotoCaminho, System.Drawing.Imaging.ImageFormat.Png);

                // Salvar o caminho da foto na variável estática para uso em outras páginas
                sessaoUsuario.foto = fotoCaminho;
            }

            // Conexão com o banco de dados e inserção
            using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ;"))
            {
                // Comando SQL para inserir o prato, incluindo a referência ao restaurante
                string inserir = "INSERT INTO prato (preco, descricao, foto, nome, fk_restaurante_id) VALUES (@preco, @descricao, @foto, @nome, @restauranteId);";
                MySqlCommand comandos = new MySqlCommand(inserir, conexao);

                // Adicionando parâmetros ao comando SQL
                comandos.Parameters.AddWithValue("@preco", preco);
                comandos.Parameters.AddWithValue("@descricao", descricao);
                comandos.Parameters.AddWithValue("@foto", string.IsNullOrEmpty(fotoCaminho) ? DBNull.Value : (object)fotoCaminho);
                comandos.Parameters.AddWithValue("@nome", nome);
                comandos.Parameters.AddWithValue("@restauranteId", restauranteId);  // Associando o prato ao restaurante

                try
                {
                    // Abrindo a conexão com o banco de dados e executando o comando
                    conexao.Open();
                    comandos.ExecuteNonQuery();  // Executa a inserção
                    MessageBox.Show("Cadastro realizado com sucesso!");

                    // Limpando os campos do formulário após o sucesso
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao cadastrar: {ex.Message}");
                }
            }
     
      
}

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.jfif;";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fotoCaminho = openFileDialog.FileName;

                    pictureBox3.Image = Image.FromFile(fotoCaminho);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            perfilEmpresa perEmp = new perfilEmpresa();
            perEmp.Show();
            this.Close();
        }
    }
}
