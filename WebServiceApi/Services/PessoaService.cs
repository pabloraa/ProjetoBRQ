using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.DataBaseConection;
using WebApi.Models;
using WebServiceApi.Interfaces;

namespace WebServiceApi.Services
{
    public class PessoaService : IPessoaService
    {
        private ApiContext _context;
        //private PessoaRepository _pessoaRepository;

        public PessoaService(ApiContext context)
        {
            _context = context;
        }

        public Pessoa create(Pessoa pessoa)
        {

            pessoa.Contas = new List<Conta>();

            pessoa.PessoaId = Guid.NewGuid();
            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();

            return pessoa;
        }

        public Pessoa BuscaPorId(Guid id)
        {
            var pessoa = _context.Pessoas.FirstOrDefault(x => x.PessoaId.Equals(id));

            return pessoa;
        }

        public List<Conta> contasEncontradas(Pessoa p)
        {
            var contasEncontradas = _context.Contas.
            Where(c => c.IdCliente.Equals(p.PessoaId)).ToList();
            p.Contas = contasEncontradas;

            return contasEncontradas;
        }

        public List<Transacao> transacaoEncontrada(Conta conta)
        {
            var transacoesEncontradas = _context.Transacoes.
                Where(t => t.IdConta.Equals(conta.Id)).ToList();
            conta.Transacoes = transacoesEncontradas;

            return transacoesEncontradas;
        }
    }
}
