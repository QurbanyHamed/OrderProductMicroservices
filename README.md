# OrderProductMicroservices

This is a simple microservices project built with **.NET 8** using **RESTful APIs**.

It includes:
- **Product Service** – manages products and inventory.
- **Order Service** – creates orders and checks product availability.
- **Shared Project** – contains shared DTOs/models.
- **Serilog** – used for structured logging.
- **Swagger** – available for both services for easy testing.

---

## 📦 Technologies

- ASP.NET Core Web API
- Entity Framework Core (In-Memory)
- Serilog for logging
- Swagger for API documentation

---

## 🧪 How to Run

### Run ProductService:
```bash
dotnet run --project ProductService
Swagger: http://localhost:7027/swagger
dotnet run --project OrderService
Swagger: http://localhost:7028/swagger

API Endpoints
Product Service
| Method | Endpoint                   | Description                  |
|--------|----------------------------|------------------------------|
| GET    | `/products/{id}`           | Get product info             |
| POST   | `/products`                | Add new product              |
| PUT    | `/products/{id}/stock`     | Update stock count           |

Order Service
| Method | Endpoint          | Description                      |
|--------|-------------------|----------------------------------|
| POST   | `/orders`         | Create an order (check product) |
| GET    | `/orders/{id}`    | Get order info                   |

Sample Order Flow
1. Create a product in ProductService.
2. Update its stock.
3. Create an order via OrderService (it contacts ProductService to validate stock).

Logging
Both services log important actions and errors to the console using Serilog
