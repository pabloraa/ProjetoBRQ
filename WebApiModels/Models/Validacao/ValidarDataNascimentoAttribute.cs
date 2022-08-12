using Recursos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace WebApiModels.Models.Validacao
{
    public class ValidarDataNascimentoAttribute : ValidationAttribute 
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return new ValidationResult(Mensagens.DataNascimentoNaoNula);
            var data = value.ToString();
            DateTime resultado = DateTime.ParseExact(data,"yyyy-MM-dd",CultureInfo.InvariantCulture);
            var resultado2 = resultado.ToString();

            if (resultado2.Length <=10)
                return new ValidationResult("Quantidades de caracteres diferentes de 10");
            return ValidationResult.Success; 
        }
    }
}
