# CrmApi

REST API for CRM (Customer Relationship Management) built with .NET 10. Portfolio project.

## Tech Stack

- **Framework:** ASP.NET Core Minimal API (.NET 10)
- **ORM:** Entity Framework Core
- **Database:** SQL Server (Docker locally, Azure in production)
- **Auth:** JWT Bearer
- **Validation:** FluentValidation
- **Docs:** Scalar
- **Logging:** Serilog
- **Deploy:** Azure App Service via GitHub Actions → Docker Hub

## Domain

Three core entities and their relationships:

```
Company (1) ──── (N) Contacts
Company (1) ──── (N) Contracts
Contact  (1) ──── (N) Contracts
```

### Contract Status Flow

```
Draft → Active → Suspended → Completed
                           → Cancelled
```

## Endpoints

```
POST   /auth/register
POST   /auth/login

GET    /companies
POST   /companies
GET    /companies/{id}
PUT    /companies/{id}
DELETE /companies/{id}

GET    /contacts
POST   /contacts
GET    /contacts/{id}
PUT    /contacts/{id}
DELETE /contacts/{id}

GET    /contracts
POST   /contracts
GET    /contracts/{id}
PUT    /contracts/{id}
DELETE /contracts/{id}
PATCH  /contracts/{id}/status
```

## Running Locally

**Requirements:** Docker, .NET 10 SDK

1. Create `.env` in `CrmApi.Api/`:
   ```env
   SA_PASSWORD=YourPassword@Here
   ```

2. Start SQL Server:
   ```bash
   docker-compose up sqlserver -d
   ```

3. Apply migrations:
   ```bash
   dotnet ef database update --project CrmApi.Api
   ```

4. Run:
   ```bash
   dotnet run --project CrmApi.Api
   ```

API docs available at `/scalar` in development.

## Architecture

```
Endpoints → DTOs → Services → Repositories → Database
```

No Controllers — routes mapped directly in Endpoint classes. No CQRS or Unit of Work — EF Core's DbContext handles it natively.
