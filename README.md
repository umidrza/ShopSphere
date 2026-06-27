# ShopSphere — Enterprise E-Commerce Platform (.NET 10)

A cloud-ready e-commerce platform built using **ASP.NET Core**, **Microservices Architecture**, **RabbitMQ**, **Redis**, **SQL Server**, **YARP API Gateway**, **Docker**, **MVC**, and **Azure-ready deployment practices**.

This project was built to demonstrate intermediate and advanced .NET engineering skills including:

- ASP.NET Core MVC
- Authentication & Authorization
- JWT + OAuth
- Microservices Architecture
- Event-Driven Design
- Asynchronous Programming
- Docker Containers
- API Gateway
- Cloud Deployment Preparation

---

# Architecture

```text
Browser
   │
   ▼

WebUI (MVC + Razor)
   │
   ▼

ApiGateway (YARP)
   │
   ├──────────────┐
   ▼              ▼

AuthService   ProductService
                  │
                  ▼

              CartService
                  │
                  ▼

             OrderService
                  │
          publish event
                  ▼

              RabbitMQ
                  │
                  ▼

        NotificationService
                  │
                  ▼

          Email Processing


Infrastructure
──────────────
SQL Server
Redis
Docker
```

---

# Features

## Authentication

- User registration
- Login
- JWT Authentication
- Google OAuth Login
- Claims-based authorization
- Role-based authorization

---

## Product Management

- Create products
- Update products
- Delete products
- View product catalog
- SQL Server persistence

---

## Shopping Cart

- Add products
- Remove products
- Redis storage
- Fast retrieval
- Temporary cart caching

---

## Orders

- Checkout flow
- Order history
- SQL persistence
- Async processing
- Event publishing

---

## Notifications

- RabbitMQ consumer
- Order confirmation workflow
- Email service abstraction

---

## Web UI

- ASP.NET Core MVC
- Razor Views
- Dependency Injection
- API integration

---

# Technology Stack

| Area | Technology |
|------|------|
| Backend | ASP.NET Core 8 |
| UI | MVC + Razor |
| Database | SQL Server |
| Cache | Redis |
| Messaging | RabbitMQ |
| Authentication | JWT |
| OAuth | Google |
| API Gateway | YARP |
| Containerization | Docker |
| Cloud | Azure Ready |
| Documentation | Swagger |

---

# Microservices

## AuthService

Responsibilities:

- Registration
- Login
- JWT Generation
- Google OAuth

---

## ProductService

Responsibilities:

- Product CRUD
- Catalog

Database:

```text
SQL Server
```

---

## CartService

Responsibilities:

- Cart management
- Fast session state

Storage:

```text
Redis
```

---

## OrderService

Responsibilities:

- Checkout
- Orders
- Event publishing

Database:

```text
SQL Server
```

---

## NotificationService

Responsibilities:

- Consume events
- Send notifications

Communication:

```text
RabbitMQ
```

---

# Local Development

## Requirements

Install:

- .NET 8 SDK
- Docker Desktop
- SQL Server
- Visual Studio / VS Code

---

# Run Locally

Run:

```bash
docker compose up --build
```

Open:

```text
WebUI:
http://localhost:8080

Gateway:
http://localhost:8000

RabbitMQ:
http://localhost:15672
```

---

