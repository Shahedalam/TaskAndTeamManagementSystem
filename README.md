# 🧩 Task & Team Management System API (.NET 7 + MySQL)

This is a full-featured **Task and Team Management API** built with **ASP.NET Core (.NET 7)**, **Entity Framework Core**, and **MySQL**. It supports user authentication, role-based authorization, task delegation, and team structuring.

---

## 📑 Table of Contents

- [Features](#-features)
- [Tech Stack](#-tech-stack)
- [Getting Started](#-getting-started)
- [Configuration](#-configuration)
- [Database Migration](#-database-migration)
- [Running the Application](#-running-the-application)
- [API Documentation](#-api-documentation)
- [Postman Collection](#-postman-collection)
- [Deployment](#-deployment)
- [License](#-license)

---

## 🌟 Features

- ✅ JWT-based Authentication
- 🧑‍💼 Role Management: Admin, Manager, Employee
- 👥 Team Management
- 📋 Task Assignment with Due Dates
- 📄 Clean RESTful API Design
- 🧪 Ready-to-test Postman Collection

---

## 🧰 Tech Stack

- **Backend**: ASP.NET Core 7
- **ORM**: Entity Framework Core (with Pomelo MySQL Provider)
- **Auth**: JWT Tokens
- **Database**: MySQL
- **API Docs**: Postman

---

## ⚙️ Getting Started

### Prerequisites

- [.NET SDK 7+](https://dotnet.microsoft.com/en-us/download)
- [MySQL 8+](https://dev.mysql.com/downloads/mysql/)
- [Postman](https://www.postman.com/downloads/)

### Clone the Project

```bash
git clone https://github.com/your-username/TaskAndTeamManagementSystem.git
cd TaskAndTeamManagementSystem
dotnet restore
```


## 🔧 Configuration

### Update MySQL Database Connection String

In `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;port=3306;database=task_team_db;user=root;password=yourpassword;"
}
```

If needed, override with `appsettings.Development.json`.

---

## 📦 Database Migration (MySQL + EF Core)

### Install Pomelo EF Core MySQL Provider

```bash
dotnet add package Pomelo.EntityFrameworkCore.MySql
```

### Install EF Tools (if not already)

```bash
dotnet tool install --global dotnet-ef
```

### Add Migration

```bash
dotnet ef migrations add InitialCreate
```

### Apply Migration to MySQL

```bash
dotnet ef database update
```

---

## 📚 API Documentation

### 🔐 Authentication

**POST** `/api/auth/login`

```json
Admin
{
  "email": "admin@demo.com",
  "password": "Admin123!"
}

Manager
{
  "email": "manager@demo.com",
  "password": "Manager123!"
}
Employee
{
  "email": "employee@demo.com",
  "password": "Employee123!"
}
```

Returns a JWT token. Use it in the header:

```
Authorization: Bearer <your-token>
```

---

### 👤 Users

| Method | Endpoint              | Description     |
|--------|-----------------------|-----------------|
| GET    | `/api/users`          | List all users  |
| POST   | `/api/users`          | Create user     |
| PUT    | `/api/users/{id}`     | Update user     |
| DELETE | `/api/users/{id}`     | Delete user     |

---

### 👥 Teams

| Method | Endpoint              | Description     |
|--------|-----------------------|-----------------|
| GET    | `/api/teams`          | List all teams  |
| POST   | `/api/teams`          | Create team     |
| PUT    | `/api/teams/{id}`     | Update team     |
| DELETE | `/api/teams/{id}`     | Delete team     |

---

### 📋 Tasks

| Method | Endpoint              | Description     |
|--------|-----------------------|-----------------|
| GET    | `/api/tasks`          | List all tasks  |
| POST   | `/api/tasks`          | Create task     |
| PUT    | `/api/tasks/{id}`     | Update task     |
| DELETE | `/api/tasks/{id}`     | Delete task     |
