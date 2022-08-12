using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiModels.Models.Validacao;

namespace WebApi.Models
{
    public class Pessoa
    {
        public string Id { get; set; }

        public string Nome { get; set; }

        [ValidarDataNascimento]
        public DateTime DataNascimento { get; set; }         

        [ValidarCpf]
        public string Cpf { get; set; }

        public List<Conta> Contas { get; set; }

        public override string ToString()
        {
            return "Id" + Id + 
                "Nome" + Nome;
        }
    }
}