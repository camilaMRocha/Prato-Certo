-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 01/12/2024 às 17:11
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
(1, 'monica', 'monica@gmail.com', '1234monica', 'C:\\Users\\Alima\\Desktop\\Etec\\4°Bimestre\\00trabalho ds\\Prato-Certo\\base\\bin\\Debug\\Fotos\\83d7d3a1-7ee8-4b79-9c44-f71afce89d30.jpg');

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
(1, 5, 36, 'Feijoada tradicional com arroz, farofa e couve', 'feijoada.jpg', 'Feijoada', 2),
(2, 5, 45, 'Churrasco de picanha com vinagrete e maionese', 'churrasco.jpg', 'Churrasco', 2),
(3, 5, 30, 'Pizza com molho de tomate, mozzarella, manjericão e azeite', 'pizza_margherita.jpg', 'Pizza Margherita', 1),
(4, 5, 25, 'Hambúrguer artesanal com queijo cheddar e bacon', 'burguer_artesanal.jpg', 'Burguer Artesanal', 1),
(5, 5, 50, 'Sushi com variedades de peixe fresco e arroz de sushi', 'sushi_variado.jpg', 'Sushi Variado', 1),
(6, 5, 2, 'carne bovina, salada, tomate, queijo e bacon', 'C:\\Users\\Alima\\Desktop\\Etec\\4°Bimestre\\00trabalho', 'hambuerguer', 1),
(7, 7, 4555, 'ghghfh', 'C:\\Users\\Alima\\Desktop\\Etec\\4°Bimestre\\00trabalho ds\\Prato-Certo\\base\\bin\\Debug\\Fotos\\bc2acabc-8956-4646-9353-5a4a9d50eb6f.png', 'ghbcgh', 1),
(8, 3, 777, 'ghtfhtyh', 'C:\\Users\\Alima\\Desktop\\Etec\\4°Bimestre\\00trabalho ds\\Prato-Certo\\base\\bin\\Debug\\Fotos\\d890a561-0787-472a-b0b0-aebc7d573c25.png', 'fgfgfg', 1),
(9, 3, 8, 'baunilha, leite e etc', 'foto.jpg', 'sorvete baunilhaa', 1),
(10, 4, 150, 'pato assado no forno com tempero', 'C:\\Users\\Alima\\Desktop\\Etec\\4°Bimestre\\00trabalho ds\\Prato-Certo\\base\\bin\\Debug\\Fotos\\1e230e6a-a871-4b2c-b011-26ea577df185.png', 'pato assado', 2),
(11, 5, 6, 'frango, farinha, óleo e entre outros', 'C:\\Users\\Alima\\Desktop\\Etec\\4°Bimestre\\00trabalho ds\\Prato-Certo\\base\\bin\\Debug\\FotosPratos\\8c29e717-c3c1-440b-aaf8-f04fe4dc36c3.png', 'coxinha', 1);

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
(2, 'gogo', 'gogo@gmail.com', '19546565', 'sodjksd', '1234gogo', NULL, '1');

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
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de tabela `cliente`
--
ALTER TABLE `cliente`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de tabela `prato`
--
ALTER TABLE `prato`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de tabela `restaurante`
--
ALTER TABLE `restaurante`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

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

