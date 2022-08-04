using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dominio.Validacao
{
    public class ValorMaiorQueZeroAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var numero = Convert.ToDouble(value);
            if (numero > 0)
                return ValidationResult.Success;
            return new ValidationResult("Somente valores maiores que zero!") ;
        }
    }
}
