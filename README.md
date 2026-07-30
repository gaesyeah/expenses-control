# ExpensesControl

Sistema de controle de gastos residenciais.

Permite o cadastro de pessoas e transações financeiras (receitas/despesas), com consulta de totais individuais e gerais.

## Tecnologias

- **.NET 10** + **C#** (Minimal APIs)
- **Entity Framework Core** + **PostgreSQL**
- **Docker Compose**
- **Scalar** para documentação interativa da API (OpenAPI)

## Regras de negócio implementadas

- Cada pessoa possui identificador único gerado automaticamente (`Guid`).
- Ao deletar uma pessoa, todas as suas transações são apagadas automaticamente (cascade delete).
- Pessoas menores de 18 anos só podem ter **despesas** cadastradas (não receitas).
- Toda transação exige uma pessoa já cadastrada, validada pelo identificador informado.
- A consulta de totais lista o total de receitas, despesas e saldo por pessoa, além do total geral.

## Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/)
- Docker Compose

## Como rodar o projeto

1. Clone o repositório:

   ```bash
   git clone https://github.com/gaesyeah/expenses-control-back.git
   cd expenses-control-back
   ```

2. Restaure as dependências:

   ```bash
   dotnet restore
   ```

3. Inicie o banco de dados:

   ```bash
   docker compose up -d
   ```

4. Execute a aplicação:

   ```bash
   dotnet watch
   ```

O banco de dados PostgreSQL é executado em um container Docker.

As migrations do Entity Framework Core são aplicadas **automaticamente** na inicialização da aplicação, não sendo necessário executar comandos como `dotnet ef migrations add` ou `dotnet ef database update` para preparar o banco.

Os dados persistem em um volume Docker (`expenses-control-data`), permanecendo disponíveis mesmo após parar ou reiniciar o container.

## Configuração

O projeto utiliza o sistema de configuração padrão do ASP.NET Core.

- Em **desenvolvimento**, a conexão com o banco é configurada em `appsettings.Development.json`.
- Em **produção**, a connection string é fornecida por variável de ambiente (`ConnectionStrings__DefaultConnection`), sem necessidade de alterar o código.

## Documentação da API

A documentação interativa é gerada via [Scalar](https://scalar.com), a partir da especificação OpenAPI da própria aplicação. Por ela é possível visualizar todos os endpoints, seus parâmetros, schemas de request/response e também **testar as requisições diretamente pelo navegador**.

### Rodando localmente

Com a aplicação em execução (`dotnet watch`), acesse:

```
http://localhost:5228/scalar/v1
```

### Versão publicada (sem precisar rodar localmente)

```
https://expenses-control-api.onrender.com/scalar/v1
```

> Hospedado no plano gratuito do Render. Caso o serviço esteja inativo há algum tempo, o primeiro acesso pode levar cerca de 1 minuto (cold start). Além disso, por estar no plano gratuito, o serviço pode ficar temporariamente indisponível.

## Endpoints principais

| Método   | Rota           | Descrição                            |
| -------- | -------------- | ------------------------------------ |
| `POST`   | `/person`      | Cadastra uma pessoa                  |
| `GET`    | `/person`      | Lista todas as pessoas               |
| `GET`    | `/person/{id}` | Busca uma pessoa por id              |
| `PATCH`  | `/person/{id}` | Atualiza parcialmente uma pessoa     |
| `DELETE` | `/person/{id}` | Remove uma pessoa e suas transações  |
| `POST`   | `/transaction` | Cadastra uma transação               |
| `GET`    | `/transaction` | Lista todas as transações            |
| `GET`    | `/totals`      | Consulta totais por pessoa e o geral |

Para detalhes completos de cada endpoint (schemas, exemplos e testes ao vivo), consulte a documentação via Scalar.

## Tipos de transação

O campo `type`, usado no cadastro de transações, aceita os seguintes valores:

| Valor | Tipo                |
| ----- | ------------------- |
| `1`   | Despesa (`Expense`) |
| `2`   | Receita (`Income`)  |
