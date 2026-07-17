# ExpensesControl

Sistema de controle de gastos residenciais, desenvolvido como desafio técnico de estágio em TI (Desenvolvimento).

Permite o cadastro de pessoas e transações financeiras (receitas/despesas), com consulta de totais individuais e gerais.

## Tecnologias

- **.NET 10** + **C#** (Minimal APIs)
- **Entity Framework Core** + **SQLite**
- **Scalar** para documentação interativa da API (OpenAPI)

## Regras de negócio implementadas

- Cada pessoa possui identificador único gerado automaticamente (`Guid`).
- Ao deletar uma pessoa, todas as suas transações são apagadas automaticamente (cascade delete).
- Pessoas menores de 18 anos só podem ter **despesas** cadastradas (não receitas).
- Toda transação exige uma pessoa já cadastrada, validada pelo identificador informado.
- A consulta de totais lista o total de receitas, despesas e saldo por pessoa, além do total geral.

## Segurança

O EF Core Sqlite traz transitivamente uma versão vulnerável do
`SQLitePCLRaw.lib.e_sqlite3` ([CVE-2025-6965](https://github.com/dotnet/efcore/issues/38257)).
Enquanto não há correção oficial da própria EF Core para essa dependência, o
`.csproj` fixa manualmente uma versão corrigida do pacote.

## Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

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

3. Rode a aplicação:
   ```bash
   dotnet watch
   ```

O banco de dados SQLite é criado e as migrations do Entity Framework Core são
aplicadas **automaticamente** na inicialização da aplicação, não sendo necessário
rodar nenhum comando manual (`dotnet ef`, `dotnet ef database update`, etc.).

Os dados persistem em um arquivo `.sqlite` local, mesmo após fechar a aplicação.

## Documentação da API

A documentação interativa é gerada via [Scalar](https://scalar.com), a partir
da especificação OpenAPI da própria aplicação. Por ela é possível visualizar
todos os endpoints, seus parâmetros, schemas de request/response, e também
**testar as requisições diretamente pelo navegador**.

### Rodando localmente

Com a aplicação em execução (`dotnet watch`), acesse:

```
http://localhost:5228/scalar/v1
```

### Versão publicada (sem precisar rodar localmente)

```
https://expenses-control-api.onrender.com/scalar/v1
```

> Hospedado no plano gratuito do Render. Caso o serviço esteja inativo há um
> tempo, o primeiro acesso pode levar até ~1 minuto (cold start). Além disso,
> por estar no plano gratuito, pode ser que o serviço esteja temporariamente
> indisponível. Caso isso aconteça, fico à disposição para reativá-lo, é só
> entrar em contato comigo.

## Endpoints principais

| Método   | Rota           | Descrição                                  |
| -------- | -------------- | ------------------------------------------ |
| `POST`   | `/person`      | Cadastra uma pessoa                        |
| `GET`    | `/person`      | Lista todas as pessoas                     |
| `GET`    | `/person/{id}` | Busca uma pessoa por id                    |
| `PATCH`  | `/person/{id}` | Atualiza parcialmente uma pessoa           |
| `DELETE` | `/person/{id}` | Remove uma pessoa (e suas transações)      |
| `POST`   | `/transaction` | Cadastra uma transação                     |
| `GET`    | `/transaction` | Lista todas as transações                  |
| `GET`    | `/totals`      | Consulta totais por pessoa e o total geral |

Para detalhes completos de cada endpoint (schemas, exemplos, testes ao vivo),
consulte a documentação via Scalar.
