# E-Commerce Order System

## Used Technologies

- .NET Core 3.1
- PostgreSql
- Docker

## Installation

### Docker

Run `docker-compose up -d` command in your terminal and navigate to `http://localhost`.

## Endpoints

Sample product data is seeded at application starting as follows:

```json
[
  {
    "id": "35984747-3fb3-4847-a573-91a21f23eb82",
    "code": "iphone",
    "name": "iPhone 11",
    "price": 10000,
    "stock": 20
  },
  {
    "id": "446a2038-90d1-4a52-a680-e54d2b6dcec8",
    "code": "mbp",
    "name": "Macbook Pro 16",
    "price": 22750,
    "stock": 40
  }
]
```

### Create Order

Send a HTTP POST request to `http://localhost/orders` endpoint.

```http
POST /orders HTTP/1.1
Host: localhost
Content-Type: application/json

{
    "ProductCode": "iphone",
    "Quantity": 10
}
```
