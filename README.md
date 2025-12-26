# Booking System

Detta projekt är ett bokningssystem för conferans rum, utvecklat som en del av en kurs i **Test-Driven Development (TDD)**.
Systemet är byggt med .NET 9 och följer en **Layered Architecture** för att bättre testbarhet och tydlig **separation of concerns**.

## Project Structure

Lösningen är uppdelad i fyra huvudprojekt för att separera **Domain logic**, gränssnitt och tester:

### 1. BookingSystem.Core

Detta är hjärtat i applikationen och innehåller den centrala business logic.

- **Models**: Innehåller **Domain entities** som `Room` och `Booking`. Logik för att kontrollera tillgänglighet ligger direkt i `Room`-modellen via metoden `IsAvailable`.
- **Services**: `BookingService` hanterar interaktionen mellan databasen och affärsregler, såsom skapande av bokningar och rum.
- **Data**: `BookingDbContext` hanterar databaskopplingen via **Entity Framework Core**.
- **Migrations**: Innehåller historik över databasschemats förändringar (**Database schema migrations**).

### 2. BookingSystem.Api

Ett RESTful API som exponerar systemets funktionalitet.

- **Controllers**: Innehåller `BookingController` och `RoomController`.
- **DTOs**: Data Transfer Objects används för att skicka data mellan klient och server.

### 3. BookingSystem.Cli

Ett **Command-line interface** (CLI) som låter användare interagera med bokningssystemet direkt via terminalen.

### 4. BookingSystem.Tests

Projektets **Test suite**, vilket är centralt för TDD-metodiken.

* **Unit Tests**: `BookingAvailableTests` verifierar logiken för rumstillgänglighet isolerat.
* **Integration Tests**: `BookingServiceTests` testar tjänstelagret mot en **in-memory SQLite database** för att säkerställa att persistens och logik fungerar ihop.

## Tech Stack

- **Framework**: .NET 9.0.
- **ORM**: Entity Framework Core 9.0.3.
- **Database**: Microsoft SQL Server (produktion/dev) och SQLite (tester).
- **Test Framework**: xUnit.
- **API Documentation**: Swagger/OpenAPI.

## TDD Methodology

I detta projekt har TDD tillämpats genom följande steg:

1. Skriva misslyckade **Unit tests** för kritiska funktioner (t.ex. överlappande bokningar i `IsAvailable`).
2. Implementera minsta möjliga kod i `BookingSystem.Core` för att få testerna att passera.
3. Refaktorera koden för bättre läsbarhet och struktur under skydd av existerande tester.
