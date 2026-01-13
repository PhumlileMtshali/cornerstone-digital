# 🧱 Cornerstone Digital – E-Commerce Consulting Platform

Cornerstone Digital is a **service-based e-commerce web application** that enables users to browse, purchase, and manage **digital consulting services** such as **website development, UX/UI design, and portfolio creation**.
The platform is built using **C# for both frontend and backend**, showcasing full-stack development skills.

---

## 🚀 Project Overview

* Developed as part of a **group academic project**
* Focuses on **selling services instead of physical products**
* Simulates a **real-world digital consulting agency**
* Allows clients to order services and track project progress online

---

## 🎯 Objectives

* Build a **full-stack e-commerce application** using C#
* Implement **service-based purchasing workflows**
* Apply **software engineering best practices**
* Use **GitHub for version control and collaboration**

---

## 🧩 Services Offered

* **Website Development**
* **UX / UI Design**
* **Portfolio Website Creation**
* **Digital Consulting**
* **Website Maintenance & Optimization**

---

## ✨ Key Features

* **User registration and authentication**
* **Browse and purchase consulting services**
* **Service-based checkout system**
* **Admin service and order management**
* **Database-driven architecture**

---

## 🛠 Technology Stack

### 🔙 Backend

* C#
* ASP.NET Core
* Entity Framework Core
* SQL Server

### 🎨 Frontend

* ASP.NET MVC (Razor Pages) **or** Blazor
* Bootstrap

### 🧰 Tools

* Visual Studio
* Git & GitHub
* SQL Server Management Studio

---
## 📂 Project Structure

The project follows the ASP.NET MVC architecture with a clear separation of concerns:
```
/Controllers      # Handles HTTP requests and application flow
/Models           # Domain models and data entities
/Views            # Razor views (UI layer)
/Data             # Database context and migrations
/Services         # Business logic and service layer
/wwwroot          # Static files (CSS, JS, images)
/appsettings.json # Application configuration settings
```
---

## 🧑‍💻 How to Run the Project Locally

### Prerequisites
- Visual Studio 2022 or later  
- .NET SDK  
- SQL Server or SQL Server Express  

### Steps

1. Clone the repository:
   ```
   git clone https://github.com/PhumlileMtshali/cornerstone-digital.git
   ```
2. Open the solution file in Visual Studio

3. Restore NuGet packages:

- Visual Studio will restore automatically, or

- Right-click the solution → Restore NuGet Packages

4. Update the database connection string in appsettings.json

5. Run database migrations:

- Using Package Manager Console:

```
Update-Database
Or using .NET CLI:
```

```
dotnet ef database update
```
6. Start the application:

- Press F5 or click Run in Visual Studio

- Access the application in your browser:
```
https://localhost:xxxx
```
---

## 📄 License

* Developed **for educational purposes only**
* All code and assets are intended for **academic use**


