/* Lógico_PRATOCERTO: */

CREATE TABLE restaurante (
    id INTEGER AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100),
    email VARCHAR(200),
    telefone VARCHAR(20),
    rua VARCHAR(200),
    senha INTEGER(11),
    foto VARCHAR(200),
    status VARCHAR(20)
);

CREATE TABLE prato (
    id INTEGER(11) AUTO_INCREMENT PRIMARY KEY,
    media_nota INTEGER,
    preco INTEGER,
    descricao VARCHAR(200),
    foto VARCHAR(200),
    nome VARCHAR(200),
    fk_restaurante_id INTEGER
);

CREATE TABLE cliente (
    id INTEGER(11) AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(200),
    email VARCHAR(200),
    senha VARCHAR(100),
    foto VARCHAR(200)
);

CREATE TABLE avaliacao (
    fk_prato_id INTEGER(11),
    fk_cliente_id INTEGER(11),
    id INTEGER(11) AUTO_INCREMENT PRIMARY KEY,
    data INTEGER,
    nota INTEGER,
    comentario VARCHAR(200)
);
 
ALTER TABLE prato ADD CONSTRAINT FK_prato_2
    FOREIGN KEY (fk_restaurante_id)
    REFERENCES restaurante (id)
    ON DELETE RESTRICT;
 
ALTER TABLE avaliacao ADD CONSTRAINT FK_avaliacao_2
    FOREIGN KEY (fk_prato_id)
    REFERENCES prato (id)
    ON DELETE SET NULL;
 
ALTER TABLE avaliacao ADD CONSTRAINT FK_avaliacao_3
    FOREIGN KEY (fk_cliente_id)
    REFERENCES cliente (id)
    ON DELETE SET NULL;
