using FluentValidation;
using Recursos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using WebApi.Models;

namespace WebApiModels.Models.Validacao
{
    public class PessoaValidator : AbstractValidator<Pessoa>
    {
        private const string expressao2 = @"[0-9&_.\-@]"; //Regex para validar letras.

        public PessoaValidator()
        {
            RuleFor(x => x.Nome)
                .NotNull().WithMessage("O nome não pode ser nulo!")
                .NotEmpty().WithMessage("O nome não pode ser vazio")
                .Must(PrimeiraLetra).WithMessage("Primeira letra maiúscula")
                .Must(ValidadorLetras).WithMessage("Somente letras")
                .MaximumLength(20).WithMessage("O nome pode ter no máximo 20 caracteres!")
                .MinimumLength(5).WithMessage("O nome não pode ter menos que 5 cacacteres!");
        }

        public static bool PrimeiraLetra(string primeira)
        {
            return primeira[0].ToString() == primeira[0].ToString().ToUpper();
        }

        public static bool ValidadorLetras(string caractere)
        {
            return !(new Regex(expressao2).IsMatch(caractere));
        }
    }
}
