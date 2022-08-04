using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.DataBaseConection;
using WebApi.Models;
using WebServiceApi.Interfaces;

namespace WebServiceApi.Services
{
    public class TransacaoService : ITransacaoService
    {
        private ApiContext _context;

        public Conta verificarConta(Transacao transacao)
        {
            var verificarConta = _context.Contas.
                FirstOrDefault(conta => conta.Id.Equals(transacao.IdConta));
            return verificarConta;
        }
    }
}
