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

namespace pratocerto
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();

            MySqlConnection conexao = new MySqlConnection();
            conexao.ConnectionString = "SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ; ";
            conexao.Open();



        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string senha = textBox2.Text;

            using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ;"))
            {
                try
                {
                    conexao.Open();

                    // Consulta para verificar login e recuperar informações do usuário
                    string query = "SELECT id, nome, email, tipo, senha, telefone, rua, foto FROM cliente WHERE email = @Email AND senha = @Senha;";
                    MySqlCommand comando = new MySqlCommand(query, conexao);
                    comando.Parameters.AddWithValue("@Email", email);
                    comando.Parameters.AddWithValue("@Senha", senha);

                    MySqlDataReader leitor = comando.ExecuteReader();

                    if (leitor.Read())
                    {
                        // Preenche a sessão com os dados do usuário
                        sessaoUsuario.id = leitor.GetInt32("id");
                        sessaoUsuario.nome = leitor.GetString("nome");
                        sessaoUsuario.email = leitor.GetString("email");
                        sessaoUsuario.tipo = leitor.GetInt32("tipo");
                        sessaoUsuario.senha = leitor.GetString("senha");

                        
                        sessaoUsuario.telefone = leitor.IsDBNull(leitor.GetOrdinal("telefone")) ? null : leitor.GetString("telefone");

                      
                        sessaoUsuario.rua = leitor.IsDBNull(leitor.GetOrdinal("rua")) ? null : leitor.GetString("rua");

                        // Verifica se a foto é nula, se for, atribui "Imagem não disponível"
                        sessaoUsuario.foto = leitor.IsDBNull(leitor.GetOrdinal("foto")) ? null : leitor.GetString("foto");

                        leitor.Close();

                        
                        if (sessaoUsuario.tipo == 0) 
                        {
                            homePage clienteHome = new homePage();
                            clienteHome.Show();
                        }
                        else if (sessaoUsuario.tipo == 1)
                        {
                            homePageEmpresa restauranteHome = new homePageEmpresa();
                            restauranteHome.Show();
                        }

                        this.Hide(); // Esconde o formulário de login
                    }
                    else
                    {
                        MessageBox.Show("Email ou senha incorretos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao conectar com o banco de dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void login_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(134, 8, 26);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            CadastroUsuario cadUsu = new CadastroUsuario();
            cadUsu.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CadastroEmpresa cadEmp = new CadastroEmpresa();
            cadEmp.Show();
        }
    }
}
