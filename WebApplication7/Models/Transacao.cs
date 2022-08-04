using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication7.Models.Enums;

namespace WebApplication7.Models
{
    public class Transacao
    {
        public Guid Id { get; set; }

        public Guid IdDonoTransacao { get; set; }
        
        public DateTime DataTransacao { get; set; }

        public Decimal ValorTransacao { get; set; }

        public TipoTransacao Tipo { get; set; }

        public string Descricao { get; set; }
    }
}