using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models.Enums;

namespace WebApi.Models
{
    public class Transacao
    {
        public Guid Id { get; set; }

        public Guid IdConta { get; set; }
        
        public DateTime DataTransacao { get; set; }

        public Decimal ValorTransacao { get; set; }

        public TipoTransacao Tipo { get; set; }

        public string Descricao { get; set; }
    }
}