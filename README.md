# Demo Task Management API

A simple task management API built with ASP.NET Core 9.0 demonstrating clean architecture and best practices.

## Architecture

![Architecture Diagram](https://www.plantuml.com/plantuml/png/NK_BJWCn3BplLrYz09T-0kgYWjG34c8hxYRhLb7pY18lWh_79OGrkKIPySnePbSOCo_U3ULYGyAC7apqAGOAFF6N8yXa6CFPaPBCTCB5yP4-UNJs7BoemSX3UHXku2b7-OPv-8k2JUF6bE3sbT3mtGQmh5hnFQ2vTBOz-JXH_g0ihiVf2EFBeHhNYzO_nvbS-H7KxxymP7p7GjN_2xJW3PsTaMqCDK9XBJjSXvxGK5Mjy3fYJqasK31MS5i7s-szRZoeNN9gNJksLUnuNQi_wIurjATRQC-hK5L9qxrZ9yCMxxu1)

## Project Dependencies

- DemoServer → Models, Interfaces, Database, Validations
- Models → Interfaces
- Database → Interfaces
- Validations → Models, Interfaces

## Project Structure

- `DemoServer/`: Main API project with controllers and configuration
- `Models/`: Data models and enums
- `Interfaces/`: Interfaces for database and validation
- `Database/`: Mock database implementation
- `Validations/`: Custom validation logic
- `UnitTests/`: Comprehensive test suite

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
3. Run the tests:

  ```bash
  dotnet test --collect:"XPlat Code Coverage"
  ```

4. Run the application (from the DemoServer folder):

  ```bash
  cd DemoServer
  dotnet run --launch-profile http
  ```

The API will be available at <http://localhost:5020> and Swagger UI at <http://localhost:5020/swagger>

## Quick API summary
- `GET /task` - Get all tasks
  - Returns: `200 OK` with tasks array or `204 No Content` if empty

- `POST /task` - Create or update a task
  - Body: Task object
  - Returns: `200 OK` or `400 Bad Request` if validation fails

- `DELETE /task?key={guid}` - Delete a task
  - Returns: `200 OK` or `400 Bad Request` if not found

For details, examples, and tests see the source files and the `UnitTests` project.

## License

MIT. See the `LICENSE` file.
