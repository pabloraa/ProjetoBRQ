using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Models;

namespace WebApiModels.Models.Validacao
{
    public class ContaValidator : AbstractValidator<Conta>
    {
        public ContaValidator()
        {
            RuleFor(x => x.Agencia)
                .NotNull().WithMessage("Agência não pode ser nulo!")
                .GreaterThan(0).WithMessage("Agência não pode ser negativa!")
                .NotEmpty().WithMessage("Agência não pode ser nula!");
            RuleFor(x => x.NumeroConta)
                .NotNull().WithMessage("O número da conta não pode ser nulo!")
                .GreaterThan(0).WithMessage("O número da conta não pode ser negativo!")
                .NotEmpty().WithMessage("O número da conta não pode ser nulo!");
            RuleFor(x => x.Ativo)
                .NotNull().WithMessage("Conta ativa = true / Conta inativa = false");
        }
    }
}
