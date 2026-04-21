# 💱 Currency Converter Web API

A professional, high-performance .NET 8 Web API built with a clean architecture. This service provides real-time currency conversion by integrating with external exchange rate providers while ensuring reliability and speed through advanced caching and error-handling strategies.

## 🚀 Key Features

* **Clean Architecture**: Separated into `Core` (Business Logic & Models) and `API` (Controllers & Infrastructure) layers for maximum maintainability.
* **External API Integration**: Fetches real-time market data from the [ExchangeRate-API](https://www.exchangerate-api.com/).
* **Performance Optimization**: Implements **In-Memory Caching** to reduce external API dependency and ensure sub-millisecond response times for repeated requests.
* **Observability**: Integrated **Structured Logging** using `ILogger` to monitor cache hits, misses, and system health in real-time.
* **Resilience**: Features a **Global Exception Handler** (using .NET 8 `IExceptionHandler`) to provide standardized, secure error responses.
* **API Documentation**: Fully documented with **Swagger/OpenAPI**.

---

## 🏗️ Project Structure

* **`CurrencyConverter.Core`**: The heart of the application. Contains the currency logic, service interfaces, and data models. No dependencies on the web framework.
* **`CurrencyConverter.API`**: The entry point. Handles HTTP requests, dependency injection registration, and middleware configuration.

---

## 🛠️ Tech Stack

* **Framework**: .NET 8.0 (C#)
* **Caching**: Microsoft.Extensions.Caching.Memory
* **Communication**: HttpClient (Typed Client)
* **Documentation**: Swashbuckle (Swagger)

---

## 🚦 Getting Started

1.  **Clone the repository**:
    ```bash
    git clone https://github.com/Alexandr0w/CurrencyConverter.git
    ```
2.  **Navigate to the project folder**:
    ```bash
    cd CurrencyConverter
    ```
3.  **Run the application**:
    Press `F5` in Visual Studio or use the CLI:
    ```bash
    dotnet run --project CurrencyConverter.API
    ```
4.  **Test the API**:
    Open `https://localhost:[PORT]/swagger` in your browser.

---

## 📈 API Example

**Request:**
`GET /api/Converter/convert?from=USD&to=EUR&amount=100`

**Response:**
```json
{
  "from": "USD",
  "to": "EUR",
  "amount": 100,
  "result": 84.8957,
  "calculationTime": "2026-04-22T00:22:05.4065386+03:00"
}