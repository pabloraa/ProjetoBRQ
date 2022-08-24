using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using WebApi.Models;

namespace WebApiModels.Models.Validacao
{
    public class ContaValidator : AbstractValidator<Conta>
    {
        private const string expressao = @"[0-9]";
        private const string expressao2 = @"[^(\d*\d{4})$]";

        public ContaValidator()
        {
            RuleFor(x => x.Agencia)
                .NotNull().WithMessage("Agência não pode ser nulo!")
                .GreaterThan(0).WithMessage("Agência não pode ser negativa!")
                .Must(ValidarNumeros).WithMessage("Somente número são aceitos")
                .NotEmpty().WithMessage("Agência não pode ser nula!");
            RuleFor(x => x.NumeroConta)
                .NotNull().WithMessage("O número da conta não pode ser nulo!")
                .GreaterThan(0).WithMessage("O número da conta não pode ser negativo!")
                .Must(ValidarNumeros).WithMessage("Somente números são aceitos")
                .NotEmpty().WithMessage("O número da conta não pode ser nulo!");
            RuleFor(x => x.Ativo)
                .NotNull().WithMessage("Conta ativa = true / Conta inativa = false");
        }

        public static bool ValidarNumeros(int numero)
        {
            Regex num = new Regex(expressao);
            Regex num2 = new Regex(expressao2);

            //if (numero.Equals(num))
            //    return false;
            //return true;

            //return numero.Equals(num)?false:true; //operador ternário
            return numero.Equals(num2) ? false : true;
            //return (new Regex(expressao).IsMatch(numero.ToString()));
        }
    }
}
