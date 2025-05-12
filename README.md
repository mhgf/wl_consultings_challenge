# Meu Projeto

O projeto é um desafio para desenvolver
uma API para gerenciar carteiras digitais e transações financeiras.

## Como Rodar

### Docker

* ``` docker compose up -d ```
* Basta acessar o http://localhost:4001/scalar/

### Local

* Radar um banco Postgres
* Rodar o restore dos pacotes <br> ``` dotnet restore ```
* Adicionar a conexão com o banco no appsettings(.net secrets)
* Rodar as migrations <br>
  ```dotnet ef database update --project WlChallenge.Infra/WlChallenge.Infra.csproj --startup-project WlChallenge.Api/WlChallenge.Api.csproj --context WlChallenge.Infra.Data.AppDbContext```
* E executar o comando <br>
  ```dotnet run --project WlChallenge.Api/WlChallenge.Api.csproj --launch-profile https```
* O projeto ficará disponivel em https://localhost:7023

### Dados

O projeto assim que é inicia ele verifica se tem dados e caso não tenha ele cria 10 novos usuários.

* Email: user(**1-10**)@email.com. *Exemplo: user5@email.com* 
* Senha: 123456789012 para todos

