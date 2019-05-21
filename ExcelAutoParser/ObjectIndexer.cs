using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ExcelAutoParser
{

    public abstract class ObjectIndexer<T>
    {
        //
        // Summary:
        //     Give ability to client for calling class attributes like class['attribute_name']
        //     specified value.
        public object this[string propertyName]
        {
            get
            {
                Type myType = typeof(T);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                return myPropInfo.GetValue(this, null);
            }
            set
            {
                Type myType = typeof(T);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                myPropInfo.SetValue(this, value, null);

            }

        }
    }
}
