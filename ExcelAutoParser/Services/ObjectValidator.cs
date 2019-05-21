using ExcelAutoParser.CustomAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ExcelAutoParser.Services
{
    public class ObjectValidator:IObjectValidator
    {
        private ICollection<ValidationResult> validationResults;
        private ICollection<ValidationResult> ValidationResults
        {

            get
            {
                return validationResults;
            }
            set
            {
                validationResults = value;
                if (value.Count() > 0)
                    ValidationMessage = validationResults.Select(t => t.ErrorMessage).Aggregate((i, j) => i + "\n" + j);
            }
        }
        private string ValidationMessage { get; set; }

        bool IsNestedValidate(PropertyInfo type)
        {
            object[] attributes = type.GetCustomAttributes(typeof(ValidateNestedAttribute), false);
            return attributes.Length > 0;

        }

        public KeyValuePair<bool, string> Validate<T>(T entity)
        {
            var context = new ValidationContext(entity, serviceProvider: null, items: null);
            ValidationResults = new List<ValidationResult>();
            List<ValidationResult> results = new List<ValidationResult>();
            bool result = true;
            if (!Validator.TryValidateObject(entity, context, results, validateAllProperties: true))
                result = false;
            ValidationResults = ValidationResults.Union(results).ToList();

            Type oType = typeof(T);
            PropertyInfo[] members = oType.GetProperties();
            foreach (PropertyInfo member in members)
            {
                if (IsNestedValidate(member))
                {
                    oType = member.PropertyType;
                    if (typeof(ICollection).IsAssignableFrom(member.PropertyType))
                    {
                        object ourlist = member.GetValue(entity, null);
                        int i = 1;
                        foreach (object obj in ourlist as ICollection)
                        {
                            context = new ValidationContext(obj, serviceProvider: null, items: null);
                            results = new List<ValidationResult>();
                            if (!Validator.TryValidateObject(obj, context, results, validateAllProperties: true))
                                result = false;
                            results.ForEach(p => p.ErrorMessage = "{" + (i) + " record}=" + p.ErrorMessage);
                            ValidationResults = ValidationResults.Union(results).ToList();
                            i++;
                        }
                    }
                    else
                    {
                        object obj = member.GetValue(entity, null);
                        context = new ValidationContext(obj, serviceProvider: null, items: null);
                        results = new List<ValidationResult>();
                        if (Validator.TryValidateObject(obj, context, results, validateAllProperties: true))
                            result = false;
                        ValidationResults = ValidationResults.Union(results).ToList();
                    }
                }
            }
            return new KeyValuePair<bool, string>(result, this.ValidationMessage);
        }

        public KeyValuePair<bool, string> Validate<T>(List<T> entities)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationResults = new List<ValidationResult>();
            int i = 1;
            bool result = true;
            foreach (T obj in entities)
            {
                var context = new ValidationContext(obj, serviceProvider: null, items: null);
                results = new List<ValidationResult>();
                if (!Validator.TryValidateObject(obj, context, results, validateAllProperties: true))
                    result = false;
                results.ForEach(p => p.ErrorMessage = "{" + (i) + " record}=" + p.ErrorMessage);
                ValidationResults = ValidationResults.Union(results).ToList();
                i++;
            }
            return new KeyValuePair<bool, string>(result, this.ValidationMessage);
        }
    }
}
