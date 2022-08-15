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
        private const int menorIdade = -18;
        private const int anoMinimo = 1900;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null)
                return new ValidationResult(Mensagens.DataNascimentoNaoNula);
            DateTime resultado = DateTime.Parse(value.ToString());
            if (resultado.Year <= anoMinimo)
                return new ValidationResult(Mensagens.AnoNaoPermitido);
            if (resultado > DateTime.Now.AddYears(menorIdade))
                return new ValidationResult(Mensagens.MenorDeIdade);

            return ValidationResult.Success; 
        }
    }
}
