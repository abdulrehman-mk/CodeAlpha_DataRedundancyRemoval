# CodeAlpha Task 1 — Data Redundancy Removal System

A web-based order management system developed using **ASP.NET Core MVC, Entity Framework Core, and SQL Server** as part of the **CodeAlpha Cloud Computing Internship**.

The project focuses on reducing unnecessary data duplication through **database normalization and proper relational design**.

## What the System Does

The application manages:

* **Categories** — organizes products.
* **Products** — stores product information and category relationships.
* **Customers** — stores customer information.
* **Orders** — records customer orders.
* **Order Details** — connects orders with their products.

### Data Relationships

```text
Category
   ↓
Product
   ↓
Order Detail
   ↑
Order
   ↑
Customer
```

Each type of information is stored separately and connected using **Primary Keys and Foreign Keys**.

For example, a customer's information is stored once in the `Customer` table. Multiple orders can reference that customer through `CustomerId` instead of storing the same customer details repeatedly.

The same approach is used for products and categories.

## Why This Design?

This structure helps to:

* Reduce duplicate data
* Maintain data consistency
* Avoid unnecessary storage
* Make updates easier
* Keep the database organized

## Technologies

**C#** • **ASP.NET Core MVC (.NET 8)** • **Entity Framework Core** • **SQL Server** • **Bootstrap 5**
