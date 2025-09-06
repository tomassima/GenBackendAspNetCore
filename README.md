# Demo Task Management API

A simple task management API built with ASP.NET Core 9.0 demonstrating clean architecture and best practices.

## Architecture

![Architecture Diagram](https://www.plantuml.com/plantuml/png/NK_BJWCn3BplLrYz09T-0kgYWjG34c8hxYRhLb7pY18lWh_79OGrkKIPySnePbSOCo_U3ULYGyAC7apqAGOAFF6N8yXa6CFPaPBCTCB5yP4-UNJs7BoemSX3UHXku2b7-OPv-8k2JUF6bE3sbT3mtGQmh5hnFQ2vTBOz-JXH_g0ihiVf2EFBeHhNYzO_nvbS-H7KxxymP7p7GjN_2xJW3PsTaMqCDK9XBJjSXvxGK5Mjy3fYJqasK31MS5i7s-szRZoeNN9gNJksLUnuNQi_wIurjATRQC-hK5L9qxrZ9yCMxxu1)

Project Dependencies:
- DemoServer → Models, Interfaces, Database, Validations
- Models → Interfaces
- Database → Interfaces
- Validations → Models, Interfaces

## Full-Stack Overview

This repository contains the backend for a simple Task Manager application. The frontend (React app) is maintained in a separate repository.

- **Backend:** ASP.NET Core 9.0 (DemoServer) implements a clean architecture for managing tasks, with Models, Interfaces, Database, and Validations projects.
- **Frontend:** A simple React app (in a separate repository) consumes the DemoServer API, providing a user interface for task management.

The React app communicates with the DemoServer API via HTTP endpoints, allowing users to view, create, update, and delete tasks. The backend handles business logic, validation, and data storage (mock database).

### How It Works

1. The React app (from its own repository) sends HTTP requests to the DemoServer API endpoints (see below).
2. DemoServer processes requests, validates data, and interacts with the mock database.
3. Responses are returned to the React app for display and further interaction.

This setup demonstrates separation of concerns, clean architecture, and modern web development best practices.



## Project Structure & Dependencies

- `DemoServer/`: Main API project with controllers and configuration
- `Models/`: Data models and enums
- `Interfaces/`: Interfaces for database and validation
- `Database/`: Mock database implementation
- `Validations/`: Custom validation logic
- `UnitTests/`: Comprehensive test suite

**Project Dependencies:**
  - DemoServer → Models, Interfaces, Database, Validations
  - Models → Interfaces
  - Database → Interfaces
  - Validations → Models, Interfaces

## Prerequisites

- .NET 9.0 SDK
- Visual Studio Code or Visual Studio 2025+

## Setup Instructions

1. Clone the repository:
   ```bash
   git clone https://github.com/tomassima/GenBackendAspNetCore.git
   cd GenBackendAspNetCore
   ```

2. Build the solution:
   ```bash
   dotnet build
   ```

3. Run the tests (see the Testing section below for details).

4. Run the application:
   ```bash
   cd DemoServer
   dotnet run --launch-profile http
   ```


## Testing

The project includes comprehensive unit tests covering:
- Database operations
- Task validation
- API endpoints
- Edge cases and error conditions

Run tests with:
```bash
dotnet test --collect:"XPlat Code Coverage"
```

The API will be available at:
- HTTP: http://localhost:5020
- Swagger UI: http://localhost:5020/swagger

## API Endpoints

Below are the main API endpoints for task management. These endpoints are consumed by the React frontend and can be tested via Swagger UI or HTTP clients.

### Tasks

- `GET /task` - Get all tasks
  - Returns: `200 OK` with tasks array or `204 No Content` if empty

- `POST /task` - Create or update a task
  - Body: Task object
  - Returns: `200 OK` or `400 Bad Request` if validation fails

- `DELETE /task?key={guid}` - Delete a task
  - Returns: `200 OK` or `400 Bad Request` if not found

## Task Model

```json
{
  "key": "guid",
  "name": "string",
  "priority": "number",
  "status": "enum (NotStarted = 0, InProgress = 1, Completed = 2)"
}
```

## Features

## Development & Features

### Features
- Clean Architecture
- Dependency Injection
- Interface-based design
- Comprehensive validation
- Swagger documentation
- Structured logging
- Thread-safe operations
- Comprehensive test coverage

### Development Guidelines
1. Follow SOLID principles
2. Add XML documentation for public APIs
3. Include unit tests for new features
4. Use meaningful commit messages
5. Keep the code clean and well-formatted

## Project Dependencies

## NuGet Dependencies
- Microsoft.AspNetCore.OpenApi
- Swashbuckle.AspNetCore
- Microsoft.AspNetCore.Mvc.Versioning
## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Testing

The project includes comprehensive unit tests covering:
- Database operations
- Task validation
- API endpoints
- Edge cases and error conditions

Run tests with:
```bash
dotnet test --collect:"XPlat Code Coverage"
```
