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
    public partial class CadastroUsuario : Form
    {
        public string imagePath;

        public CadastroUsuario()
        {
            InitializeComponent();

            //conexao com o banco
            MySqlConnection conexao = new MySqlConnection();
            conexao.ConnectionString = "SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ; ";
            conexao.Open();

        }

        private void CadastroUsuario_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(134, 8, 26);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            
        }

        private void label7_Click(object sender, EventArgs e)
        {
           
        }

        private bool ValidarSenha(string senha)
        {

            if (senha.Length < 8)
            {
                MessageBox.Show("A senha deve ter pelo menos 8 caracteres.");
                return false;
            }


            if (!senha.Any(char.IsLetter))
            {
                MessageBox.Show("A senha deve conter pelo menos uma letra.");
                return false;
            }


            if (!senha.Any(char.IsDigit))
            {
                MessageBox.Show("A senha deve conter pelo menos um número.");
                return false;
            }

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string senha = textBox3.Text;
            string confirmarSenha = textBox4.Text;

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Por favor, insira o seu nome.", "Campo obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Por favor, insira o seu email.", "Campo obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!ValidarSenha(senha))
            {
                return; 
            }

            // Verificar se as senhas coincidem
            if (senha == confirmarSenha)
            {
                string nome = textBox1.Text;
                string email = textBox2.Text;

                string fotoCaminho = null;


                if (pictureBox1.Image != null)
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
                    pictureBox1.Image.Save(fotoCaminho, System.Drawing.Imaging.ImageFormat.Png);

                    // Salvar o caminho da foto na variável estática para uso em outras páginas
                    sessaoUsuario.foto = fotoCaminho;
                }

                // Conexão com o banco de dados e inserção
                using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ;"))
                {
                    string inserir = "INSERT INTO cliente (nome, email, senha, foto) VALUES (@nome, @email, @senha, @foto);";
                    MySqlCommand comandos = new MySqlCommand(inserir, conexao);

                    comandos.Parameters.AddWithValue("@nome", nome);
                    comandos.Parameters.AddWithValue("@email", email);
                    comandos.Parameters.AddWithValue("@senha", senha);
                    comandos.Parameters.AddWithValue("@foto", string.IsNullOrEmpty(fotoCaminho) ? DBNull.Value : (object)fotoCaminho);

                    try
                    {
                        conexao.Open();
                        comandos.ExecuteNonQuery();
                        MessageBox.Show("Cadastro realizado com sucesso!");
                        this.Close();  
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao cadastrar: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("As senhas não coincidem.");
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CadastroEmpresa cadEmp = new CadastroEmpresa();
            cadEmp.Show();
            this.Close();
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            login log = new login();
            log.Show();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.jfif;";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fotoCaminho = openFileDialog.FileName;

                    pictureBox1.Image = Image.FromFile(fotoCaminho);
                }
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
