using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ArrayMustContainAtLeastTwoValuesAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Type valueType = value.GetType();

            if (value == null 
                || !valueType.IsArray 
                || !typeof(double).IsAssignableFrom(valueType.GetElementType())
                || ((double[])value).Length < 2)
            {
                return new ValidationResult("There needs to be at least 2 numbers, and they must be in the format [a, b.cc]");
            }

            return ValidationResult.Success;
        }
    }
}
