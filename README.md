<center>
  <p align="center">
    &nbsp;
    <img src="https://user-images.githubusercontent.com/20674439/158480674-3b8895e7-420e-4025-bd78-8058ba255476.png"  width="150" />
  </p>  
  <h1 align="center">🚀 API Produtos</h1>
  <p align="center">
    API para realizar o cadastro de Produtos<br />
  </p>
</center>
<br />

## Ferramentas necessárias

- Visual Studio 2022 ou Visual Studio Code
- SDK do .NET 8 instalado
- Docker e Docker Compose


## Como executar?

- Clone o repositório:
```sh
git clone https://github.com/jlcoelho/api-produtos.git
```
- Entre na paste e rode o comando do Docker Compose
```sh
docker compose up -d

```
- Caso esteja pelo Visual Studio Code rode o comando via CLI
```sh
dotnet run --project src/Wake.Commerce.Api
```

---

> ## Design Patterns
* Repository
* Dependency Injection
* UnitOfWork
* Mediator


> ## Metodologias e Design
* Clean Architecture
* DDD
* Conventional Commits
* Use Cases

> ## Bibliotecas e Ferramentas
* MediatR
* MediatR.Extensions.Microsoft.DependencyInjection
* Microsoft.EntityFrameworkCore.SqlServer
* FluentValidation
* Microsoft.EntityFrameworkCore.Relational
* Bogus
* FluentAssertions
* Moq
* xunit
* Microsoft.EntityFrameworkCore.InMemory