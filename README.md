
---

# Very Small Market System (VSM System)

VSM System is a microservice-based system designed to manage user, product, and order processes using a microservices architecture. Each service is developed using the C# programming language and runs on .NET Core 6. The system is designed to facilitate product management, ordering, and payment processes within a relatively small scope.

## Services

VSM System consists of three main services:

1. **User Management Service:**
   This service is responsible for managing user data. Users can have multiple roles, namely "Administrator" or "Customer". When users successfully log in, they are provided with a token that will be used to access other services. If the token expires, users must log in again.

2. **Product Management Service:**
   This service is used to add, modify, and delete products. The "Administrator" role can manage products, while the "Customer" role only has permission to view products.

3. **Order & Payment Management Service:**
   This service handles the order creation and payment processes. Both users and administrators can create orders, but once created, orders cannot be modified. Payments can only be made through the "Cash" or "Transfer" methods.

## Technology

- **Architecture:** Microservice
- **Programming Language:** C Sharp (C#)
- **Framework:** .NET Core 6
- **Version Control:** GIT
- **Database:** SQL Server

## Requirements

### User Management Service

- Each user can have multiple roles.
- There are two roles: "Administrator" and "Customer".
- Each login will generate a token to access other services.
- If the token expires, users must log in again.
- A new token will replace an expired token.

### Product Management Service

- Only the "Administrator" role can manage products.
- The "Customer" role only has permission to view products.

### Order & Payment Management Service

- Both users and administrators can create orders.
- Created orders cannot be modified.
- Payments can only be made through the "Cash" or "Transfer" methods.
- Orders have statuses: "Pending", "Waiting for Payment", and "Completed".

## Usage Instructions

1. Make sure you have .NET Core 6 and SQL Server installed.
2. Clone this repository.
3. Set up the database according to the configuration.
4. Run each service.
5. Access the service APIs through the specified endpoints.

## Contribution

Contributions are always welcome. Please open an issue for discussion before creating a pull request.

## License

This project is licensed under the Nuari Project License.

---

