# Very Small Market System (VSM System)

The VSM System is a microservices-based system designed to manage user, product, and ordering processes using a microservices architecture. Each service is developed using the C# programming language and runs on .NET Core 6. The system is designed to facilitate product management, ordering, and payment processes on a relatively small scale.

## Services

The VSM System consists of three main services:

1. **User Management Service:**
   This service is responsible for managing user data. Users can have a single role, either "Administrator" or "Customer." When users successfully log in, they are issued a token that will be used to access other services. If the token's expiration time is reached, users must log in again.

2. **Product Management Service:**
   This service is used to add, modify, and delete products. The "Administrator" role can manage products, while the "Customer" role only has permission to view products. Products can also be viewed by anonymous users.

3. **Ordering & Payment Management Service:**
   This service handles the order creation and payment processes. Both "Customers" and "Administrators" can create orders, but once created, orders cannot be modified. Payments can only be made using the "Cash" or "Transfer" methods. Anonymous users need to register before placing an order.

## Technology

- **Architecture:** Microservices
- **Programming Language:** C Sharp (C#)
- **Framework:** .NET Core 6
- **Version Control:** GIT
- **Database:** SQL Server
- **Others:** Database First Method

## Requirements

### User Management Service

- Each user can have only one role.
- There are two roles: "Administrator" and "Customer."
- Every login generates a token for accessing other services.
- If the token's expiration time is reached, users must log in again.
- A new token will replace an expired token.

### Product Management Service

- Only "Administrators" can manage products.
- "Customers" have permission only to view products.

### Ordering & Payment Management Service

- Both users and administrators can create orders.
- Created orders cannot be modified.
- Payments can only be made using the "Cash" or "Transfer" methods.
- Orders have statuses: "Pending," "Waiting for Payment," and "Completed."

## Usage Instructions

1. Make sure you have .NET Core 6 and SQL Server installed.
2. Clone this repository.
3. Configure the database according to the configuration.
4. Run each service.
5. Access the service APIs through the specified endpoints.

## Contribution

Contributions are always welcome. Please open an issue for discussion before creating a pull request.

## License

This project is licensed under the Nuari Project License.