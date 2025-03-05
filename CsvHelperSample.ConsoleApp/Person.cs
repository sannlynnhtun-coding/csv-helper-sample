using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace CsvHelperSample.ConsoleApp;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

public class PersonMap : ClassMap<Person>
{
    public PersonMap()
    {
        Map(m => m.Id).Index(0).Name("id");
        Map(m => m.Name).Index(1).Name("name");
        Map(m => m.Age).Index(2).Name("age");
    }
}
