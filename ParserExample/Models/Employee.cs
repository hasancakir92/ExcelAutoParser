
using ExcelAutoParser;
using ExcelAutoParser.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ParserExample.Models
{
    public class Employee:ObjectIndexer<Employee>
    {
       

        [ExcelColumn("A")]
        [Required(ErrorMessage ="Employee Id is Required")]
        public string EmployeeId { get; set; }
        [ExcelColumn("B")]
        [Required(ErrorMessage = "Employee Name is Required")]
        public string EmployeeName { get; set; }
    }
}
