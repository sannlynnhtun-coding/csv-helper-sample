using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Program;

namespace CsvHelperSample.ConsoleApp;

public class CsvService
{
    private readonly CsvHelperPackageService _csvHelperService;

    public CsvService(CsvHelperPackageService csvHelperService)
    {
        _csvHelperService = csvHelperService;
    }

    public List<Person> GetStructuredData(Stream stream)
    {
        using var reader = new StreamReader(stream);
        using var csv = _csvHelperService.GetCsvReader(reader);
        csv.Context.RegisterClassMap<PersonMap>();
        return csv.GetRecords<Person>().ToList();
    }

    public List<dynamic> GetDynamicData(Stream stream)
    {
        using var reader = new StreamReader(stream);
        using var csv = _csvHelperService.GetCsvReader(reader);
        return csv.GetRecords<dynamic>().ToList();
    }

    public byte[] GenerateCsv(List<Person> records)
    {
        using var memoryStream = new MemoryStream();
        using var writer = new StreamWriter(memoryStream);
        using var csv = _csvHelperService.GetCsvWriter(writer);

        csv.Context.RegisterClassMap<PersonMap>();
        csv.WriteRecords(records);
        writer.Flush();

        return memoryStream.ToArray();
    }
}
