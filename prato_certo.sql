-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 02/12/2024 às 04:11
-- Versão do servidor: 10.4.32-MariaDB
-- Versão do PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `prato_certo`
--

-- --------------------------------------------------------

--
-- Estrutura para tabela `avaliacao`
--

CREATE TABLE `avaliacao` (
  `fk_prato_id` int(11) DEFAULT NULL,
  `fk_cliente_id` int(11) DEFAULT NULL,
  `id` int(11) NOT NULL,
  `data` int(11) DEFAULT NULL,
  `nota` int(11) DEFAULT NULL,
  `comentario` varchar(200) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `avaliacao`
--

INSERT INTO `avaliacao` (`fk_prato_id`, `fk_cliente_id`, `id`, `data`, `nota`, `comentario`) VALUES
(1, 1, 61, 2024, 4, 'Prato incrível, muito saboroso!'),
(2, 3, 97, 2024, 3, 'Gostei bastante, mas poderia melhorar.'),
(3, 4, 98, 2024, 5, 'Recomendo, sabores incríveis e apresentação ótima!'),
(4, 4, 99, 2024, 4, 'Saudável e muito bem preparado.'),
(5, 5, 100, 2024, 5, 'Melhor prato que já experimentei!'),
(6, 6, 101, 2024, 2, 'Boa refeição, mas o tempero estava fraco.'),
(7, 7, 102, 2024, 4, 'Sabores autênticos, vale a pena experimentar.'),
(8, 8, 103, 2024, 3, 'Rápido e saboroso, uma boa escolha.'),
(9, 9, 104, 2024, 5, 'Delicioso, não há nada igual!'),
(10, 10, 105, 2024, 1, 'Não gostei, estava sem sabor.'),
(11, 11, 106, 2024, 4, 'Muito bom, mas poderia ser mais quente.');

--
-- Acionadores `avaliacao`
--
DELIMITER $$
CREATE TRIGGER `atualizar_media_nota` AFTER INSERT ON `avaliacao` FOR EACH ROW BEGIN
    DECLARE novo_media_nota DECIMAL(5,2);
    
    -- Calcular a nova média das notas para o prato após a inserção de uma avaliação
    SELECT AVG(nota) 
    INTO novo_media_nota
    FROM avaliacao
    WHERE fk_prato_id = NEW.fk_prato_id;
    
    -- Atualizar a média na tabela prato
    UPDATE prato
    SET media_nota = novo_media_nota
    WHERE id = NEW.fk_prato_id;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Estrutura para tabela `cliente`
--

CREATE TABLE `cliente` (
  `id` int(11) NOT NULL,
  `nome` varchar(200) DEFAULT NULL,
  `email` varchar(200) DEFAULT NULL,
  `senha` varchar(100) DEFAULT NULL,
  `foto` varchar(200) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `cliente`
--

INSERT INTO `cliente` (`id`, `nome`, `email`, `senha`, `foto`) VALUES
(1, 'monica', 'monica@gmail.com', '1234monica', 'C:\\Users\\Alima\\Desktop\\Etec\\4°Bimestre\\00trabalho ds\\Prato-Certo\\base\\bin\\Debug\\Fotos\\83d7d3a1-7ee8-4b79-9c44-f71afce89d30.jpg'),
(3, 'João Silva', 'joao.silva@email.com', 'Senha1234', 'foto1.jpg'),
(4, 'Maria Oliveira', 'maria.oliveira@email.com', 'Maria2023', 'foto2.jpg'),
(5, 'Carlos Santos', 'carlos.santos@email.com', 'Carlos89', 'foto3.jpg'),
(6, 'Ana Lima', 'ana.lima@email.com', 'Ana2023L', 'foto4.jpg'),
(7, 'Paulo Souza', 'paulo.souza@email.com', 'Paulo876', 'foto5.jpg'),
(8, 'Fernanda Costa', 'fernanda.costa@email.com', 'Fer45678', 'foto6.jpg'),
(9, 'Rafael Almeida', 'rafael.almeida@email.com', 'Rafa1984', 'foto7.jpg'),
(10, 'Juliana Melo', 'juliana.melo@email.com', 'Juli5678', 'foto8.jpg'),
(11, 'Bruno Rocha', 'bruno.rocha@email.com', 'Bru54321', 'foto9.jpg'),
(12, 'Carla Nunes', 'carla.nunes@email.com', 'Carla123', 'foto10.jpg'),
(13, 'Cliente 50', 'cliente50@email.com', 'Clien50a', 'foto50.jpg'),
(14, 'Lucas Pereira', 'lucas.pereira@email.com', 'Lucas2024', 'foto51.jpg'),
(15, 'Fernanda Alves', 'fernanda.alves@email.com', 'Fer45678', 'foto52.jpg'),
(16, 'Gabriel Souza', 'gabriel.souza@email.com', 'Gab12345', 'foto53.jpg'),
(17, 'Isabela Mendes', 'isabela.mendes@email.com', 'IsaM1234', 'foto54.jpg'),
(18, 'Ricardo Costa', 'ricardo.costa@email.com', 'Ric45678', 'foto55.jpg'),
(19, 'Amanda Ferreira', 'amanda.ferreira@email.com', 'Amanda01', 'foto56.jpg'),
(20, 'Thiago Lima', 'thiago.lima@email.com', 'Thi2023L', 'foto57.jpg'),
(21, 'Camila Rocha', 'camila.rocha@email.com', 'Cami0987', 'foto58.jpg'),
(22, 'Felipe Nogueira', 'felipe.nogueira@email.com', 'Feli5678', 'foto59.jpg'),
(23, 'Priscila Martins', 'priscila.martins@email.com', 'Pris8765', 'foto60.jpg'),
(24, 'Mariana Silva', 'mariana.silva@email.com', 'Mari1234', 'foto61.jpg'),
(25, 'Leandro Almeida', 'leandro.almeida@email.com', 'Lean4321', 'foto62.jpg'),
(26, 'Paula Oliveira', 'paula.oliveira@email.com', 'Paul8907', 'foto63.jpg'),
(27, 'Rafaela Lima', 'rafaela.lima@email.com', 'Rafa4567', 'foto64.jpg'),
(28, 'Vitor Santos', 'vitor.santos@email.com', 'Vito7654', 'foto65.jpg'),
(29, 'Jéssica Souza', 'jessica.souza@email.com', 'Jess0987', 'foto66.jpg'),
(30, 'André Carvalho', 'andre.carvalho@email.com', 'Andr1234', 'foto67.jpg'),
(31, 'Larissa Melo', 'larissa.melo@email.com', 'Lari5678', 'foto68.jpg'),
(32, 'Pedro Henrique', 'pedro.henrique@email.com', 'Pedr6789', 'foto69.jpg'),
(33, 'Sofia Moreira', 'sofia.moreira@email.com', 'Sofi5432', 'foto70.jpg');

-- --------------------------------------------------------

--
-- Estrutura para tabela `prato`
--

CREATE TABLE `prato` (
  `id` int(11) NOT NULL,
  `media_nota` int(11) DEFAULT NULL,
  `preco` int(11) DEFAULT NULL,
  `descricao` varchar(200) DEFAULT NULL,
  `foto` varchar(200) DEFAULT NULL,
  `nome` varchar(200) DEFAULT NULL,
  `fk_restaurante_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `prato`
--

INSERT INTO `prato` (`id`, `media_nota`, `preco`, `descricao`, `foto`, `nome`, `fk_restaurante_id`) VALUES
(1, 4, 36, 'Feijoada tradicional com arroz, farofa e couve', 'feijoada.jpg', 'Feijoada', 2),
(2, 3, 45, 'Churrasco de picanha com vinagrete e maionese', 'churrasco.jpg', 'Churrasco', 2),
(3, 5, 30, 'Pizza com molho de tomate, mozzarella, manjericão e azeite', 'pizza_margherita.jpg', 'Pizza Margherita', 1),
(4, 4, 25, 'Hambúrguer artesanal com queijo cheddar e bacon', 'burguer_artesanal.jpg', 'Burguer Artesanal', 1),
(5, 5, 50, 'Sushi com variedades de peixe fresco e arroz de sushi', 'sushi_variado.jpg', 'Sushi Variado', 1),
(6, 2, 2, 'carne bovina, salada, tomate, queijo e bacon', 'C:\\Users\\Alima\\Desktop\\Etec\\4°Bimestre\\00trabalho', 'hambuerguer', 1),
(7, 4, 4555, 'ghghfh', 'C:\\Users\\Alima\\Desktop\\Etec\\4°Bimestre\\00trabalho ds\\Prato-Certo\\base\\bin\\Debug\\Fotos\\bc2acabc-8956-4646-9353-5a4a9d50eb6f.png', 'ghbcgh', 1),
(8, 3, 777, 'ghtfhtyh', 'C:\\Users\\Alima\\Desktop\\Etec\\4°Bimestre\\00trabalho ds\\Prato-Certo\\base\\bin\\Debug\\Fotos\\d890a561-0787-472a-b0b0-aebc7d573c25.png', 'fgfgfg', 1),
(9, 5, 8, 'baunilha, leite e etc', 'foto.jpg', 'sorvete baunilhaa', 1),
(10, 1, 150, 'pato assado no forno com tempero', 'C:\\Users\\Alima\\Desktop\\Etec\\4°Bimestre\\00trabalho ds\\Prato-Certo\\base\\bin\\Debug\\Fotos\\1e230e6a-a871-4b2c-b011-26ea577df185.png', 'pato assado', 2),
(11, 4, 6, 'frango, farinha, óleo e entre outros', 'C:\\Users\\Alima\\Desktop\\Etec\\4°Bimestre\\00trabalho ds\\Prato-Certo\\base\\bin\\Debug\\FotosPratos\\8c29e717-c3c1-440b-aaf8-f04fe4dc36c3.png', 'coxinha', 1),
(45, 5, 26, 'Prato sofisticado com ingredientes frescos', 'foto51.jpg', 'Prato Gourmet 51', 1),
(46, 4, 19, 'Prato regional delicioso e autêntico', 'foto52.jpg', 'Prato Regional 52', 2),
(47, 5, 30, 'Combinação perfeita de sabores internacionais', 'foto53.jpg', 'Prato Internacional 53', 3),
(48, 4, 23, 'Refeição saudável e equilibrada', 'foto54.jpg', 'Prato Saudável 54', 4),
(49, 5, 46, 'Especialidade do chef com apresentação impecável', 'foto55.jpg', 'Chef\'s Special 55', 5),
(50, 4, 16, 'Prato simples, mas muito saboroso', 'foto56.jpg', 'Prato Caseiro 56', 6),
(51, 4, 29, 'Sabores autênticos da cozinha local', 'foto57.jpg', 'Prato Tradicional 57', 7),
(52, 4, 21, 'Opção leve para refeições rápidas', 'foto58.jpg', 'Prato Rápido 58', 8),
(53, 5, 36, 'Comida típica com um toque gourmet', 'foto59.jpg', 'Prato Típico 59', 9),
(54, 4, 20, 'Refeição completa para o dia a dia', 'foto60.jpg', 'Prato Familiar 60', 10),
(55, 5, 51, 'Delícia exclusiva do menu', 'foto61.jpg', 'Prato Exclusivo 61', 11),
(56, 4, 15, 'Comida econômica e saborosa', 'foto62.jpg', 'Prato Econômico 62', 12),
(57, 4, 27, 'Refeição com ingredientes orgânicos', 'foto63.jpg', 'Prato Orgânico 63', 13),
(58, 5, 56, 'Prato de luxo com sabores refinados', 'foto64.jpg', 'Prato de Luxo 64', 14),
(59, 4, 24, 'Especialidade regional imperdível', 'foto65.jpg', 'Prato Especial 65', 15),
(60, 4, 17, 'Prato simples e reconfortante', 'foto66.jpg', 'Prato Conforto 66', 16),
(61, 5, 33, 'Combinação gourmet de carnes e vegetais', 'foto67.jpg', 'Prato Completo 67', 17),
(62, 4, 27, 'Opção vegana com sabores incríveis', 'foto68.jpg', 'Prato Vegano 68', 18),
(63, 4, 18, 'Prato rápido e acessível', 'foto69.jpg', 'Prato Simples 69', 19),
(64, 5, 41, 'Prato especial com toque de sofisticação', 'foto70.jpg', 'Prato Final 70', 20);

-- --------------------------------------------------------

--
-- Estrutura para tabela `restaurante`
--

CREATE TABLE `restaurante` (
  `id` int(11) NOT NULL,
  `nome` varchar(100) DEFAULT NULL,
  `email` varchar(200) DEFAULT NULL,
  `telefone` varchar(20) DEFAULT NULL,
  `rua` varchar(200) DEFAULT NULL,
  `senha` varchar(20) DEFAULT NULL,
  `foto` varchar(200) DEFAULT NULL,
  `status` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `restaurante`
--

INSERT INTO `restaurante` (`id`, `nome`, `email`, `telefone`, `rua`, `senha`, `foto`, `status`) VALUES
(1, 'mc downald\'s', 'mc@gmail.com', '1959565654', 'centroo', '123456mc', NULL, '1'),
(2, 'gogo', 'gogo@gmail.com', '19546565', 'sodjksd', '1234gogo', NULL, '1'),
(3, 'Restaurante Saboroso', 'contato@saboroso.com', '123456789', 'Rua A, 123', 'Rest1234', 'foto1.jpg', '1'),
(4, 'Delícias da Ana', 'ana@email.com', '987654321', 'Rua B, 456', 'Ana5678', 'foto2.jpg', '1'),
(5, 'Comida Caseira', 'caseira@email.com', '555123456', 'Rua C, 789', 'Caseira1', 'foto3.jpg', '1'),
(6, 'Sabores do Chef', 'chef@email.com', '444654321', 'Rua D, 101', 'Chef2023', 'foto4.jpg', '0'),
(7, 'Casa da Pizza', 'pizza@email.com', '333789456', 'Rua E, 202', 'Pizza456', 'foto5.jpg', '1'),
(8, 'Restaurante 50', 'restaurante50@email.com', '123450987', 'Rua Z, 500', 'Rest5050', 'foto50.jpg', '0'),
(9, 'Restaurante Gourmet', 'gourmet@email.com', '112233445', 'Rua G, 1001', 'Gour1234', 'foto51.jpg', '1'),
(10, 'Bistrô do Chef', 'bistro@chef.com', '998877665', 'Rua H, 2020', 'Bistr789', 'foto52.jpg', '0'),
(11, 'Comida Mineira', 'mineira@email.com', '123412341', 'Rua I, 3030', 'Mineira9', 'foto53.jpg', '1'),
(12, 'Pizzaria da Esquina', 'pizzaria@email.com', '876543210', 'Rua J, 4040', 'Pizza987', 'foto54.jpg', '1'),
(13, 'Hamburgueria Top', 'topburgers@email.com', '111222333', 'Rua K, 5050', 'Top12345', 'foto55.jpg', '1'),
(14, 'Sabores da Terra', 'sabores@email.com', '222333444', 'Rua L, 6060', 'Terra123', 'foto56.jpg', '0'),
(15, 'Restaurante da Família', 'familia@email.com', '333444555', 'Rua M, 7070', 'Fam45678', 'foto57.jpg', '1'),
(16, 'Churrascaria Fogo Vivo', 'churrasco@email.com', '444555666', 'Rua N, 8080', 'Churr123', 'foto58.jpg', '1'),
(17, 'Bar do Zé', 'bardoze@email.com', '555666777', 'Rua O, 9090', 'ZeBar123', 'foto59.jpg', '0'),
(18, 'Frutos do Mar', 'mariscos@email.com', '666777888', 'Rua P, 1010', 'FrutoMar', 'foto60.jpg', '1'),
(19, 'Vegano & Saudável', 'vegano@email.com', '777888999', 'Rua Q, 1111', 'VegSaud', 'foto61.jpg', '1'),
(20, 'Delícias do Campo', 'campo@email.com', '888999000', 'Rua R, 1212', 'DelCampo', 'foto62.jpg', '1'),
(21, 'Café Colonial', 'cafe@email.com', '999000111', 'Rua S, 1313', 'Cafe2023', 'foto63.jpg', '1'),
(22, 'Doces e Companhia', 'doces@email.com', '111222333', 'Rua T, 1414', 'Doce1234', 'foto64.jpg', '1'),
(23, 'Burgers Artesanais', 'artesanal@email.com', '222333444', 'Rua U, 1515', 'BurgArt', 'foto65.jpg', '1'),
(24, 'Massas Italianas', 'massas@email.com', '333444555', 'Rua V, 1616', 'MassIta', 'foto66.jpg', '1'),
(25, 'Sorveteria Geladinho', 'geladinho@email.com', '444555666', 'Rua W, 1717', 'Gelado12', 'foto67.jpg', '1'),
(26, 'Petiscos do Bar', 'petiscos@email.com', '555666777', 'Rua X, 1818', 'Petisc34', 'foto68.jpg', '1'),
(27, 'Cantina da Mama', 'mama@email.com', '666777888', 'Rua Y, 1919', 'Mama2023', 'foto69.jpg', '1'),
(28, 'Restaurante Final', 'final@email.com', '777888999', 'Rua Z, 2020', 'Final999', 'foto70.jpg', '0');

--
-- Índices para tabelas despejadas
--

--
-- Índices de tabela `avaliacao`
--
ALTER TABLE `avaliacao`
  ADD PRIMARY KEY (`id`),
  ADD KEY `FK_avaliacao_2` (`fk_prato_id`),
  ADD KEY `FK_avaliacao_3` (`fk_cliente_id`);

--
-- Índices de tabela `cliente`
--
ALTER TABLE `cliente`
  ADD PRIMARY KEY (`id`);

--
-- Índices de tabela `prato`
--
ALTER TABLE `prato`
  ADD PRIMARY KEY (`id`),
  ADD KEY `FK_prato_2` (`fk_restaurante_id`);

--
-- Índices de tabela `restaurante`
--
ALTER TABLE `restaurante`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT para tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `avaliacao`
--
ALTER TABLE `avaliacao`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=107;

--
-- AUTO_INCREMENT de tabela `cliente`
--
ALTER TABLE `cliente`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=34;

--
-- AUTO_INCREMENT de tabela `prato`
--
ALTER TABLE `prato`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=65;

--
-- AUTO_INCREMENT de tabela `restaurante`
--
ALTER TABLE `restaurante`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- Restrições para tabelas despejadas
--

--
-- Restrições para tabelas `avaliacao`
--
ALTER TABLE `avaliacao`
  ADD CONSTRAINT `FK_avaliacao_2` FOREIGN KEY (`fk_prato_id`) REFERENCES `prato` (`id`) ON DELETE SET NULL,
  ADD CONSTRAINT `FK_avaliacao_3` FOREIGN KEY (`fk_cliente_id`) REFERENCES `cliente` (`id`) ON DELETE SET NULL;

--
-- Restrições para tabelas `prato`
--
ALTER TABLE `prato`
  ADD CONSTRAINT `FK_prato_2` FOREIGN KEY (`fk_restaurante_id`) REFERENCES `restaurante` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
