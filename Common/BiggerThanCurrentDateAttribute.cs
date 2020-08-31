using System;
using System.ComponentModel.DataAnnotations;

namespace dashboard.Common
{
    public class BiggerThanCurrentDateAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "Het onderzoek moet vandaag of ergens in de toekomst plaatsvinden. ";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dateTime = Convert.ToDateTime(value);
            if (dateTime.CompareTo(DateTime.Now) < 0)
              { 
                return new ValidationResult(ErrorMessage ?? DefaultErrorMessage);
            }
            else return ValidationResult.Success;
        }
    }
}