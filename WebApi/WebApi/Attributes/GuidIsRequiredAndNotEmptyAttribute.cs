using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class GuidIsRequiredAndNotEmptyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || (Guid)value == Guid.Empty) 
            {
                return new ValidationResult("Value is required");
            }
                
            return ValidationResult.Success;           
        }
    }
}
