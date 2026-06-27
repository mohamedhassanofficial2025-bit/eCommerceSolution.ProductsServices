# 🛍️ eCommerceSolution - Products Microservice

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Minimal%20API-512BD4?logo=dotnet)](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis)
[![MySQL](https://img.shields.io/badge/MySQL-8.0-4479A1?logo=mysql)](https://www.mysql.com/)
[![EF Core](https://img.shields.io/badge/EF%20Core-MySQL-512BD4?logo=dotnet)](https://learn.microsoft.com/en-us/ef/core/)
[![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?logo=docker)](https://www.docker.com/)
[![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?logo=swagger)](https://swagger.io/)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)

A production-ready **Products Microservice** built with .NET 8 Minimal API, following a clean 3-tier architecture with Repository & Service patterns. Part of a larger eCommerce microservices ecosystem.

---

## 📋 Table of Contents

- [Architecture](#-architecture)
- [Tech Stack](#-tech-stack)
- [Features](#-features)
- [Getting Started](#-getting-started)
- [API Endpoints](#-api-endpoints)
- [Project Structure](#-project-structure)
- [Data Models](#-data-models)
- [Validation](#-validation)
- [Docker](#-docker)
- [Configuration](#-configuration)

---

## 🏗️ Architecture

```
┌─────────────────────────────────────────────────────┐
│                  ProductsMicroService.API           │
│              (Minimal API - Presentation Layer)      │
│        Endpoints · Middleware · Swagger · CORS       │
└──────────────────────┬──────────────────────────────┘
                       │
┌──────────────────────▼──────────────────────────────┐
│                BusinessLogicLayer                     │
│               (Service Layer - BLL)                   │
│    Services · DTOs · Validators · AutoMapper         │
└──────────────────────┬──────────────────────────────┘
                       │
┌──────────────────────▼──────────────────────────────┐
│                DataAccessLayer                        │
│               (Data Access Layer - DAL)               │
│    Repositories · DbContext · Entities · EF Core      │
└──────────────────────┬──────────────────────────────┘
                       │
                ┌──────▼──────┐
                │   MySQL 8   │
                │  Database   │
                └─────────────┘
```

### Design Patterns

| Pattern | Implementation |
|---|---|
| **Repository Pattern** | `IProductsRepository` / `ProductsRepository` |
| **Service Layer Pattern** | `IProductsService` / `ProductService` |
| **DTO Pattern** | `ProductAddRequest`, `ProductUpdateRequest`, `ProductResponse` |
| **Validator Pattern** | FluentValidation validators |
| **Mapper Pattern** | AutoMapper profiles |
| **Middleware Pattern** | `ExceptionHandlingMiddleware` |
| **Dependency Injection** | Built-in DI with extension methods per layer |

---

## 🚀 Tech Stack

| Technology | Version |
|---|---|
| **.NET** | 8.0 |
| **C#** | 12.0 |
| **ASP.NET Core** | Minimal API |
| **Entity Framework Core** | MySQL Provider 8.0.5 |
| **MySQL** | 8.0 |
| **FluentValidation** | 11.9 |
| **AutoMapper** | 13.0.1 |
| **Swashbuckle (Swagger)** | 10.0.1 |
| **Docker** | Multi-stage build |

### NuGet Packages

| Package | Project | Version |
|---|---|---|
| `MySql.EntityFrameworkCore` | API · DAL | 8.0.5 |
| `MySql.Data` | API · DAL | 9.0.0 |
| `FluentValidation.AspNetCore` | API · BLL | 11.3.0 |
| `AutoMapper` | BLL | 13.0.1 |
| `Swashbuckle.AspNetCore` | API | 10.0.1 |
| `System.Text.Json` | DAL | 8.0.4 |
| `Microsoft.Extensions.DependencyInjection.Abstractions` | BLL · DAL | 8.0.1 |

---

## ✨ Features

- ✅ **Full CRUD** — Create, Read, Update, Delete products
- 🔍 **Search** — Search by product name or category (case-insensitive)
- 🧩 **Minimal API** — Modern, lean endpoint definitions
- ✅ **FluentValidation** — Request validation with auto + manual validation
- 🔄 **AutoMapper** — Clean entity-to-DTO mapping
- 🏗️ **Repository Pattern** — Abstracted data access layer
- 🐳 **Dockerized** — Multi-stage Docker build ready
- 📊 **Swagger UI** — Interactive API documentation
- 🔒 **CORS** — Pre-configured for Angular frontend (`localhost:4200`)
- 🛡️ **Global Error Handling** — Custom exception middleware with logging
- 🧾 **JSON Enum Serialization** — Human-readable enum strings in API responses
- 📦 **Clean Architecture** — 3-tier separation of concerns

---

## 🏁 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL 8](https://dev.mysql.com/downloads/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (optional)

### 1. Clone & Setup

```bash
git clone https://github.com/mohamedhassanofficial2025-bit/eCommerceSolution.ProductsServices.git
cd eCommerceSolution.ProductsServices
```

### 2. Configure Database

Update the connection string in `ProductsMicroService.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost; Port=3306; Database=ecommerceproductsdatabase; User ID=root; Password=your_password"
  }
}
```

### 3. Apply Migrations

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Run the Application

```bash
dotnet run --project ProductsMicroService.API
```

The API will be available at:
- **HTTP**: `http://localhost:5000`
- **HTTPS**: `https://localhost:5001`
- **Swagger**: `http://localhost:5000/swagger`

### 5. Run with Docker

```bash
docker build -t products-microservice -f ProductsMicroService.API/Dockerfile .
docker run -p 8080:8080 -p 8081:8081 products-microservice
```

---

## 📡 API Endpoints

| Method | Route | Description | Request | Response |
|---|---|---|---|---|
| **GET** | `/api/Products` | List all products | — | `200` → `ProductResponse[]` |
| **GET** | `/api/Products/{id:guid}` | Get product by ID | `Guid` path param | `200` → `ProductResponse` / `404` |
| **GET** | `/api/Products/search/{query}` | Search by name or category | `string` path param | `200` → `ProductResponse[]` |
| **POST** | `/api/Products` | Create a product | `ProductAddRequest` JSON body | `201` → `ProductResponse` |
| **PUT** | `/api/Products` | Update a product | `ProductUpdateRequest` JSON body | `200` → `ProductResponse` |
| **DELETE** | `/api/Products/{id:guid}` | Delete a product | `Guid` path param | `200` → message / `404` |

### Request / Response Examples

<details>
<summary><b>📥 Create Product</b></summary>

**Request:**
```json
POST /api/Products
{
  "productName": "Smartphone",
  "category": "Electronics",
  "unitPrice": 699.99,
  "quantityInStock": 150
}
```

**Response:**
```json
{
  "productID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "productName": "Smartphone",
  "category": "Electronics",
  "unitPrice": 699.99,
  "quantityInStock": 150
}
```
</details>

<details>
<summary><b>🔍 Search Products</b></summary>

**Request:**
```
GET /api/Products/search/Laptop
```

**Response:**
```json
[
  {
    "productID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "productName": "Gaming Laptop",
    "category": "Electronics",
    "unitPrice": 1299.99,
    "quantityInStock": 25
  }
]
```
</details>

<details>
<summary><b>📋 Product Categories</b></summary>

| Category |
|---|
| `Electronics` |
| `HomeAppliances` |
| `Furniture` |
| `Accessories` |
</details>

---

## 📁 Project Structure

```
eCommerceSolution.ProductsService/
│
├── ProductsMicroService.API/            # 🎯 Presentation Layer
│   ├── APIEndPoints/
│   │   └── EndPoints.cs                 # Minimal API route definitions
│   ├── Middleware/
│   │   └── ExceptionHandlingMiddleware.cs  # Global error handler
│   ├── Properties/
│   │   └── launchSettings.json          # Dev environment config
│   ├── appsettings.json                 # App configuration
│   ├── appsettings.Development.json
│   ├── Dockerfile                       # Multi-stage Docker build
│   ├── Program.cs                       # Host builder & DI setup
│   └── ProductsMicroService.API.csproj
│
├── BusinessLogicLayer/                  # ⚙️ Service Layer (BLL)
│   ├── DTO/
│   │   ├── ProductAddRequest.cs
│   │   ├── ProductUpdateRequest.cs
│   │   ├── ProductResponse.cs
│   │   └── CategoryOptions.cs           # Enum: Electronics, HomeAppliances, Furniture, Accessories
│   ├── Mappers/
│   │   ├── ProductAddRequestToProductMappingProfile.cs
│   │   ├── ProductUpdateRequestToProductMappingProfile.cs
│   │   └── ProductToProductResponseMappingProfile.cs
│   ├── ServiceContracts/
│   │   └── IProductsService.cs
│   ├── Services/
│   │   └── ProductsService.cs
│   ├── Validators/
│   │   ├── ProductAddRequestValidator.cs
│   │   └── ProductUpdateRequestValidator.cs
│   ├── DependencyInjection.cs           # BLL service registration
│   └── BusinessLogicLayer.csproj
│
├── DataAccessLayer/                     # 🗄️ Data Access Layer (DAL)
│   ├── Context/
│   │   └── ApplicationDbContext.cs      # EF Core DbContext
│   ├── Entities/
│   │   └── Product.cs                   # Domain entity
│   ├── Repositories/
│   │   └── ProductsRepository.cs        # Repository implementation
│   ├── RepositoryContracts/
│   │   └── IProductsRepository.cs       # Repository interface
│   ├── DependencyInjection.cs           # DAL service registration
│   └── DataAccessLayer.csproj
│
├── .dockerignore
├── .gitignore
├── eCommerceSolution.ProductsService.sln
└── README.md
```

---

## 📦 Data Models

### Product Entity

| Property | Type | Notes |
|---|---|---|
| `ProductID` | `Guid` | Primary key |
| `ProductName` | `string` | Required |
| `Category` | `string` | Stored as string, mapped via `CategoryOptions` enum |
| `UnitPrice` | `double?` | Optional |
| `QuantityInStock` | `int?` | Optional |

### CategoryOptions Enum

```csharp
public enum CategoryOptions
{
    Electronics,
    HomeAppliances,
    Furniture,
    Accessories
}
```

### DTO Mapping

```
ProductAddRequest    ──AutoMapper──►  Product (Entity)
ProductUpdateRequest ──AutoMapper──►  Product (Entity)
Product (Entity)     ──AutoMapper──►  ProductResponse
```

---

## ✅ Validation Rules

| Field | Rule | Error Message |
|---|---|---|
| `ProductName` | `NotEmpty` | "Product Name can't be blank" |
| `Category` | `IsInEnum` | "Category can't be blank" |
| `UnitPrice` | `InclusiveBetween(0, ∞)` | "Unit Price should be between 0 and the maximum value" |
| `QuantityInStock` | `InclusiveBetween(0, 2⁶³-1)` | "Quantity in Stock should be between 0 and 2147483647" |

Validation is applied at two levels:
1. **Auto-validation** — FluentValidation auto-validation on model binding
2. **Manual validation** — Explicit `ValidateAndThrow()` in the service layer

---

## 🐳 Docker

### Multi-Stage Build

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base       # Runtime
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build          # SDK
FROM build AS publish                                    # Publish
FROM base AS final                                       # Production
```

### Build & Run

```bash
# Build
docker build -t products-microservice -f ProductsMicroService.API/Dockerfile .

# Run
docker run -d -p 8080:8080 -p 8081:8081 --name products-api products-microservice
```

### Docker Compose (recommended for full stack)

```yaml
services:
  products-api:
    build:
      context: .
      dockerfile: ProductsMicroService.API/Dockerfile
    ports:
      - "8080:8080"
      - "8081:8081"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_HTTP_PORTS=8080
      - ASPNETCORE_HTTPS_PORTS=8081
    depends_on:
      - mysql-db

  mysql-db:
    image: mysql:8
    environment:
      MYSQL_ROOT_PASSWORD: your_password
      MYSQL_DATABASE: ecommerceproductsdatabase
    ports:
      - "3306:3306"
```

---

## ⚙️ Configuration

### appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost; Port=3306; Database=ecommerceproductsdatabase; User ID=root; Password=admin"
  },
  "AllowedOrigins": "http://localhost:4200",
  "AllowedHosts": "*"
}
```

### CORS

Pre-configured for Angular development at `http://localhost:4200` with any method/header allowed.

### Environment Variables (Docker)

| Variable | Default | Description |
|---|---|---|
| `ASPNETCORE_ENVIRONMENT` | `Production` | Runtime environment |
| `ASPNETCORE_HTTP_PORTS` | `8080` | HTTP port |
| `ASPNETCORE_HTTPS_PORTS` | `8081` | HTTPS port |
| `ConnectionStrings__DefaultConnection` | — | MySQL connection string |

---

## 🛠️ Development

### Prerequisites

Install the .NET SDK and MySQL, then:

```bash
# Restore dependencies
dotnet restore

# Build
dotnet build

# Run tests (when added)
dotnet test

# Watch mode (hot reload)
dotnet watch run --project ProductsMicroService.API
```

### Key Commands

| Command | Description |
|---|---|
| `dotnet run` | Run the API |
| `dotnet build` | Build the solution |
| `dotnet watch run` | Run with hot reload |
| `dotnet ef migrations add <name>` | Create a new migration |
| `dotnet ef database update` | Apply migrations |
| `docker build` | Build Docker image |

---

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

---

## 📄 License

This project is licensed under the **MIT License** — see the [LICENSE](LICENSE) file for details.

---

## 🌟 Acknowledgments

- Built with [.NET 8](https://dotnet.microsoft.com/)
- Database by [MySQL](https://www.mysql.com/)
- API documentation via [Swagger](https://swagger.io/)
- Part of the **eCommerceSolution** microservices ecosystem

---

<p align="center">
  Made with ❤️ using .NET 8
</p>
