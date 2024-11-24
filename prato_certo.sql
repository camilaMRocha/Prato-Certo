-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 25/11/2024 às 00:24
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
-- Estrutura para tabela `avalia`
--

CREATE TABLE `avalia` (
  `id` int(11) NOT NULL,
  `FK_cliente_id` int(11) NOT NULL,
  `FK_prato_id` int(11) NOT NULL,
  `comentario` text NOT NULL,
  `nota` int(1) DEFAULT NULL CHECK (`nota` between 1 and 5)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura para tabela `cliente`
--

CREATE TABLE `cliente` (
  `id` int(11) NOT NULL,
  `nome` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `senha` varchar(255) NOT NULL,
  `tipo` tinyint(1) NOT NULL,
  `telefone` varchar(20) DEFAULT NULL,
  `rua` varchar(100) DEFAULT NULL,
  `foto` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `cliente`
--

INSERT INTO `cliente` (`id`, `nome`, `email`, `senha`, `tipo`, `telefone`, `rua`, `foto`) VALUES
(16, 'franken', 'frank@gmail.com', '1234frank', 0, NULL, NULL, 'C:\\Users\\Alima\\Desktop\\Etec\\4°Bimestre\\00trabalho ds\\Prato-Certo\\base\\bin\\Debug\\Fotos\\8ba7081c-3217-4a3e-b0dd-8aae7cce48de.png'),
(17, 'rukia kuchiki', 'rukia@gmail.com', '1234rukia', 0, NULL, NULL, 'C:\\Users\\Alima\\Desktop\\Etec\\4°Bimestre\\00trabalho ds\\Prato-Certo\\base\\bin\\Debug\\Fotos\\5a62187a-358b-4657-b940-fcdfa0e80871.jfif'),
(18, 'jessica', 'jessica@gmail.com', '1234jessica', 0, NULL, NULL, 'C:\\Users\\Alima\\Desktop\\Etec\\4°Bimestre\\00trabalho ds\\Prato-Certo\\base\\bin\\Debug\\Fotos\\df9bb70b-2552-4091-8495-804dd07b899c.png'),
(19, 'dicas e promos', 'diquinhas@gmail.com', '1234dicas', 1, '1959563565', 'instagram', 'C:\\Users\\Alima\\Desktop\\Etec\\4°Bimestre\\00trabalho ds\\Prato-Certo\\base\\bin\\Debug\\Fotos\\ab15fd69-d0d5-444b-acbe-4d27387af524.png');

-- --------------------------------------------------------

--
-- Estrutura para tabela `prato`
--

CREATE TABLE `prato` (
  `id` int(11) NOT NULL,
  `nome` varchar(100) NOT NULL,
  `preco` decimal(10,2) NOT NULL,
  `descricao` text NOT NULL,
  `FK_restaurante_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Índices para tabelas despejadas
--

--
-- Índices de tabela `avalia`
--
ALTER TABLE `avalia`
  ADD PRIMARY KEY (`id`),
  ADD KEY `FK_cliente_id` (`FK_cliente_id`),
  ADD KEY `FK_prato_id` (`FK_prato_id`);

--
-- Índices de tabela `cliente`
--
ALTER TABLE `cliente`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `email` (`email`);

--
-- Índices de tabela `prato`
--
ALTER TABLE `prato`
  ADD PRIMARY KEY (`id`),
  ADD KEY `FK_restaurante_id` (`FK_restaurante_id`);

--
-- AUTO_INCREMENT para tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `avalia`
--
ALTER TABLE `avalia`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de tabela `cliente`
--
ALTER TABLE `cliente`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT de tabela `prato`
--
ALTER TABLE `prato`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Restrições para tabelas despejadas
--

--
-- Restrições para tabelas `avalia`
--
ALTER TABLE `avalia`
  ADD CONSTRAINT `avalia_ibfk_1` FOREIGN KEY (`FK_cliente_id`) REFERENCES `cliente` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `avalia_ibfk_2` FOREIGN KEY (`FK_prato_id`) REFERENCES `prato` (`id`) ON DELETE CASCADE;

--
-- Restrições para tabelas `prato`
--
ALTER TABLE `prato`
  ADD CONSTRAINT `prato_ibfk_1` FOREIGN KEY (`FK_restaurante_id`) REFERENCES `cliente` (`id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
