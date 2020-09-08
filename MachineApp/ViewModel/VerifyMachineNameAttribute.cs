using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MachineApp.ViewModel
{
    public class VerifyMachineNameAttribute : ValidationAttribute, IClientModelValidator
    {
        public string MachineName { get; set; }
        
        public void AddValidation(ClientModelValidationContext context)
        {
           context.Attributes.Add("Malfunction.MachineName", FormatErrorMessage(context.ModelMetadata.GetDisplayName()));
        }
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            if((string)value != "Choose Machine...") 
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Please choose a machine!");
        }
    }
}