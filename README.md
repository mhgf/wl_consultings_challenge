# 💳 Carteiras Digitais API

Este projeto é um desafio para desenvolver uma **API RESTful** com foco em **carteiras digitais** e **transações
financeiras**. O sistema permite o gerenciamento de usuários e transações, com persistência de dados em banco
PostgreSQL.

---

## 📦 Tecnologias Utilizadas

- [.NET 9](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://learn.microsoft.com/ef/core/)
- [PostgreSQL](https://www.postgresql.org/)
- [Docker](https://www.docker.com/)
- [Scalar UI](https://scalar.com/)

---

## 🚀 Como Rodar o Projeto

### ▶️ Com Docker

1. Execute o comando:
   ```bash
   docker compose up -d
   ```
2. Acesse a interface do Swagger em:  
   [http://localhost:4001/scalar/](http://localhost:4001/scalar/)

---

### 🛠️ Ambiente Local (sem Docker)

1. Suba um banco de dados PostgreSQL local
2. Restaure os pacotes do projeto:
   ```bash
   dotnet restore
   ```
3. Configure a string de conexão no `appsettings.json` ou via **.NET Secrets**:
   ```bash
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Database=wl_challenge;Username=seu_usuario;Password=sua_senha"
   ```
4. Execute as migrations:
   ```bash
   dotnet ef database update \
     --project WlChallenge.Infra/WlChallenge.Infra.csproj \
     --startup-project WlChallenge.Api/WlChallenge.Api.csproj \
     --context WlChallenge.Infra.Data.AppDbContext
   ```
5. Rode o projeto:
   ```bash
   dotnet run --project WlChallenge.Api/WlChallenge.Api.csproj --launch-profile https
   ```
6. Acesse a aplicação em:  
   [https://localhost:7023](https://localhost:7023)

---

## 👥 Dados de Teste

Ao iniciar, a API verifica se existem dados no banco. Caso contrário, ela cria automaticamente **10 usuários** de teste
com as seguintes credenciais:

- **Emails:** `user1@email.com` até `user10@email.com`
- **Senha:** `123456789012`

---

## 🧪 Testes

Para rodar os testes (caso existam):

```bash
dotnet test
```

---

## 📁 Estrutura do Projeto

```
WlChallenge/
├── WlChallenge.Api/           # Projeto principal da API
├── WlChallenge.Application/   # Regras de negócio
├── WlChallenge.Domain/        # Entidades e interfaces
├── WlChallenge.Infra/         # Persistência de dados
├── Tests/         # Testes unitários 
│   └── WlChallenge.Application.Test/
│   └── WlChallenge.Domain.Test/
└── docker-compose.yml         # Configuração para containers
```

---

## ✍️ Autor

Desenvolvido por **Matheus Ferreira**  
Sinta-se à vontade para contribuir ou enviar sugestões!