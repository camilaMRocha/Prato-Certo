-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 27/11/2024 às 20:41
-- Versão do servidor: 10.4.32-MariaDB
-- Versão do PHP: 8.0.30

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
(1, 1, 1, 1732736013, 4, 'Gostei muito do prato!'),
(1, 1, 2, 1732736084, 3, 'Bom, mas pode melhorar!'),
(1, 1, 3, 1732736189, 3, 'Bom, mas pode melhorar!');

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
(1, 'Cliente Teste', 'cliente@teste.com', 'senha123', 'cliente.jpg');

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
(1, 3, 30, 'Prato Teste de Avaliação', 'prato.jpg', 'Prato Teste', 1);

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
  `senha` int(11) DEFAULT NULL,
  `foto` varchar(200) DEFAULT NULL,
  `status` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `restaurante`
--

INSERT INTO `restaurante` (`id`, `nome`, `email`, `telefone`, `rua`, `senha`, `foto`, `status`) VALUES
(1, 'Restaurante Teste', 'teste@restaurante.com', '1234567890', 'Rua Teste', 1234, 'foto.jpg', 'Ativo');

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
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de tabela `cliente`
--
ALTER TABLE `cliente`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de tabela `prato`
--
ALTER TABLE `prato`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de tabela `restaurante`
--
ALTER TABLE `restaurante`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

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
