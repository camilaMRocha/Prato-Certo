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

                    // Verifique primeiro se o usuário é um restaurante
                    string queryRestaurante = "SELECT id, nome, email, senha, telefone, rua, foto, status FROM restaurante WHERE email = @Email AND senha = @Senha;";
                    using (MySqlCommand comandoRestaurante = new MySqlCommand(queryRestaurante, conexao))
                    {
                        comandoRestaurante.Parameters.AddWithValue("@Email", email);
                        comandoRestaurante.Parameters.AddWithValue("@Senha", senha);

                        using (MySqlDataReader leitorRestaurante = comandoRestaurante.ExecuteReader())
                        {
                            if (leitorRestaurante.Read())
                            {
                                // Preenche a sessão com os dados do restaurante
                                sessaoUsuario.id = leitorRestaurante.GetInt32("id");
                                sessaoUsuario.nome = leitorRestaurante.GetString("nome");
                                sessaoUsuario.email = leitorRestaurante.GetString("email");
                                sessaoUsuario.senha = leitorRestaurante.GetString("senha");
                                sessaoUsuario.telefone = leitorRestaurante.GetString("telefone");
                                sessaoUsuario.rua = leitorRestaurante.GetString("rua");
                                sessaoUsuario.status = leitorRestaurante.GetInt32("status");
                                sessaoUsuario.foto = leitorRestaurante.IsDBNull(leitorRestaurante.GetOrdinal("foto")) ? null : leitorRestaurante.GetString("foto");

                                // Redireciona para a página de restaurante
                                homePageEmpresa restauranteHome = new homePageEmpresa();
                                restauranteHome.Show();
                                this.Hide(); // Esconde o formulário de login
                                return; // Sai da função se for um restaurante
                            }
                        }
                    }

                    // Se não encontrar o restaurante, verifica se é um cliente
                    string queryCliente = "SELECT id, nome, email, senha, foto FROM cliente WHERE email = @Email AND senha = @Senha;";
                    using (MySqlCommand comandoCliente = new MySqlCommand(queryCliente, conexao))
                    {
                        comandoCliente.Parameters.AddWithValue("@Email", email);
                        comandoCliente.Parameters.AddWithValue("@Senha", senha);

                        using (MySqlDataReader leitorCliente = comandoCliente.ExecuteReader())
                        {
                            if (leitorCliente.Read())
                            {
                                // Preenche a sessão com os dados do cliente
                                sessaoUsuario.id = leitorCliente.GetInt32("id");
                                sessaoUsuario.nome = leitorCliente.GetString("nome");
                                sessaoUsuario.email = leitorCliente.GetString("email");
                                sessaoUsuario.senha = leitorCliente.GetString("senha");
                                sessaoUsuario.foto = leitorCliente.IsDBNull(leitorCliente.GetOrdinal("foto")) ? null : leitorCliente.GetString("foto");

                                // Redireciona para a página do cliente
                                homePage clienteHome = new homePage();
                                clienteHome.Show();
                                this.Hide(); // Esconde o formulário de login
                            }
                            else
                            {
                                MessageBox.Show("Email ou senha incorretos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
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
