
# 🥩 Sistema de Cadastro de Carnes - Clean Architecture

Bem-vindo ao repositório do **Sistema de Cadastro de Carnes**, uma aplicação full stack desenvolvida com foco em arquitetura limpa (Clean Architecture), utilizando **.NET 8** no backend (Web API) e **ASP.NET Razor MVC** no frontend.

Este projeto foi desenvolvido com o objetivo de ser didático, modular e fácil de manter, atendendo a um cenário típico de cadastro e gerenciamento de **carnes**, **compradores** e **pedidos**.

---

## 📌 Índice

1. [Arquitetura](#arquitetura)
2. [Funcionalidades](#funcionalidades)
3. [Tecnologias Utilizadas](#tecnologias-utilizadas)
4. [Como Rodar Localmente](#como-rodar-localmente)
5. [Banco de Dados](#banco-de-dados)
6. [Autor](#autor)

---

## 🏗 Arquitetura

O projeto adota o padrão **Clean Architecture**, organizado nas seguintes camadas:

- **Domain (Model):** Contém as entidades e regras de domínio.
- **Application (Service):** Contém interfaces, DTOs e a lógica de negócio.
- **Infrastructure (Data):** Repositórios e contexto do banco de dados usando Entity Framework Core.
- **Presentation (WebUI & WebAPI):** Interface com o usuário via Razor Pages e consumo da API via HttpClient.

---

## ✅ Funcionalidades

### 🔹 Carnes
- CRUD de carnes com validação de origem.
- Validação de exclusão apenas se não houver pedidos vinculados.

### 🔹 Compradores
- Cadastro com Nome, Documento (CPF/CNPJ), Cidade e Estado.
- Exclusão somente se não houver pedidos associados.

### 🔹 Pedidos
- Pedido com data, comprador, e múltiplos itens.
- Seleção de carne, preço e moeda para cada item.
- Conversão automática de moeda com integração à API externa (AwesomeAPI).

---

## 💻 Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/)
- ASP.NET Core Razor Pages
- ASP.NET Web API
- Entity Framework Core
- SQL Server
- SweetAlert2 (feedbacks visuais)
- Bootstrap 5

---

## 🚀 Como Rodar Localmente

### 🔧 Pré-requisitos

- Visual Studio 2022 ou superior
- .NET SDK 8.0+
- SQL Server LocalDB ou equivalente
- Git

### 📥 Clonando o projeto

```bash
git clone https://github.com/seu-usuario/CadastroCarnes.git
cd CadastroCarnes
```

### ⚙️ Configurando o banco de dados

1. Crie o banco de dados com o script em `Documentacao/Script_Banco.sql` ou copie do `README`.
2. Atualize a `connectionString` no arquivo `appsettings.json` da WebAPI e do WebUI:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=CadastroCarnes;Trusted_Connection=True;"
}
```

### ▶️ Rodando o projeto

Abra o `CadastroCarnes.sln` no Visual Studio e defina o projeto **WebUI** como startup. Pressione **F5** ou **Ctrl + F5** para rodar o sistema no navegador.

A API será consumida automaticamente pelo frontend Razor.

---

## 🗄 Banco de Dados

A estrutura do banco inclui as tabelas:

- `Cidade`
- `Origem`
- `Moeda`
- `Carne`
- `Comprador`
- `Pedido`
- `ItemPedido`

### Script DDL + DML

```sql
CREATE DATABASE CadastroCarnes;
GO
USE CadastroCarnes;
GO
CREATE TABLE Cidade (
 Id INT PRIMARY KEY IDENTITY(1,1),
 Nome NVARCHAR(100),
 Estado NVARCHAR(2)
);

-- Demais tabelas no PDF ou script completo
```

Inserts iniciais já incluem cidades, origens e moedas.

## 👨‍💻 Autor

Desenvolvido por **Alan** – Analista de Sistemas / Desenvolvedor Full Stack
