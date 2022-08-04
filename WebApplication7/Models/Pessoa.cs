using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class Pessoa
    {
        public int PessoaId { get; set; }

        public string Nome { get; set; }

        public string Tipo { get; set; }

        public string Data_Nascimento { get; set; }

        public string Profissao { get; set; }

        public string Conta_Corrente { get; set; }

        public string Agencia { get; set; }

        public string Conta_destino { get; set; }

        public string Id_Cliente { get; set; }

        public string Cpf { get; set; }

        public string Rg { get; set; }

        public string Extrato { get; set; }

        public string Saldo { get; set; }

    }
}