using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelAutoParser.Services
{
    public interface IObjectValidator
    {
        //
        // Summary:
        //     Validate an T object. 
        //     
        //
        // Parameters:
        //   entity:
        //     The entity that needs to validated!
        
        KeyValuePair<bool, string> Validate<T>(T entity);
        //
        // Summary:
        //     Validate collection of T object. 
        //     
        //
        // Parameters:
        //   entities:
        //     collection of T object.

        KeyValuePair<bool, string> Validate<T>(List<T> entities);

    }
}
