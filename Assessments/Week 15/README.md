# 🛒 Northwind Catalog (Full Stack .NET Project)

## 🚀 Overview

Northwind Catalog is a 3-layer full-stack .NET application built using the Northwind database. It demonstrates DB First approach, Web APIs, MVC UI, and unit testing.

---

## 🏗️ Architecture

```
NorthwindCatalog
│
├── NorthwindCatalog.Web        → MVC UI
├── NorthwindCatalog.Services   → API + Business Logic
├── NorthwindCatalog.Tests      → Unit Tests
```

---

## ⚙️ Tech Stack

* ASP.NET Core MVC
* ASP.NET Web API
* Entity Framework Core (DB First)
* SQL Server
* AutoMapper
* xUnit
* Bootstrap

---

## 🧱 Features

### ✅ DB First Approach

* Models generated using EF Core scaffolding

### ✅ Repository Pattern

* Clean separation of data access logic

### ✅ REST APIs

* Get Categories
* Get Products by Category
* Get Summary Analytics

### ✅ MVC UI

* Card-based category display
* Product listing with inventory value
* Summary dashboard

### ✅ UI Enhancements

* Modern card design
* Image-based categories
* Hover animations

### ✅ Unit Testing

* InventoryValue logic tested using xUnit

---

## 📡 API Endpoints

```
GET /api/categories
GET /api/products/by-category/{id}
GET /api/products/summary
```

---

## 🖼️ UI Preview

* Categories displayed in cards with images
* Products shown with pricing and stock
* Summary table for analytics

---

## ▶️ How to Run

1. Clone repository:

```
git clone <your-repo-link>
```

2. Open solution in Visual Studio

3. Configure connection string in `appsettings.json`

4. Run:

* Start Services project (API)
* Start Web project (UI)

---

## 🧪 Run Tests

```
dotnet test
```

---

## 📌 Key Learnings

* DB First with EF Core
* Repository Pattern
* API to MVC integration
* Clean UI design
* Unit testing in .NET