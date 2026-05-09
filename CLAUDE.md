# CrmApi — Project Context for Claude Code

## Project Overview

A CRM (Customer Relationship Management) REST API built with .NET 10, designed as a portfolio project. The API manages clients, companies, and contracts with status workflows.

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
| Logging | Serilog (Console + File sinks) |
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

---

## Project Structure

```
CrmApi/
├── CrmApi.slnx
└── CrmApi.Api/
    ├── Endpoints/
    │   ├── ContactEndpoints.cs
    │   ├── CompanyEndpoints.cs
    │   └── ContractEndpoints.cs
    ├── DTOs/
    │   ├── Requests/
    │   └── Responses/
    ├── Services/
    │   ├── Interfaces/
    │   └── Implementations/
    ├── Repositories/
    │   ├── Interfaces/
    │   └── Implementations/
    ├── Models/
    │   ├── Contact.cs
    │   ├── Company.cs
    │   ├── Contract.cs
    │   └── User.cs
    ├── Data/
    │   └── AppDbContext.cs
    ├── Migrations/
    ├── Middlewares/
    ├── Program.cs
    ├── appsettings.json
    ├── appsettings.Development.json
    ├── appsettings.Production.json
    ├── Dockerfile
    ├── docker-compose.yml
    └── .env                  # never commit — in .gitignore
```

---

## Domain Entities

### Company
```
Id, Name, CNPJ, Segment, Website, CreatedAt
```

### Contact
```
Id, Name, Email, Phone, Position, CompanyId, CreatedAt
```

### Contract
```
Id, Title, Value, Status, StartDate, EndDate, ContactId, CompanyId, CreatedAt
```

### User
```
Id, Name, Email, PasswordHash, Role, CreatedAt
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

Status transitions are handled via a dedicated `PATCH /contracts/{id}/status` endpoint.

---

## API Endpoints

```
POST   /auth/register
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

## NuGet Packages

```
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Design
Microsoft.AspNetCore.Authentication.JwtBearer
AutoMapper
FluentValidation
FluentValidation.AspNetCore
Serilog.AspNetCore
Serilog.Sinks.Console
Serilog.Sinks.File
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

> Connection string stored in Azure App Service environment variables — never in code or appsettings.Production.json committed to the repo.

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

## Development Order

1. Project setup + folder structure
2. Models + EF Core DbContext
3. Migrations + first database creation
4. Auth (JWT — register/login)
5. Companies CRUD (no dependencies)
6. Contacts CRUD (depends on Company)
7. Contracts CRUD + status flow (depends on both)
8. FluentValidation on all DTOs
9. Serilog configuration
10. Scalar documentation
11. Dockerfile + docker-compose
12. GitHub Actions CI/CD pipeline
13. Deploy to Azure App Service

---

## Important Decisions

- **Minimal API** over Controllers — cleaner, more modern .NET style
- **No Unit of Work** — EF Core DbContext covers this natively
- **No CQRS** — overkill for 3 entities
- **Docker Hub** over Azure Container Registry — free tier, no extra cost
- **SQL Server on Azure** — created upfront to avoid migration work later
- **`.env` file** for local secrets, **Azure App Service config** for production secrets
