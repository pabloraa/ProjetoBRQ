using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Models;

namespace WebApiModels.Models.Validacao
{
    public class PessoaValidator : AbstractValidator<Pessoa>
    {
        public PessoaValidator()
        {
            RuleFor(x => x.Nome)
                .NotNull().WithMessage("O nome não pode ser nulo")
                .MaximumLength(20).WithMessage("O nome pode ter no máximo 20 caracteres!")
                .MinimumLength(5).WithMessage("O nome não pode ter menos que 5 cacacteres!");
        }
    }
}
