# Checkout Challenge - Payment Gateway

This .NET Web API provides functionalities for merchants to process payments through a payment gateway and retrieve details of previously made payments.

## Prerequisites

Make sure you have installed:

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download)
- A suitable text editor or IDE (e.g., [Visual Studio Code](https://code.visualstudio.com/))

## Getting Started

Here are the steps to get the project up and running:

1. **Clone the repository** <br>Use the following command to clone the repository to your local machine:

```
git clone https://github.com/shanonbeary/PaymentGateway.git
```

2. **Navigate to the project directory** <br>
   After cloning, use the following command to navigate to the project directory:

```
cd PaymentGateway
```

3. **Setup Database** <br>
   If your application uses a database, make sure you have the correct connection string in the appsettings.json file for your database. Then, use the following command to apply migrations and create the database:

```
dotnet ef database update
```

4. **Run the project** <br>
   Run the project using the following command:

```
dotnet run
```

You should now be able to access the API endpoints at http://localhost:5000 (or https://localhost:5001 for HTTPS).

## Testing

TODO

## Architecture

TODO

## Cloud Architecture

TODO

## Asumptions

TODO

## Areas for improvement
