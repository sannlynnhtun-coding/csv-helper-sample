using CsvHelperSample.ConsoleApp;
using System;

var csvHelperService = new CsvHelperPackageService();
var csvService = new CsvService(csvHelperService);

// Example: Generate and download CSV
var records = new List<Person>
{
            new Person { Id = 1, Name = "John Doe", Age = 30 },
            new Person { Id = 2, Name = "Jane Smith", Age = 25 },
            new Person { Id = 3, Name = "Bob Johnson", Age = 45 }
        };

var csvBytes = csvService.GenerateCsv<Person, PersonMap>(records);
File.WriteAllBytes("people.csv", csvBytes);
Console.WriteLine("CSV file generated and saved as 'people.csv'.");

// Example: Read structured data from a CSV file
var filePath = "people.csv";
using var fileStream = File.OpenRead(filePath);
var structuredData = csvService.GetStructuredData(fileStream);
Console.WriteLine("Structured Data:");
foreach (var person in structuredData)
{
    Console.WriteLine($"ID: {person.Id}, Name: {person.Name}, Age: {person.Age}");
}

// Example: Read dynamic data from a CSV file
using var dynamicFileStream = File.OpenRead(filePath);
var dynamicData = csvService.GetDynamicData(dynamicFileStream);
Console.WriteLine("Dynamic Data:");
foreach (var record in dynamicData)
{
    Console.WriteLine(record);
}