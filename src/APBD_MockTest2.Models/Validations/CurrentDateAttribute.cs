using System.ComponentModel.DataAnnotations;

namespace ClassLibrary1.Validations;

[AttributeUsage(AttributeTargets.Property)]
public class CurrentDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null)
        {
            return false;
        }
        
        var dateValue = (DateTime)value;
        return dateValue <= DateTime.Now;
    }
}