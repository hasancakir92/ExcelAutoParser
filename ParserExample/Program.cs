using ExcelAutoParser.Services;
using ParserExample.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace ParserExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string FilePath = @"ExampleFiles/Employee.xlsx";
            FileInfo file = new FileInfo(FilePath);
            Stream fileStream = file.OpenRead();
            IExcelParser _excelParser = new ExcelParser();
            IObjectValidator _objectValidator=new ObjectValidator();
            List<Employee> models = _excelParser.ParseExcelList<Employee>(fileStream,"EmployeeList");
            KeyValuePair<bool, string> Result = _objectValidator.Validate<Employee>(models);
            Console.ReadLine();
        }
    }
}
