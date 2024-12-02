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
using Excel = Microsoft.Office.Interop.Excel;

namespace pratocerto
{
    public partial class perfilEmpresa : Form
    {
        public perfilEmpresa()
        {
            InitializeComponent();
            MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ; ");
            conexao.Open();

            // Atualizando informações do restaurante logado
            label1.Text = $"{sessaoUsuario.nome}!";
            label2.Text = $"{sessaoUsuario.nome}";

            textBox2.Text = sessaoUsuario.nome;
            textBox3.Text = sessaoUsuario.email;
            textBox4.Text = sessaoUsuario.telefone;
            textBox5.Text = sessaoUsuario.rua;
            textBox6.Text = sessaoUsuario.senha;


            try
            {
                // Consulta SQL para buscar os pratos do restaurante logado
                string query = "SELECT * FROM prato WHERE fk_restaurante_id = @restauranteId";

                using (MySqlCommand consulta = new MySqlCommand(query, conexao))
                {
                    // Substituir @restauranteId pelo ID do restaurante logado
                    consulta.Parameters.AddWithValue("@restauranteId", sessaoUsuario.id);

                    using (MySqlDataReader resultado = consulta.ExecuteReader())
                    {
                        // Limpa qualquer configuração anterior
                        dataGridView1.Columns.Clear();

                        // Adiciona colunas de dados
                        dataGridView1.Columns.Add("prato_id", "ID");
                        dataGridView1.Columns.Add("prato_nome", "Nome");
                        dataGridView1.Columns.Add("descricao", "Descrição");
                        dataGridView1.Columns.Add("preco", "Preço");
                        dataGridView1.Columns.Add("media_nota", "Média Nota");


                        // Adiciona a coluna de Botões "Alterar" e "Deletar"
                        DataGridViewButtonColumn btnAlterar = new DataGridViewButtonColumn();
                        btnAlterar.HeaderText = "Alterar";
                        btnAlterar.Name = "btnAlterar";
                        btnAlterar.Text = "Alterar";
                        btnAlterar.UseColumnTextForButtonValue = true;
                        dataGridView1.Columns.Add(btnAlterar);

                        DataGridViewButtonColumn btnDeletar = new DataGridViewButtonColumn();
                        btnDeletar.HeaderText = "Deletar";
                        btnDeletar.Name = "btnDeletar";
                        btnDeletar.Text = "Deletar";
                        btnDeletar.UseColumnTextForButtonValue = true;
                        dataGridView1.Columns.Add(btnDeletar);

                        // Preenchendo o DataGridView com os dados
                        while (resultado.Read())
                        {

                            // Adiciona as informações ao DataGridView
                            dataGridView1.Rows.Add(
                                resultado["id"].ToString(),
                                resultado["nome"].ToString(),
                                resultado["descricao"].ToString(),
                                resultado["preco"].ToString(),
                                resultado["media_nota"].ToString()
                            );
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar pratos: {ex.Message}");
            }
            finally
            {
                // Fechar a conexão no final
                conexao.Close();
            }




        }

        private void CarregarPratos()
        {
            
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
            if (!string.IsNullOrEmpty(sessaoUsuario.foto) && File.Exists(sessaoUsuario.foto))
            {
                pictureBox1.Image = Image.FromFile(sessaoUsuario.foto); // Recupera e exibe a foto corretamente
            }
            else
            {
                pictureBox1.Image = null;
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
            string novoTel = textBox4.Text;

            if (string.IsNullOrWhiteSpace(novoTel))
            {
                MessageBox.Show("Por favor, insirá um novo telefone válido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ; "))
            {
                string atualizar = "UPDATE restaurante SET telefone = @novoTel WHERE id = @idUsuario";

                MySqlCommand comando = new MySqlCommand(atualizar, conexao);
                comando.Parameters.AddWithValue("@novoTel", novoTel);
                comando.Parameters.AddWithValue("@idUsuario", sessaoUsuario.id);

                try
                {
                    conexao.Open();
                    int linhasAfetadas = comando.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                    {
                        sessaoUsuario.telefone = novoTel;
                        MessageBox.Show("Telefone foi alterado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Nenhuma alteração foi realizada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Errp ao alterar o telefone.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
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
                comando.Parameters.AddWithValue("@foto", caminhoFoto); // Salvar caminho completo ou relativo
                comando.Parameters.AddWithValue("@id", sessaoUsuario.id);

                try
                {
                    conexao.Open();
                    int rowsAffected = comando.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Foto alterada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Erro ao atualizar foto no banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao atualizar a foto: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                // Pegar o ID do restaurante a partir de um TextBox
                int restauranteId = sessaoUsuario.id;

                // String de conexão com o banco de dados
                string connectionString = "Server=127.0.0.1;Database=prato_certo;User=root;Password=;";

                // Comando SQL para atualizar o status do restaurante para '0'
                string sql = "UPDATE restaurante SET status = '0' WHERE id = @restauranteId";

                // Usar o MySqlCommand para executar o comando SQL
                using (var connection = new MySqlConnection(connectionString))
                {
                    // Abrir a conexão
                    connection.Open();

                    using (var command = new MySqlCommand(sql, connection))
                    {
                        // Passar o parâmetro do ID do restaurante
                        command.Parameters.AddWithValue("@restauranteId", restauranteId);

                        // Executar o comando para atualizar o status
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Restaurante desativado com sucesso!");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Restaurante não encontrado ou já desativado.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string novoNome = textBox2.Text;

            if (string.IsNullOrWhiteSpace(novoNome))
            {
                MessageBox.Show("Por favor, insirá um novo nome válido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ; "))
            {
                string atualizar = "UPDATE restaurante SET nome = @novoNome WHERE id = @idUsuario";

                MySqlCommand comando = new MySqlCommand(atualizar, conexao);
                comando.Parameters.AddWithValue("@novoNome", novoNome);
                comando.Parameters.AddWithValue("@idUsuario", sessaoUsuario.id);

                try
                {
                    conexao.Open();
                    int linhasAfetadas = comando.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                    {
                        sessaoUsuario.nome = novoNome;
                        MessageBox.Show("Nome foi alterado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        label1.Text = $"{sessaoUsuario.nome}";
                        label2.Text = $"{sessaoUsuario.nome}";
                    }
                    else
                    {
                        MessageBox.Show("Nenhuma alteração foi realizada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Errp ao alterar o nome.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string novoEmail = textBox3.Text;

            if (string.IsNullOrWhiteSpace(novoEmail))
            {
                MessageBox.Show("Por favor, insirá um novo E-mail válido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ; "))
            {
                string atualizar = "UPDATE restaurante SET email = @novoEmail WHERE id = @idUsuario";

                MySqlCommand comando = new MySqlCommand(atualizar, conexao);
                comando.Parameters.AddWithValue("@novoEmail", novoEmail);
                comando.Parameters.AddWithValue("@idUsuario", sessaoUsuario.id);

                try
                {
                    conexao.Open();
                    int linhasAfetadas = comando.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                    {
                        sessaoUsuario.email = novoEmail;
                        MessageBox.Show("E-mail foi alterado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Nenhuma alteração foi realizada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Errp ao alterar o e-mail.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string novoLocal = textBox5.Text;

            if (string.IsNullOrWhiteSpace(novoLocal))
            {
                MessageBox.Show("Por favor, insirá um local nome válido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ; "))
            {
                string atualizar = "UPDATE restaurante SET rua = @novoLocal WHERE id = @idUsuario";

                MySqlCommand comando = new MySqlCommand(atualizar, conexao);
                comando.Parameters.AddWithValue("@novoLocal", novoLocal);
                comando.Parameters.AddWithValue("@idUsuario", sessaoUsuario.id);

                try
                {
                    conexao.Open();
                    int linhasAfetadas = comando.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                    {
                        sessaoUsuario.rua = novoLocal;
                        MessageBox.Show("Local foi alterado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Nenhuma alteração foi realizada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Errp ao alterar o local.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            string novoSenha = textBox6.Text;

            if (string.IsNullOrWhiteSpace(novoSenha))
            {
                MessageBox.Show("Por favor, insirá um novo senha válido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ; "))
            {
                string atualizar = "UPDATE restaurante SET senha = @novoSenha WHERE id = @idUsuario";

                MySqlCommand comando = new MySqlCommand(atualizar, conexao);
                comando.Parameters.AddWithValue("@novoSenha", novoSenha);
                comando.Parameters.AddWithValue("@idUsuario", sessaoUsuario.id);

                try
                {
                    conexao.Open();
                    int linhasAfetadas = comando.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                    {
                        sessaoUsuario.senha = novoSenha;
                        MessageBox.Show("Senha foi alterado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("Nenhuma alteração foi realizada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Errp ao alterar o senha.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            adicionarComida AdiCom = new adicionarComida();
            AdiCom.Show();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Garante que estamos lidando com uma linha válida
            {
                // Identifica se o clique foi na coluna "Alterar"
                if (dataGridView1.Columns[e.ColumnIndex].Name == "btnAlterar")
                {
                    // Obtenha o ID do prato usando o alias correto
                    int idPrato = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["prato_id"].Value);
                    string nomePrato = dataGridView1.Rows[e.RowIndex].Cells["prato_nome"].Value.ToString();
                    string descricaoPrato = dataGridView1.Rows[e.RowIndex].Cells["descricao"].Value.ToString();
                    string precoPrato = dataGridView1.Rows[e.RowIndex].Cells["preco"].Value.ToString();

                    // Agora, você pode passar esses dados para o formulário de alteração
                    alterarComida formularioAlterar = new alterarComida(idPrato, nomePrato, descricaoPrato, precoPrato);
                    formularioAlterar.ShowDialog();
                }
                // Identifica se o clique foi na coluna "Deletar"
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "btnDeletar")
                {
                    int idPrato = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["prato_id"].Value);
                    DialogResult resultado = MessageBox.Show("Tem certeza que deseja excluir este prato?", "Excluir", MessageBoxButtons.YesNo);

                    if (resultado == DialogResult.Yes)
                    {
                        // Chama a função para deletar o prato
                        DeletarPrato(idPrato);

                        // Atualiza o DataGridView
                        CarregarPratos();
                    }
                }
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 0)
            {
                // Instancia o aplicativo Excel
                Excel.Application XcellApp = new Excel.Application();

                // Adiciona um novo workbook
                XcellApp.Workbooks.Add(Type.Missing);

                // Preenche o cabeçalho com os nomes das colunas
                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    XcellApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }

                // Preenche os dados das células
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        // Verifica se a célula tem valor antes de tentar acessar
                        XcellApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value?.ToString();
                    }
                }

                // Ajusta a largura das colunas automaticamente
                XcellApp.Columns.AutoFit();

                // Exibe o Excel para o usuário
                XcellApp.Visible = true;
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 5) // Coluna 5 é onde a imagem está
            {
                DataGridViewImageCell imageCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewImageCell;

                if (imageCell != null && imageCell.Value != null)
                {
                    // Defina o tamanho desejado da imagem (ajuste os valores conforme necessário)
                    Image imagem = imageCell.Value as Image;
                    int largura = 100; // Largura desejada
                    int altura = 100; // Altura desejada

                    // Crie uma nova imagem redimensionada
                    imagem = new Bitmap(imagem, new Size(largura, altura));

                    // Atualize a célula com a imagem redimensionada
                    imageCell.Value = imagem;
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        // Método para deletar o prato
        private void DeletarPrato(int idPrato)
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection("SERVER=localhost;DATABASE=prato_certo;UID=root;PASSWORD= ;"))
                {
                    conexao.Open();

                    // Comando SQL para excluir o prato
                    string sql = "DELETE FROM prato WHERE id = @idPrato";
                    using (MySqlCommand comando = new MySqlCommand(sql, conexao))
                    {
                        comando.Parameters.AddWithValue("@idPrato", idPrato);

                        // Executa o comando
                        int linhasAfetadas = comando.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            MessageBox.Show("Prato excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Erro ao excluir o prato.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir prato: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
            perfilEmpresa perEmp = new perfilEmpresa();
            perEmp.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
