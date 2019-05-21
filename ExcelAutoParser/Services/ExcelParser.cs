using ExcelAutoParser.CustomAttributes;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ExcelAutoParser.Services
{
    public class ExcelParser : IExcelParser
    {
        string GetColumn(PropertyInfo type)
        {
            object[] attributes = type.GetCustomAttributes(typeof(ExcelColumnAttribute), false);

            string result = string.Empty;
            if (attributes.Length != 0)
            {
                foreach (object attribute in attributes)
                {
                    ExcelColumnAttribute column = attribute as ExcelColumnAttribute;

                    if (column != null)
                    {
                        result = column.ColumnString;
                        break;
                    }
                }
            }
            return result;
        }
        public List<T> ParseExcelList<T>(Stream excelFileStream, string worksheetName) where T : ObjectIndexer<T>, new()
        {
            using (ExcelPackage package = new ExcelPackage(excelFileStream))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[worksheetName];
                int totalRows = workSheet.Dimension.Rows;
                Type oType = typeof(T);
                PropertyInfo[] members = oType.GetProperties();
                List<T> models = new List<T>();
                List<KeyValuePair<PropertyInfo, string>> excelColumns = new List<KeyValuePair<PropertyInfo, string>>();
                foreach (PropertyInfo member in members)
                {
                    string Column = GetColumn(member);

                    if (Column == null)
                        continue;
                    excelColumns.Add(new KeyValuePair<PropertyInfo, string>(member, Column));
                }
                int index = 0;
                for (int i = 2; i <= totalRows; i++)
                {
                    if (models.Count() <= index)
                        models.Add(new T());

                    foreach (KeyValuePair<PropertyInfo, string> column in excelColumns)
                    {
                        if (column.Key.PropertyType == typeof(string))
                        {
                            models[index][column.Key.Name] = workSheet.Cells[column.Value + "" + i].Value.ToString();
                        }

                    }
                    index++;
                }

                return models;
            }
        }
    }
}
