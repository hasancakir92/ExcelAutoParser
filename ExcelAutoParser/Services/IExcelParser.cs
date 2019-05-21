using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExcelAutoParser.Services
{
    public interface IExcelParser
    {
        //
        // Summary:
        //     Parser excel rows to list of an T object. 
        //     
        //
        // Parameters:
        //   excelFileStream:
        //     Stream of excel file that will be parsed.
        //   worksheetName:
        //     Worksheet name in excel file that will be parser.
        List<T> ParseExcelList<T>(Stream excelFileStream,string worksheetName) where T : ObjectIndexer<T>, new();
    }
}
