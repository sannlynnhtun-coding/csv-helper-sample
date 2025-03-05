# CSV Handler Console Application

## Overview
This **Console Application** demonstrates how to handle **CSV file operations** using the **CsvHelper** library in **C#**. It provides functionality to **read**, **process**, and **generate CSV files** in both **structured** (strongly-typed) and **dynamic** (flexible) formats. The application is designed to be **modular**, **reusable**, and easy to maintain.

---

## Features
- **Structured Data Handling**: Read CSV files into strongly-typed objects (e.g., `Person` class).
- **Dynamic Data Handling**: Read CSV files into dynamic objects for flexible parsing.
- **CSV Generation**: Generate and download CSV files from a list of objects.
- **Modular Design**: Separates common CSV operations into reusable services.

---

## Key Components
1. **CsvHelperPackageService**: Handles common CSV operations like creating `CsvReader` and `CsvWriter` instances.
2. **CsvService**: Implements specific CSV tasks such as reading structured/dynamic data and generating CSV files.
3. **Console Application**: Demonstrates usage of the services for CSV file manipulation.

---

## How It Works
### 1. **Generate a CSV File**
The application can generate a CSV file from a list of strongly-typed objects (e.g., `Person`).

```csharp
var records = new List<Person>
{
    new Person { Id = 1, Name = "John Doe", Age = 30 },
    new Person { Id = 2, Name = "Jane Smith", Age = 25 },
    new Person { Id = 3, Name = "Bob Johnson", Age = 45 }
};

var csvBytes = csvService.GenerateCsv(records);
File.WriteAllBytes("people.csv", csvBytes);
Console.WriteLine("CSV file generated and saved as 'people.csv'.");
```

### 2. **Read Structured Data**
Read a CSV file into a list of strongly-typed objects.

```csharp
using var fileStream = File.OpenRead("people.csv");
var structuredData = csvService.GetStructuredData(fileStream);

Console.WriteLine("Structured Data:");
foreach (var person in structuredData)
{
    Console.WriteLine($"ID: {person.Id}, Name: {person.Name}, Age: {person.Age}");
}
```

### 3. **Read Dynamic Data**
Read a CSV file into a list of dynamic objects for flexible parsing.

```csharp
using var dynamicFileStream = File.OpenRead("people.csv");
var dynamicData = csvService.GetDynamicData(dynamicFileStream);

Console.WriteLine("Dynamic Data:");
foreach (var record in dynamicData)
{
    Console.WriteLine(record);
}
```

---

## Prerequisites
- **.NET SDK** (6.0 or later)
- **CsvHelper** library (install via NuGet)

---

## Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/csv-handler-console-app.git
   ```
2. Navigate to the project directory:
   ```bash
   cd csv-handler-console-app
   ```
3. Install the **CsvHelper** package:
   ```bash
   dotnet add package CsvHelper
   ```
4. Build and run the application:
   ```bash
   dotnet run
   ```

---

## Usage
- Modify the `Program.cs` file to customize CSV file paths or data.
- Use the `CsvService` methods to handle CSV operations in your own projects.

---

## Example Output
### Generated CSV File (`people.csv`)
```
id,name,age
1,John Doe,30
2,Jane Smith,25
3,Bob Johnson,45
```

### Console Output
```
CSV file generated and saved as 'people.csv'.
Structured Data:
ID: 1, Name: John Doe, Age: 30
ID: 2, Name: Jane Smith, Age: 25
ID: 3, Name: Bob Johnson, Age: 45
Dynamic Data:
{ Id = 1, Name = John Doe, Age = 30 }
{ Id = 2, Name = Jane Smith, Age = 25 }
{ Id = 3, Name = Bob Johnson, Age = 45 }
```