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

          
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ;"))
            {
                try
                {

                    conexao.Open();

                    
                    string query = "SELECT * FROM cliente WHERE email = @email AND senha = @senha;";
                    MySqlCommand comando = new MySqlCommand(query, conexao);

                    comando.Parameters.AddWithValue("@email", email);
                    comando.Parameters.AddWithValue("@senha", senha);


                    MySqlDataReader leitor = comando.ExecuteReader();

                    if (leitor.Read())
                    {
                        // Login bem-sucedido, verifica o tipo do usuário
                        int tipo = leitor.GetInt32("tipo");
                        leitor.Close();

                        if (tipo == 0)
                        {
                            //home usuário comum
                            MessageBox.Show("Bem-vindo, cliente!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            homePage clienteHome = new homePage();
                            clienteHome.Show();
                        }
                        else if (tipo == 1)
                        {
                            //home empresa
                            MessageBox.Show("Bem-vindo, restaurante!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            homePageEmpresa restauranteHome = new homePageEmpresa();
                            restauranteHome.Show();
                        }

                        this.Hide(); // Esconde a janela atual após o login
                    }
                    else
                    {
                        // Login falhou
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
