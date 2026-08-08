# CodeAlpha_DataRedundancyRemoval

An ASP.NET Core MVC (.NET 8) application built with Entity Framework Core, SQL Server,
and Bootstrap 5 that demonstrates **removing data redundancy through database normalization**.

## Domain model

| Entity        | Purpose                                                                 |
|---------------|--------------------------------------------------------------------------|
| `Category`    | Stored once; `Product` references it by `CategoryId` (no repeated text) |
| `Product`     | Stored once; referenced from `OrderDetail` by `ProductId`               |
| `Customer`    | Stored once; `Order` references it by `CustomerId`                      |
| `Order`       | Header row per order; line items live in `OrderDetail`                  |
| `OrderDetail` | Junction table resolving the Order &harr; Product many-to-many relation |

Instead of repeating a customer's name/email or a product's name/price on every
row (redundant data), each fact is stored exactly once and linked with a foreign key.

## Prerequisites

- Visual Studio 2022 (17.8+) with the **ASP.NET and web development** workload
- .NET 8 SDK
- SQL Server (LocalDB, which ships with Visual Studio, works out of the box)

## Getting started

1. Open `CodeAlpha_DataRedundancyRemoval.sln` in Visual Studio 2022.
2. Restore NuGet packages (Visual Studio does this automatically on open, or
   run `dotnet restore`).
3. Confirm the connection string in `appsettings.json` — the default targets
   LocalDB:
   ```
   Server=(localdb)\mssqllocaldb;Database=CodeAlpha_DataRedundancyRemovalDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True
   ```
4. Run the app (F5, or `dotnet run`). On startup, `Program.cs` calls
   `context.Database.Migrate()`, which creates the database and applies the
   `InitialCreate` migration automatically — including the seed data — the
   first time it runs. No manual `Update-Database` step is required.

## Regenerating migrations (optional)

The `Migrations` folder already contains a hand-verified `InitialCreate`
migration with seed data. If you change the models and want a fresh migration,
use the Package Manager Console in Visual Studio:

```powershell
Add-Migration <MigrationName>
Update-Database
```

or the .NET CLI:

```bash
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

(Requires the `dotnet-ef` tool: `dotnet tool install --global dotnet-ef`.)

## Project structure

```
CodeAlpha_DataRedundancyRemoval.sln
CodeAlpha_DataRedundancyRemoval/
├── Controllers/       Home, Categories, Products, Customers, Orders, OrderDetails
├── Models/             Category, Product, Customer, Order, OrderDetail, ErrorViewModel
├── Data/                ApplicationDbContext (EF Core config + HasData seed)
├── Migrations/         InitialCreate migration (schema + seed data)
├── Views/               Razor views (Bootstrap 5) for every controller
├── wwwroot/              site.css / site.js (Bootstrap 5 loaded via CDN)
└── Program.cs            App startup, DI, auto-migrate on launch
```
