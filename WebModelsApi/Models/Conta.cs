using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class Conta
    {
        public Guid Id { get; set; }

        public List<Transacao> Transacoes { get; set; }

        public Guid IdCliente { get; set; }

        public int Agencia { get; set; }

        public int NumeroConta { get; set; }

        public DateTime DataAbertura { get; set; }

        public DateTime? DataEncerramento { get; set; }

        public bool Ativo  { get; set; }
    }
}