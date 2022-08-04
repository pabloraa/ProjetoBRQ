using Recursos;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebApi.Models
{
    public class ValidarCpfAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value is null)
                return new ValidationResult(Mensagens.CpfNaoNulo); //O renam gosta assim

            var cpf = value.ToString();
            
            if (cpf.Length != 14)
                return new ValidationResult("Quantidade de caracteres diferente de 14!");
            if (!ValidaPosicaCaracter(cpf, 3, '.') || !ValidaPosicaCaracter(cpf, 7, '.'))
                return new ValidationResult("Ponto na posição inválida!");
            if (!ValidaPosicaCaracter(cpf, 11, '-'))
                return new ValidationResult("Traço na posição inválida!");            
            if (cpf.Where(c => char.IsNumber(c)).Count() != 11)
                return new ValidationResult("O Cpf tem que ter 11 números!");
            return ValidationResult.Success;
        }
        private bool ValidaPosicaCaracter(string cpf, int posicao, char c)
        {
            return cpf[posicao] == c;
        }
    }
}