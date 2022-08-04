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
    public class PessoaService : IPessoaService
    {
        private readonly ApiContext _context;
        private readonly IContaService _contaService;
        private readonly ITransacaoService _transacaoService;
        
        public PessoaService(ApiContext context, IContaService contaService, ITransacaoService transacaoService)
        {
            _context = context;
            _contaService = contaService;
            _transacaoService = transacaoService;
            
        }

        public Pessoa Create(Pessoa pessoa)
        {

            pessoa.Contas = new List<Conta>();

            pessoa.Id = Guid.NewGuid().ToString();
            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();

            return pessoa;
        }

        public Pessoa BuscaPorId(string id)
        {
            var pessoa = _context.Pessoas.FirstOrDefault(x => x.Id.Equals(id));

            return pessoa;
        }

        public Resultadoservice DeletarPorId(string id)
        {
            Pessoa p = BuscaPorId(id);

            if (p is null)
            {
                return Resultadoservice.NaoEncontrado;
            }

            var buscaContas = _contaService.BuscarContasPorPessoaId(id);
            p.Contas = buscaContas;

            if (p.Contas.Count > 0)
            {
                return Resultadoservice.NaoPodeExcluir;
            }

            _context.Pessoas.Remove(p);
            _context.SaveChanges();

            return Resultadoservice.Ok;
        }

        public Pessoa BuscarPorId(string id)
        {
            Pessoa p = BuscaPorId(id);

            if (p is null)
            {
                return null;
            }

            var listaContas = _contaService.BuscarContasPorPessoaId(p.Id);

            p.Contas = listaContas;

            foreach (var conta in listaContas)
            {
                var listaTransacoes = _transacaoService.BuscarTransacoesPorIdConta(conta.Id);
                conta.Transacoes = listaTransacoes;
            }

            return p;
        }

        public Pessoa ResultadoCriarPessoa(Pessoa pessoa)
        {
            Pessoa p = Create(pessoa);
            return p;
        }
    }
}
