using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelAutoParser.CustomAttributes
{
    public class ExcelColumnAttribute : Attribute
    {
        //
        // Summary:
        //     Initializes a new instance of ExcelAutoParser.ExcelColumnAttribute using the
        //     specified excel column in integer.
        //
        // Parameters:
        //   columnInt:
        //     columnInt
        public ExcelColumnAttribute(int columnInt)
        {
            this.columnInt = columnInt;
        }
        //
        // Summary:
        //     Initializes a new instance of ExcelAutoParser.ExcelColumnAttribute using the
        //     specified excel column in string.
        //
        // Parameters:
        //   columnInt:
        //     columnString
        public ExcelColumnAttribute(string columnString)
        {
            this.columnString = columnString;
        }
        protected int columnInt;

        //
        // Summary:
        //     Gets the columnInt.
        public int ColumnInt
        {
            get
            {
                return this.columnInt;

            }
        }
        protected string columnString;
        //
        // Summary:
        //     Gets the columnString.
        public string ColumnString
        {
            get
            {
                return this.columnString;

            }
        }
    }
}
