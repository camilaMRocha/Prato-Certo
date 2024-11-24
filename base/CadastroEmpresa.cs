﻿using System;
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
    public partial class CadastroEmpresa : Form
    {
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

            // Verificar se a senha é válida
            if (!ValidarSenha(senha))
            {
                return; // Interrompe o cadastro se a senha for inválida
            }

            // Verificar se as senhas coincidem
            if (senha == confirmarSenha)
            {
                string nome = textBox1.Text;
                string email = textBox2.Text;
                string telefone = textBox3.Text;
                string rua = textBox4.Text;

                //inserindo o cliente
                using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ;"))
                {
                    // Corrigir a consulta SQL
                    string inserir = "INSERT INTO cliente (nome, email, senha, tipo, telefone, rua) VALUES (@nome, @email, @senha, @tipo, @telefone, @rua);";
                    MySqlCommand comandos = new MySqlCommand(inserir, conexao);

                    // Adicionar os parâmetros corretamente
                    comandos.Parameters.AddWithValue("@nome", nome);
                    comandos.Parameters.AddWithValue("@email", email);
                    comandos.Parameters.AddWithValue("@senha", senha);
                    comandos.Parameters.AddWithValue("@tipo", 1);
                    comandos.Parameters.AddWithValue("@telefone", telefone);
                    comandos.Parameters.AddWithValue("@rua", rua);

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
    }
}
