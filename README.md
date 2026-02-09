EcommerceProductAPI

This is a ASP.NET Core Web API for managing products in an e-commerce system. It provides RESTful endpoints to create, read, update, and delete product,Category,User,UserRole,Role as an Admin.Please refer the controllers for endpoint details.

🧱 Project Overview

The solution uses a layered architecture to separate responsibilities and make the code maintainable and scalable.

EcommerceProductAPI/
├── Ecommerce.Api/            # API project (REST endpoints)
├── Ecommerce.Core/           # Core domain models and interfaces
├── Ecommerce.Data/           # Data access implementations
├── .vscode/
├── EcommerceProductAPI.sln   # Solution file

🚀 Features

✔ Basic CRUD operations for Products,Categories,Users,Roles,UserRole
✔ Clean layered structure with Dependency Injection
✔ Ready for database integration (EF Core, SQL Server, etc.)

🛠️ Tech Stack

Layer	         Technology

API                ASP.NET Core Web API
Language           C#
Architecture       Clean Architecture
ORM                Entity Framework
Tools              VS Code, Docker & Azure sql server
Version Control    Git & GitHub

📦 Getting Started
Prerequisites

Before running this API, make sure you have:

.NET SDK (.NET 8)
A database ( Azure SQL Server, Mysql or Any sql server)

💻 Clone the Repository
git clone https://github.com/Tejdeep-Akula/EcommerceProductAPI.git
cd EcommerceProductAPI

🔧 Restore Dependencies
dotnet restore

Configure Database

Update your connection string in appsettings.json inside the Ecommerce.Api project:

Run the API
dotnet run --project Ecommerce.Api

Swagger / API Explorer 

Swagger/OpenAPI is configured, you can explore the API in your browser at:https://localhost:{PORT}/swagger/index.html
