# CodeAlpha_DataRedundancyRemoval

### CodeAlpha Cloud Computing Internship — Task

A web-based **Data Redundancy Removal System** built with ASP.NET Core MVC and SQL Server. The project demonstrates how proper database normalization and relationships can prevent unnecessary duplication and keep data accurate and consistent.

## 🎯 What This Project Does

The system manages **Categories, Products, Customers, and Orders** while avoiding repeated data.

For example, instead of storing a customer's name and contact information again with every order, the customer is stored once and each order refers to that customer using a `CustomerId`.

The same approach is used for products and categories.

### Database Relationship

```text
Category
   ↓
Product
   ↓
OrderDetail
   ↑
Order
   ↑
Customer
```

This structure keeps the database organized and reduces unnecessary data duplication.

## ✨ Features

* Category management
* Product management
* Customer management
* Order management
* Order detail management
* CRUD operations
* Primary and Foreign Key relationships
* Database normalization
* SQL Server integration
* Seed data for testing
* Automatic database migration on startup

## 🛠️ Technologies

* **ASP.NET Core MVC (.NET 8)**
* **C#**
* **Entity Framework Core**
* **SQL Server / LocalDB**
* **Bootstrap 5**

## 💡 Key Learning

This project demonstrates how **database normalization and relational design** can reduce redundancy, improve data consistency, and make an application easier to maintain.

## 📌 CodeAlpha Internship

**Organization:** CodeAlpha
**Task:** Task — Data Redundancy Removal System
**Batch:** July 2026
**Intern:** Abdul Rehman
