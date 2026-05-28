# CrmApi — Project Context for Claude Code

## Project Overview

A CRM (Customer Relationship Management) REST API built with .NET 10, designed as a portfolio project. The API manages contacts, companies, and contracts with status workflows.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | .NET 10 — ASP.NET Core Minimal API |
| ORM | Entity Framework Core |
| Database (Dev) | SQL Server via Docker |
| Database (Prod) | SQL Server on Azure |
| Auth | JWT Bearer |
| Validation | FluentValidation |
| Mapping | Mapster |
| Docs | Scalar |
| Containers | Docker + Docker Compose |
| CI/CD | GitHub Actions → Docker Hub → Azure App Service |
| Deploy | Azure App Service |

---

## Architecture

```
Endpoints → DTOs → Services → Repository → Database
```

- **No CQRS** — not necessary for this scope
- **No Unit of Work** — EF Core's DbContext already implements it natively
- **Minimal API** — no Controllers, routes mapped directly in Endpoint classes
- **Generic Repository** — `Repository<TEntity>` base with CRUD; specific repos extend it

---

## Project Structure

```
CrmApi/
├── CrmApi.slnx
└── CrmApi.Api/
    ├── Api/
    │   ├── Endpoints/
    │   │   ├── AuthEndpoints.cs
    │   │   ├── CompanyEndpoints.cs
    │   │   ├── ContactEndpoints.cs
    │   │   └── ContractEndpoints.cs
    │   └── Exceptions/
    │       ├── NotFoundException.cs
    │       ├── BusinessRuleException.cs
    │       └── GlobalExceptionHandler.cs
    ├── Domain/
    │   ├── Entities/
    │   │   ├── Base.cs                  # abstract — Id + CreatedAt
    │   │   ├── CompanyEntity.cs
    │   │   ├── ContactEntity.cs
    │   │   ├── ContractEntity.cs
    │   │   └── UserEntity.cs
    │   └── Enums/
    │       ├── EContractStatus.cs
    │       └── EUserRole.cs
    ├── Infrastructure/
    │   ├── Auth/
    │   │   ├── Configurations/
    │   │   │   └── TokenConfiguration.cs
    │   │   └── Services/
    │   │       ├── Contract/            # interfaces
    │   │       │   ├── IPasswordHasherService.cs
    │   │       │   └── ITokenService.cs
    │   │       └── Implementation/
    │   │           ├── PasswordHasherService.cs
    │   │           └── TokenService.cs
    │   ├── Configuration/
    │   │   ├── AuthConfiguration.cs
    │   │   ├── DatabaseConfiguration.cs
    │   │   └── ScalarConfiguration.cs
    │   ├── Data/
    │   │   ├── Database/
    │   │   │   ├── AppDbContext/
    │   │   │   │   └── AppDbContext.cs
    │   │   │   └── Configurations/      # IEntityTypeConfiguration<T>
    │   │   │       ├── ContractConfiguration.cs
    │   │   │       └── UserConfiguration.cs
    │   │   ├── DTOs/
    │   │   │   ├── Requests/
    │   │   │   └── Responses/
    │   │   └── Repositories/
    │   │       ├── Contract/            # interfaces
    │   │       │   ├── ICompanyRepository.cs
    │   │       │   ├── IContactRepository.cs
    │   │       │   └── IContractRepository.cs
    │   │       ├── Generic/
    │   │       │   ├── IRepository.cs
    │   │       │   └── Repository.cs
    │   │       └── Implementation/
    │   │           ├── CompanyRepository.cs
    │   │           ├── ContactRepository.cs
    │   │           └── ContractRepository.cs
    │   ├── Extensions/
    │   │   ├── ConfigurationsExtension.cs   # AddConfigurations()
    │   │   ├── EndpointsExtension.cs        # MapEndpoints()
    │   │   ├── RepositoriesExtension.cs     # AddRepositories()
    │   │   └── ServicesExtension.cs         # AddServices()
    │   ├── Migrations/
    │   └── Services/
    │       ├── Contract/                # interfaces
    │       │   ├── IAuthService.cs
    │       │   ├── ICompanyService.cs
    │       │   ├── IContactService.cs
    │       │   └── IContractService.cs
    │       └── Implementation/
    │           ├── AuthService.cs
    │           ├── CompanyService.cs
    │           ├── ContactService.cs
    │           └── ContractService.cs
    ├── Program.cs
    ├── Usings.cs
    ├── appsettings.json
    ├── appsettings.Development.json
    ├── appsettings.Production.json
    ├── Dockerfile
    ├── docker-compose.yml
    └── .env                             # never commit — in .gitignore
```

---

## Domain Entities

All entities inherit `Base` (abstract): `Id` (int), `CreatedAt` (DateTime UTC).

### CompanyEntity
```
CorporateName, CNPJ, Segment, WebSite?
```

### ContactEntity
```
Name, Email, Phone, Position, CompanyId
```

### ContractEntity
```
Title, Value (decimal 18,2), Status (EContractStatus), StartDate, EndDate?, ContactId, CompanyId
```

### UserEntity
```
Name, Email, PasswordHash, Role (EUserRole)
```

---

## Entity Relationships

```
Company (1) ──── (N) Contacts
Company (1) ──── (N) Contracts
Contact  (1) ──── (N) Contracts
```

---

## Contract Status Flow

```
Draft → Active → Suspended → Completed
                           → Cancelled
```

Status transitions handled via `PATCH /contracts/{id}/status`.

---

## API Endpoints

```
POST   /auth/register    (requires Admin or Manager role)
POST   /auth/login

GET    /contacts
POST   /contacts
GET    /contacts/{id}
PUT    /contacts/{id}
DELETE /contacts/{id}

GET    /companies
POST   /companies
GET    /companies/{id}
PUT    /companies/{id}
DELETE /companies/{id}

GET    /contracts
POST   /contracts
GET    /contracts/{id}
PUT    /contracts/{id}
DELETE /contracts/{id}
PATCH  /contracts/{id}/status
```

---

## Exception Handling

`GlobalExceptionHandler` (implements `IExceptionHandler`) maps exceptions to HTTP status codes:

| Exception | Status |
|---|---|
| `NotFoundException` | 404 |
| `BusinessRuleException` | 409 |
| `UnauthorizedAccessException` | 401 |
| `ArgumentOutOfRangeException` | 400 |
| `KeyNotFoundException` | 404 |
| `ValidationException` | 400 |
| Default | 500 |

---

## NuGet Packages

```
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Design
Microsoft.AspNetCore.Authentication.JwtBearer
Microsoft.AspNetCore.OpenApi
Mapster
FluentValidation.AspNetCore
Scalar.AspNetCore
```

---

## Docker Setup

### docker-compose.yml (Dev only)

```yaml
services:
  crmapi.api:
    image: ${DOCKER_REGISTRY-}crmapiapi
    build:
      context: .
      dockerfile: CrmApi.Api/Dockerfile
    depends_on:
      - sqlserver

  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: crm-sqlserver
    environment:
      SA_PASSWORD: ${SA_PASSWORD}
      ACCEPT_EULA: "Y"
    ports:
      - "1433:1433"
    volumes:
      - sqlserver_data:/var/opt/mssql

volumes:
  sqlserver_data:
```

### .env (never commit)

```env
SA_PASSWORD=CrmApi@Dev#2025!
```

---

## Database Credentials

### Development (Docker local)

```
Server:    localhost,1433
Database:  CrmApiDb
Login:     sa
Password:  CrmApi@Dev#2025!
```

### Production (Azure)

Connection string stored in Azure App Service environment variables — never in code or committed files.

---

## Environment Configuration

```
appsettings.json               # base config
appsettings.Development.json   # points to Docker SQL Server
appsettings.Production.json    # reads from Azure environment variables
```

---

## CI/CD Pipeline

```
Push to main
    → GitHub Actions
    → Build Docker image
    → Push to Docker Hub (free, no Azure Container Registry needed)
    → Azure App Service pulls the image automatically
```

---

## Important Decisions

- **Minimal API** over Controllers — cleaner, more modern .NET style
- **No Unit of Work** — EF Core DbContext covers this natively
- **No CQRS** — overkill for 3 entities
- **Generic Repository** — `Repository<TEntity>` reduces boilerplate; specific repos extend only when needed
- **Mapster** over AutoMapper — less config, better performance
- **Docker Hub** over Azure Container Registry — free tier, no extra cost
- **SQL Server on Azure** — created upfront to avoid migration work later
- **`.env` file** for local secrets, **Azure App Service config** for production secrets
