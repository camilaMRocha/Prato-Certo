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
            string email = textBox1.Text.Trim();
            string senha = textBox2.Text.Trim();

            bool loginValido = false; // Controlador para evitar múltiplas ações simultâneas

            using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD=;"))
            {
                try
                {
                    conexao.Open();

                    // Verifica primeiro na tabela de restaurantes
                    string queryRestaurante = "SELECT id, nome, email, senha, telefone, rua, foto, status FROM restaurante WHERE email = @Email AND senha = @Senha;";
                    using (MySqlCommand comandoRestaurante = new MySqlCommand(queryRestaurante, conexao))
                    {
                        comandoRestaurante.Parameters.AddWithValue("@Email", email);
                        comandoRestaurante.Parameters.AddWithValue("@Senha", senha);

                        using (MySqlDataReader leitorRestaurante = comandoRestaurante.ExecuteReader())
                        {
                            if (leitorRestaurante.Read())
                            {
                                // Login bem-sucedido como restaurante
                                loginValido = true;

                                // Preencher dados de sessão
                                sessaoUsuario.id = leitorRestaurante.GetInt32("id");
                                sessaoUsuario.nome = leitorRestaurante.GetString("nome");
                                sessaoUsuario.email = leitorRestaurante.GetString("email");
                                sessaoUsuario.senha = leitorRestaurante.GetString("senha");
                                sessaoUsuario.telefone = leitorRestaurante.GetString("telefone");
                                sessaoUsuario.rua = leitorRestaurante.GetString("rua");
                                sessaoUsuario.foto = leitorRestaurante.IsDBNull(leitorRestaurante.GetOrdinal("foto")) ? null : leitorRestaurante.GetString("foto");

                                // Verifica status
                                if (leitorRestaurante.GetInt32("status") == 0)
                                {
                                    AtivarRestaurante(sessaoUsuario.id);
                                }

                                // Redireciona para a homepage de restaurante
                                homePageEmpresa restauranteHome = new homePageEmpresa();
                                restauranteHome.Show();
                                this.Hide();
                                return;
                            }
                        }
                    }

                    // Se não for restaurante, verifica na tabela de clientes
                    if (!loginValido)
                    {
                        string queryCliente = "SELECT id, nome, email, senha, foto FROM cliente WHERE email = @Email AND senha = @Senha;";
                        using (MySqlCommand comandoCliente = new MySqlCommand(queryCliente, conexao))
                        {
                            comandoCliente.Parameters.AddWithValue("@Email", email);
                            comandoCliente.Parameters.AddWithValue("@Senha", senha);

                            using (MySqlDataReader leitorCliente = comandoCliente.ExecuteReader())
                            {
                                if (leitorCliente.Read())
                                {
                                    // Login bem-sucedido como cliente
                                    loginValido = true;

                                    // Preencher dados de sessão
                                    sessaoUsuario.id = leitorCliente.GetInt32("id");
                                    sessaoUsuario.nome = leitorCliente.GetString("nome");
                                    sessaoUsuario.email = leitorCliente.GetString("email");
                                    sessaoUsuario.senha = leitorCliente.GetString("senha");
                                    sessaoUsuario.foto = leitorCliente.IsDBNull(leitorCliente.GetOrdinal("foto")) ? null : leitorCliente.GetString("foto");

                                    // Redireciona para a homepage do cliente
                                    homePage clienteHome = new homePage();
                                    clienteHome.Show();
                                    this.Hide();
                                    return;
                                }
                            }
                        }
                    }

                    // Se nenhum login foi validado, exibe mensagem de erro
                    if (!loginValido)
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

        private void AtivarRestaurante(int restauranteId)
        {
            string connectionString = "SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ;";
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                try
                {
                    conexao.Open();

                    // Comando SQL para atualizar o status do restaurante para 1 (Ativo)
                    string query = "UPDATE restaurante SET status = 1 WHERE id = @restauranteId AND status = 0";

                    using (MySqlCommand cmd = new MySqlCommand(query, conexao))
                    {
                        // Adiciona o parâmetro para o ID do restaurante
                        cmd.Parameters.AddWithValue("@restauranteId", restauranteId);

                        // Executa o comando de atualização
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Restaurante reativado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Não foi possível reativar o restaurante. Verifique se ele está desativado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao reativar o restaurante: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
