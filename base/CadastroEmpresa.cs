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
    public partial class CadastroEmpresa : Form
    {
        public string imagePath;

        public CadastroEmpresa()
        {
            InitializeComponent();

            MySqlConnection conexao = new MySqlConnection();
            conexao.ConnectionString = "SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ; ";
            conexao.Open();

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

        private void CadastroEmpresa_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(134, 8, 26);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Validação das senhas
            string senha = textBox3.Text;
            string confirmarSenha = textBox4.Text;

            if (senha != confirmarSenha)
            {
                MessageBox.Show("As senhas não coincidem.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarSenha(senha))
            {
                return;
            }

            // Validação de campos obrigatórios
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox5.Text) || string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Por favor, preencha os campos obrigatórios (Nome, email, telefone e rua).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Captura os dados do formulário
            string nome = textBox1.Text;
            string email = textBox2.Text;
            string telefone = textBox5.Text;
            string rua = textBox6.Text;

            string fotoCaminho = null;

            // Salvar a imagem, se existir
            if (pictureBox1.Image != null)
            {
                try
                {
                    string diretorioImagens = Path.Combine(Application.StartupPath, "Fotos");
                    if (!Directory.Exists(diretorioImagens))
                    {
                        Directory.CreateDirectory(diretorioImagens);
                    }

                    string nomeImagem = Guid.NewGuid().ToString() + ".png";
                    fotoCaminho = Path.Combine(diretorioImagens, nomeImagem);
                    pictureBox1.Image.Save(fotoCaminho, System.Drawing.Imaging.ImageFormat.Png);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao salvar a imagem: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Conexão com o banco de dados
            using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ;"))
            {
                string inserir = "INSERT INTO restaurante (nome, telefone, rua, email, foto, senha) " +
                                 "VALUES (@nome, @telefone, @rua, @email, @foto, @senha);";

                MySqlCommand comando = new MySqlCommand(inserir, conexao);

                comando.Parameters.AddWithValue("@nome", nome);
                comando.Parameters.AddWithValue("@email", email);
                comando.Parameters.AddWithValue("@senha", senha);
                comando.Parameters.AddWithValue("@telefone", telefone ?? (object)DBNull.Value);
                comando.Parameters.AddWithValue("@rua", rua ?? (object)DBNull.Value);
                comando.Parameters.AddWithValue("@foto", fotoCaminho ?? (object)DBNull.Value);

                try
                {
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Cadastro realizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao cadastrar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void label7_Click(object sender, EventArgs e)
        {
                                
        }

        private void label8_Click(object sender, EventArgs e)
        {
            
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            login log = new login();
            log.Show();
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CadastroUsuario cadUsu = new CadastroUsuario();
            cadUsu.Show();
            this.Close();
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '(' && e.KeyChar != ')' && e.KeyChar != '-')
            {
                e.Handled = true; 
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

                    pictureBox1.Image = Image.FromFile(fotoCaminho);
                }
            }
        }
    }
}
