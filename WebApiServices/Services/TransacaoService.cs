using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.DataBaseConection;
using WebApi.Models;
using WebApiModels.Models.Enums;
using WebServiceApi.Interfaces;

namespace WebServiceApi.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ApiContext _context;
               

        public TransacaoService(ApiContext apiContext)
        {
            _context = apiContext;
           
        }
        public Transacao VerificarTransacao(Transacao transacao)
        {
            var verificarTransacao = _context.Transacoes.
                FirstOrDefault(conta => conta.Id.Equals(transacao.ContaId));
            return verificarTransacao;
        }

        public Transacao VerificarPorId(string id) 
        {
            var verificaTransacao = _context.Transacoes.
                FirstOrDefault(conta => conta.Id.Equals(id));
            return verificaTransacao;
        }

        public List<Transacao> BuscarTransacoes()
        {
            return _context.Transacoes.ToList();
        }

        public List<Transacao> BuscarTransacoesPorIdConta(string idConta)
        {
            var verificaTransacao = _context.Transacoes.
                Where(transacao => transacao.ContaId.Equals(idConta)).ToList();
            return verificaTransacao;
        }

        public ResultadoTransacoes BuscaTransacoesPorId(string id)
        {
            var buscaTransacoes = _context.Transacoes.Where(transacao => transacao.Id.Equals(id));
            if (buscaTransacoes is null)
                return ResultadoTransacoes.NaoEncontrado;
            return ResultadoTransacoes.Ok;
        }
        public Transacao Create(Transacao transacao)
        {
            if (transacao is null)
                return null;
            transacao.Id = Guid.NewGuid().ToString();
            transacao.DataTransacao = DateTime.Now;
            _context.Transacoes.Add(transacao);
            _context.SaveChanges();
            return transacao;
        }
    }
}
