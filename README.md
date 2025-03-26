# InternetCoffee API

## Overview
This API simulates an internet-connected coffee machine that allows users to brew coffee with specific conditions. It is built using .NET Core, Clean Architecture principles, and MediatR.

## Features
- Brew Coffee Endpoint: Calls to `/brew-coffee` return a message with the prepared time.
- Service Availability: Every fifth request returns `503 Service Unavailable`.
- April Fools' Day Special: Requests on April 1st return `418 I'm a teapot`.
- Caching Mechanism: Uses `Lo` to track request counts.
- swagger: `/swagger` for API documentation.

## Technologies Used
- .NET Core(.NET8)
- MediatR for CQRS
- MemoryCache for caching
- xUnit & Moq for unit testing and integration testing

## API Endpoints
### Brew Coffee
Request:
```
GET /brew-coffee
```

Responses:
- 200 OK: `{ "message": "Your piping hot coffee is ready", "prepared": "2025-03-25T14:00:00Z" }`
- 503 Service Unavailable: The machine is out of coffee (every 5th request).
- 418 I'm a teapot: April 1st special case.

## Testing
- Unit Tests: Validate core business logic.
- Integration Tests: Ensure end-to-end functionality.

## How to Run
1. Clone the repository.
2. Install dependencies via `.NET CLI`.
3. Run the API using `dotnet run`.
4. Execute tests with `dotnet test`.

## Code Structure
- src
  - Domain: Entities, Value Objects (if applicable)
  - Application: Queries, Handlers, Responses
  - Infrastructure: Caching Service and Database Service(if applicable)
  - Presentation: API Controllers
- tests 
  - Unit: Unit tests for core business logic
  - Integration: Integration tests for end-to-end functionality

## Roadmap
### Phase 1: Core Features (Completed)
- Implement basic coffee brewing logic
- Add caching mechanism for request tracking
- Introduce April Fools' Day special response
- Implement unit and integration tests
- Add API documentation using Swagger

### Phase 2: Iced Coffee (Completed)
- Add Iced Coffee by wheather condition

### Phase 3: Advanced Features
- Add JWT authentication for API endpoints
- Introduce user authentication & role-based access
- Implement database storage for tracking coffee requests
 

## Authors
csuamthboy
