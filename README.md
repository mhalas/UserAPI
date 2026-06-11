# 👤 UserAPI

[![.NET](https://github.com/USER/REPO/actions/workflows/build-and-publish.yml/badge.svg)](https://github.com/USER/REPO/actions/workflows/build-and-publish.yml)

Welcome to **UserAPI**, a clean, robust, and modern .NET 10 Web API designed for managing user data with ease. This project is built with **Clean Architecture** principles in mind, ensuring that your business logic remains isolated, testable, and easy to maintain.

Whether you're looking to integrate a user management system or just exploring a solid .NET boilerplate, you're in the right place!

---

## 🚀 Features

- **Full CRUD Support**: Manage users with dedicated endpoints.
- **Clean Architecture**: Separation of concerns across Domain, Application, Infrastructure, and API layers.
- **Mediator Pattern**: Decoupled request/response handling using [MediatR](https://github.com/jbogard/MediatR).
- **Validation**: Strict input validation with FluentValidation.
- **Docker Ready**: Fully containerized for consistent development and deployment.
- **Automated CI/CD**: Integrated GitHub Actions for building, testing, and releasing.

---

## 🛠️ Tech Stack

- **Framework**: .NET 10 (ASP.NET Core)
- **Database**: Microsoft SQL Server
- **Architecture**: Clean Architecture / CQRS
- **Key Libraries**: MediatR, Entity Framework Core, FluentValidation, Swashbuckle (Swagger)
- **DevOps**: Docker, Docker Compose, GitHub Actions

---

## 🏁 Getting Started

### 📋 Prerequisites

Before you dive in, make sure you have the following installed:
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Docker & Docker Compose](https://www.docker.com/products/docker-desktop)
- An IDE like [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [JetBrains Rider](https://www.jetbrains.com/rider/)

### ⚙️ Environment Setup

1. **Clone the repository**:
   ```bash
   git clone https://github.com/your-username/UserAPI.git
   cd UserAPI
   ```

2. **Configure your environment**:
   Copy the `.env.example` file to create your own `.env` file:
   ```bash
   cp .env.example .env
   ```
   Open the `.env` file and adjust the database password or host if needed.

---

## 🐳 Running with Docker

The easiest way to get UserAPI up and running is with Docker Compose. This will spin up both the API and a SQL Server instance.

```bash
docker-compose up --build
```

- **API**: [http://localhost:8080](http://localhost:8080)
- **Swagger UI**: [http://localhost:8080/swagger](http://localhost:8080/swagger)

---

## 💻 Local Development

If you prefer running the API locally without Docker:

1. **Update Connection String**: Ensure `src/Api/appsettings.json` points to a running SQL Server instance.
2. **Restore & Build**:
   ```bash
   dotnet restore src/UserAPI.slnx
   dotnet build src/UserAPI.slnx
   ```
3. **Run the API**:
   ```bash
   dotnet run --project src/Api/Api.csproj
   ```

---

## 📂 Project Structure

- **src/Api**: The entry point. Contains controllers, middleware, and dependency injection setup.
- **src/Application**: The "brain" of the app. Contains business logic, CQRS Commands/Queries, and Validators.
- **src/Domain**: The core. Contains entities and interfaces. No dependencies on other layers.
- **src/Infrastructure**: The "plumbing". Contains the database context, repositories, and external service implementations.

---

## 🧪 Testing

We take quality seriously. The project includes Unit, Integration, and E2E tests.

```bash
dotnet test src/UserAPI.slnx
```

---

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 🤝 Contributing

Contributions are what make the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

*Made with ❤️ by the UserAPI Team.*
