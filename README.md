# Prato-Certo

> **Aviso:** Este projeto está em desenvolvimento. Algumas funcionalidades podem estar incompletas ou sujeitas a mudanças.

O **Prato-Certo** é um aplicativo projetado para coletar e analisar avaliações de clientes sobre pratos e serviços gastronômicos. Ele oferece aos estabelecimentos uma visão detalhada sobre o desempenho dos seus pratos e serviços, ajudando na melhoria contínua da qualidade com base na opinião dos consumidores. Além disso, o sistema auxilia os clientes na tomada de decisões, permitindo que vejam os melhores e piores pratos conforme as avaliações de outros usuários.

![Status do Projeto](https://img.shields.io/badge/status-em--desenvolvimento-yellow)
![Versão](https://img.shields.io/badge/versão-0.1.0-orange)

## Tabela de Conteúdos
- [Sobre o Projeto]
- [Funcionalidades]
- [Requisitos do Projeto]
  - Requisitos Funcionais
  - Requisitos Não Funcionais
- [Configuração do Ambiente]
  - Pré-Requisitos
  - Como Importar o Projeto
  - Configuração do Banco de Dados
  - Repositório de Imagens
- [Como Usar]
  - Executando o Sistema
  - Login Inicial e Dados de Teste
- [Estrutura do Código]
- [Histórias de Usuário]
- [Desenvolvedores]




## Sobre o Projeto
O projeto foi desenvolvido para atender à crescente demanda por feedbacks precisos e relevantes na indústria gastronômica. Com o **Feedback Gastronômico**, restaurantes podem aprimorar seus produtos e serviços com base em dados reais, enquanto clientes obtêm insights confiáveis para escolher os melhores pratos.

## Funcionalidades
- **Avaliação de Pratos**: Clientes podem avaliar pratos com notas e comentários.
- **Listagem de Melhores Pratos**: Baseada nas avaliações de outros clientes.
- **Relatórios para Gerentes**: Visualização detalhada do desempenho dos pratos e serviços.
- **Perfil de Usuário e Gerente**: Permite alterações e personalizações.
- **Painel Administrativo para Estabelecimento**: Gerenciamento do prato.
## Requisitos
 ## Requisitos Não-Funcionais
- **Compatibilidade**: Adaptar o design para diferentes tamanhos.
- **Usabilidade**: Criação de uma interface limpa.
- **Segurança**: Verificação de campos vazios e formato de e-mail.
- **Backup**: Backup automático.
- **Escalabilidade**: Permitir adicionar novas avaliações sem comprometer o desempenho.
- **Desempenho**: O sistema deve carregar as avaliações rapidamente.
- **Armazenamento de Imagens**: Uso eficiente do espaço para armazenar e recuperar imagens.
  
 ## Requisitos Funcionais
- **Cadastro**: Formulário para inserir nome, e-mail e senha.
- **Avaliação**: Avaliação dos pratos do restaurante escolhido.
- **Listagem de Avaliação**: Filtrar os pratos por melhores e piores avaliações.
- **Detalhes do Prato**: Exibir informações como Ingredientes e média de avaliação.

## Configuração do Ambiente 
 ## Pré-Requisitos 
 
 **Certifique-se de ter instalado**:
 - MySQL Server
 - Visual Studio com suporte ao .NET Framework
 - MySQL Connector/NET

 ## Como Importar o Projeto
 1. Clone o Repositório:
    git clone                             https://github.com/usuario/Prato-     Certo.git
 2. Abra o Projeto no Visual Studio.
 3. Certifique-se de que as             dependências (MySQL Connector e      Microsoft.Office.Interop.Excel)       estejam configuradas via NuGet.

 ## Configuração Do Banco de Dados
 1. Crie o Banco de Dados:
    **CREATE DATABASE prato_certo**
 2. Importe o arquivo                     **prato_certo.sql** localizado.       na pasta /database:
    Inclui as tabelas: prato,             restaurante,usuario,entre outras.
 3. (Opcional) Insira dados de            exemplo usando os scripts na          pasta /database/seeds.

  ## Repositório de Imagens 
  1. Crie uma pasta no servidor local      para armazenar imagens:
     C:\PratoCerto\Imagens
  2. No código, configure o caminho       para essa pasta no acesso às          imagens:
    string caminhoImage=
     @"C:\PratoCerto\Imagens".
  4. Certifique-se de que o programa    tenha permissões para                 leitura/escrita nesse diretório.

## Como Usar
 ## Executando o Sistema
 1. No Visual Studio, compile o projeto (pressione F5).
 2. O sistema será iniciado em ambiente local.

 ## Login Inicial e Dados de Teste
 Usuário Administrador:
 Login: admin@pratocerto.com
 Senha: admin123

 Usuário Cliente (Exemplo):
 Login: cliente@pratocerto.com
 Senha: cliente123


## Estrutura do Código 
 •/src: Contém os arquivos principais do sistema.
 
   °/Forms: Telas e formulários do sistema.
   
   °/Controllers: Controle da lógica de negócio.
   
   °/Models: Modelos e classes relacionadas ao banco de dados.

 •/database: Scripts SQL para criar e popular o banco de dados.
 •/docs: Documentação adicional e imagens.
 •/assets: Imagens e outros arquivos usados no projeto.


 
## Histórias de Usuário

1. **Usuário Comum**  
   *Como usuário, quero saber quais melhores pratos do programa.*  

   
2. **Usuário Comum**  
   *Como usuário, quero comentar sobre o prato no aplicativo, para avaliar o sabor da comida.*  
   
   
3. **Gerente do Restaurante**  
   *Como restaurante quero ver quais pratos eu tenho.*  
   
4. **Usuário Comum e Gerente do Restaurante**  
   *Como usuário, quero alterar meu perfil; Como gerente quero alterar meu perfil.*  
    

5. **Usuário Comum**  
   *Como usuário, quero poder filtrar as avaliações dos pratos por nota, para ver primeiro as melhores ou as piores avaliações.*  
 

6. **Usuário Comum**  
   *Como usuário, quero ver os detalhes dos pratos antes de escolher, como ingredientes e tipo de cozinha, para poder decidir o que mais me agrada.*  


7. **Usuário Comum**  
   *Como usuário, quero compartilhar minha avaliação nas redes sociais para que meus amigos também possam saber sobre minha experiência com o prato.*  
    

8. **Gerente do Restaurante**  
   *Como gerente, quero postar meus pratos na página, para melhorar meus pratos com as avaliações.*  
  

## Desenvolvedores

| Nome        | Responsabilidades |
|-------------|-------------------|
| **Camila de M. Rocha**      |  histórias de usuário 1 e 3. |
| **Joyce C. da Silva**       | istórias de usuário 2 e 7. |
| **Ana Laura M. de Almeida**   |  histórias de usuário 4 e 6. |
| **Guilherme Pereira**   | histórias de usuário 5 e 8. |

---

