# Lending API

## Features
- ASP.NET Core 7.0
- Minimal APIs
- Clean Architecture
- CQRS
- Docker
- Open API
- Marten Database

## Running API

### Start
`docker compose -f docker-compose.yml build`

`docker compose -f docker-compose.yml up -d`

[http://localhost:8082/swagger](http://localhost:8082/swagger)

### Stop

`docker compose down`

### Test
`dotnet test Lending.sln`

## Libraries
- Carter
- MediatR
- Mapster
- Marten
- FluentValidation

## Test Libraries
 - Xunit
 - Testcontainers
 - Bogus
 - Respawn
 - FluentAssertions

 ##### Effort: ~6 hours
 - Initial Code Development: 2h
 - Automation/Containers: 1h
 - Testing/Fixes 4h