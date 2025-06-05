# 🖥️ Asus Egypt Dashboard API

This repository contains the backend API for the **Asus Egypt Dashboard**, a web-based admin panel designed to manage Asus product data, categories, users, and orders in Egypt.

---

## 🚀 Project Overview

The **Asus Egypt Dashboard API** is built with **ASP.NET Core Web API** and serves as the backend for managing the business logic and data operations of the Asus Egypt product platform. It exposes RESTful endpoints for product CRUD operations, category and order management, and user-related features.

---

## 🧰 Tech Stack

- **Backend:** ASP.NET Core Web API
- **ORM:** Entity Framework Core
- **Database:** SQL Server
- **Authentication:** JWT (optional — depending on implementation)
- **Development Tools:** Visual Studio / VS Code
- **Version Control:** Git & GitHub

---

## 📂 Folder Structure

```
Asus-Egypt-Dashboard-API/
├── Controllers/
├── Models/
├── Data/
├── DTOs/
├── Services/
├── Mappings/
├── Program.cs
├── Startup.cs (if using .NET Core 3.1 or 5)
├── appsettings.json
└── README.md
```

---

## ⚙️ Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/kerolosawny/Asus_Egypt.git
cd Asus_Egypt
```

### 2. Set Up the Environment

- Open the solution in **Visual Studio** or **VS Code**
- Create a local **SQL Server** database
- Update your `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=AsusDashboardDb;Trusted_Connection=True;"
}
```

### 3. Apply Migrations (if applicable)

```bash
dotnet ef database update
```

### 4. Run the API

```bash
dotnet run
```

By default, it will be hosted at `https://localhost:5001` or `http://localhost:5000`.

---

## 🧪 API Endpoints (Sample)

> Note: These are example endpoints — replace with your actual routes.

- `GET /api/products` – Get all products
- `POST /api/products` – Create a product
- `PUT /api/products/{id}` – Update a product
- `DELETE /api/products/{id}` – Delete a product
- `GET /api/categories` – Get all categories
- `POST /api/orders` – Place a new order

---

## 🔐 Authentication (Optional)

> If your project uses authentication, add this section.

This project can be extended to use **JWT authentication**. You’ll need to:

- Register users
- Login and receive a token
- Add `[Authorize]` to protected endpoints

---

## 🛠️ Features

- ✅ Product CRUD operations
- ✅ Category management
- ✅ Order processing
- ✅ Admin dashboard support
- 🔒 Optional authentication
- 🧩 Modular service structure
- 🌐 RESTful architecture


