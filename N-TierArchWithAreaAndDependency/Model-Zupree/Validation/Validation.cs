using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Zupree.Validation
{
    public class ValidatePurchaseDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime CurrentDate = DateTime.Now;
            string message = string.Empty;
            if (Convert.ToDateTime(value) <= CurrentDate)
            {
                return ValidationResult.Success;
            }
            else
            {
                message = "Purchase Date must be samller than Current date";
                return new ValidationResult(message);
            }
        }
    }
    //public class GreaterThanAttribute : ValidationAttribute
    //{
    //    private readonly string _comparisonProperty;

    //    public GreaterThanAttribute(string comparisonProperty)
    //    {
    //        _comparisonProperty = comparisonProperty;
    //    }

    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        ErrorMessage = ErrorMessageString;
    //        int currentValue =Convert.ToInt32((IComparable)value);
    //        var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
    //        var comparisonValue =Convert.ToInt32(property.GetValue(validationContext.ObjectInstance));

    //        if (comparisonValue < currentValue)
    //        {
    //            return ValidationResult.Success;
    //        }
    //        else
    //        {
    //            return new ValidationResult(ErrorMessage);
    //        }
    //    }
    //}
}
