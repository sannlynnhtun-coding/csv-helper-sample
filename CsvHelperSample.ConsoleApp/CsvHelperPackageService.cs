using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using CsvHelper;

namespace CsvHelperSample.ConsoleApp;

public class CsvHelperPackageService
{
    public CsvReader GetCsvReader(StreamReader reader)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture);
        return new CsvReader(reader, config);
    }

    public CsvWriter GetCsvWriter(StreamWriter writer)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture);
        return new CsvWriter(writer, config);
    }
}
