using Dominio.Validacao;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApi.Models.Enums;

namespace WebApi.Models
{
    public class Transacao
    {
        public string Id { get; set; }

        public string ContaId { get; set; }
        
        public DateTime DataTransacao { get; set; }
        
        [ValorMaiorQueZero]
        public double ValorTransacao { get; set; }

        public TipoTransacao Tipo { get; set; }

        public string Descricao { get; set; }
    }
}