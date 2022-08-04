using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Pessoa
    {
        public Guid PessoaId { get; set; }

        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }         

        public string Cpf { get; set; }

        public List<Conta> Contas { get; set; }
    }
}