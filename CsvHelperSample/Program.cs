using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;

// Create a new .NET Minimal API project
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add antiforgery services
builder.Services.AddAntiforgery();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add antiforgery middleware between UseRouting and UseEndpoints
app.UseRouting();
app.UseAntiforgery();

// Endpoint to get structured data from uploaded file
app.MapPost("/api/structured", async (IFormFile file) =>
{
    if (file == null || file.Length == 0)
        return Results.BadRequest("No file uploaded");

    using var stream = file.OpenReadStream();
    using var reader = new StreamReader(stream);
    using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

    csv.Context.RegisterClassMap<PersonMap>();
    var records = csv.GetRecords<Person>().ToList();

    return Results.Ok(records);
})
.WithName("GetStructuredData")
.WithOpenApi();

// Endpoint to get dynamic data from uploaded file
app.MapPost("/api/dynamic", async (IFormFile file) =>
{
    if (file == null || file.Length == 0)
        return Results.BadRequest("No file uploaded");

    using var stream = file.OpenReadStream();
    using var reader = new StreamReader(stream);
    using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

    var records = csv.GetRecords<dynamic>().ToList();

    return Results.Ok(records);
})
.WithName("GetDynamicData")
.WithOpenApi();

// Endpoint to generate and download CSV
app.MapGet("/api/download", () =>
{
    var records = new List<Person>
    {
        new Person { Id = 1, Name = "John Doe", Age = 30 },
        new Person { Id = 2, Name = "Jane Smith", Age = 25 },
        new Person { Id = 3, Name = "Bob Johnson", Age = 45 }
    };

    using var memoryStream = new MemoryStream();
    using var writer = new StreamWriter(memoryStream);
    using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));

    csv.Context.RegisterClassMap<PersonMap>();
    csv.WriteRecords(records);
    writer.Flush();

    return Results.File(
        memoryStream.ToArray(),
        "text/csv",
        "people.csv"
    );
})
.WithName("DownloadCsv")
.WithOpenApi();

app.Run();

// Sample structured data class
public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

// Class map for structured data
public class PersonMap : ClassMap<Person>
{
    public PersonMap()
    {
        Map(m => m.Id).Index(0).Name("id");
        Map(m => m.Name).Index(1).Name("name");
        Map(m => m.Age).Index(2).Name("age");
    }
}