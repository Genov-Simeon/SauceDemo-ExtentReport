# Sauce Demo UI Tests with Playwright

This project contains automated UI tests for the Sauce Demo website using .NET Playwright.

## Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022 or later
- Node.js (required for Playwright)

## Setup

1. Clone the repository
2. Install Playwright browsers by running:
   ```bash
   pwsh playwright.ps1 install
   ```
3. Build the solution:
   ```bash
   dotnet build
   ```

## Running Tests

### Using Visual Studio

1. Open the solution in Visual Studio
2. Select the test configuration (DEV, QA, etc.)
3. Run tests using the Test Explorer

### Using Command Line

Run tests with specific configuration:
```bash
dotnet test --configuration DEV
```

Run tests with specific browser:
```bash
dotnet test --configuration DEV.Firefox
```

## Test Scenarios

### Scenario 1: Complete Order Flow
Tests the complete order flow including:
- Login
- Adding items to cart
- Removing items from cart
- Checkout process
- Order completion verification

### Scenario 2: Price Sorting
Tests the price sorting functionality:
- Login
- Sort items by price (high to low)
- Verify sorting order
- Logout

## Configuration

The project supports different configurations:
- DEV (Chrome)
- QA (Chrome)
- DEV.Firefox
- QA.Firefox

Configuration files are located in the `appsettings.{Configuration}.json` files.

## Reports

Test reports are generated using ExtentReports and can be found in the `TestResults` directory after test execution. 