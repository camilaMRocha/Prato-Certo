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
    public partial class perfilEmpresa : Form
    {
        public perfilEmpresa()
        {
            InitializeComponent();

            MySqlConnection conexao = new MySqlConnection();
            conexao.ConnectionString = "SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ; ";
            conexao.Open();

            label1.Text = $"{sessaoUsuario.nome}!";
            label2.Text = $"{sessaoUsuario.nome}!";

            textBox2.Text = sessaoUsuario.nome;
            textBox3.Text = sessaoUsuario.email;
            textBox4.Text = sessaoUsuario.telefone;
            textBox5.Text = sessaoUsuario.rua;
            textBox6.Text = sessaoUsuario.senha;

        }

        private void perfilEmpresa_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(sessaoUsuario.foto) && File.Exists(sessaoUsuario.foto))
            {
                // Se a foto existe, carregar na PictureBox
                pictureBox1.Image = Image.FromFile(sessaoUsuario.foto);
                pictureBox1.BorderStyle = BorderStyle.None;  // Remover borda

                pictureBox3.Image = Image.FromFile(sessaoUsuario.foto);
                pictureBox3.BorderStyle = BorderStyle.None;
            }
            else
            {
                // Caso contrário, deixar a PictureBox vazia e adicionar uma borda
                pictureBox1.Image = null;  // A PictureBox ficará sem imagem
                pictureBox1.BorderStyle = BorderStyle.Fixed3D;  // Adiciona uma borda

                pictureBox3.Image = null;
                pictureBox3.BorderStyle = BorderStyle.Fixed3D;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            this.panel1.BackColor = Color.FromArgb(134, 8, 26);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            this.panel2.BackColor = Color.FromArgb(134, 8, 26);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            homePageEmpresa homeRestaurante = new homePageEmpresa();
            homeRestaurante.Show();
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            this.panel3.BackColor = Color.FromArgb(178, 48, 67);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            this.panel6.BackColor = Color.FromArgb(178, 48, 67);
        }

        private void panel5_Paint_1(object sender, PaintEventArgs e)
        {
           
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            visualizarComentários visuCom = new visualizarComentários();
            visuCom.Show();
            this.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            visualizarComentários visuCom = new visualizarComentários();
            visuCom.Show();
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            alterarComida altComi = new alterarComida();
            altComi.Show();
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            excluirComida excComi = new excluirComida();
            excComi.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.jfif;";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string novoCaminhoFoto = openFileDialog.FileName;

                    // Copiar a imagem para a pasta local do aplicativo
                    string diretorioFotos = Path.Combine(Application.StartupPath, "Fotos");
                    if (!Directory.Exists(diretorioFotos))
                    {
                        Directory.CreateDirectory(diretorioFotos);
                    }

                    // Gerar um novo nome para a imagem
                    string nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(novoCaminhoFoto);
                    string caminhoDestino = Path.Combine(diretorioFotos, nomeArquivo);

                    File.Copy(novoCaminhoFoto, caminhoDestino, true);

                    // Atualizar a PictureBox com a nova imagem
                    pictureBox1.Image = Image.FromFile(caminhoDestino);
                    pictureBox3.Image = Image.FromFile(caminhoDestino);

                    // Atualizar a foto na sessão
                    sessaoUsuario.foto = caminhoDestino;

                    // Atualizar o banco de dados
                    AtualizarFotoUsuario(caminhoDestino);
                }
            }
        }
            private void AtualizarFotoUsuario(string caminhoFoto)
            {
                using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ;"))
                {
                    string query = "UPDATE cliente SET foto = @foto WHERE id = @id";

                    MySqlCommand comando = new MySqlCommand(query, conexao);
                    comando.Parameters.AddWithValue("@foto", caminhoFoto);
                    comando.Parameters.AddWithValue("@id", sessaoUsuario.id);

                    try
                    {
                        conexao.Open();
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Foto alterada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao atualizar a foto: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        
    }
}
