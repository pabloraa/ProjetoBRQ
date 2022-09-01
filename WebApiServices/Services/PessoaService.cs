using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<List<Pessoa>> GetAll()
        {
            return await _context.Pessoas.ToListAsync();
        }

        public async Task<Pessoa> Create(Pessoa pessoa)
        {

            pessoa.Contas = new List<Conta>();

            pessoa.Id = Guid.NewGuid().ToString();
            await _context.Pessoas.AddAsync(pessoa);
            await _context.SaveChangesAsync();

            return pessoa;
        }

        public async Task<Pessoa> BuscaPorId(string id)
        {
            var pessoa = await _context.Pessoas.FirstOrDefaultAsync(x => x.Id.Equals(id));

            return pessoa;
        }

        public async Task<Resultadoservice> DeletarPorId(string id)
        {
            Pessoa p = await BuscaPorId(id);

            if (p is null)
            {
                return Resultadoservice.NaoEncontrado;
            }

            var buscaContas = await _contaService.BuscarContasPorPessoaId(id);
            p.Contas = buscaContas;

            if (p.Contas.Count > 0)
            {
                return Resultadoservice.NaoPodeExcluir;
            }

            _context.Pessoas.Remove(p);
            await _context.SaveChangesAsync();

            return Resultadoservice.Ok;
        }

        public async Task<Pessoa> BuscarPorId(string id)
        {
            Pessoa p = await BuscaPorId(id);

            if (p is null)
            {
                return null;
            }

            var listaContas = await _contaService.BuscarContasPorPessoaId(p.Id);

            p.Contas = listaContas;

            foreach (var conta in listaContas)
            {
                var listaTransacoes = await _transacaoService.BuscarTransacoesPorIdConta(conta.Id);
                conta.Transacoes = listaTransacoes;
            }

            return p;
        }

        public async Task<Pessoa> ResultadoCriarPessoa(Pessoa pessoa)
        {
            Pessoa p = await Create(pessoa);
            return p;
        }

        public async Task<Pessoa> AtualizarPessoa(string id, Pessoa pessoa)
        {
            var pessoaEncontrada = await _context.Pessoas.FirstOrDefaultAsync(
                p=>p.Id.Equals(id));
            if (pessoaEncontrada is null)
                return null;
            pessoaEncontrada.Nome = pessoa.Nome;
            pessoaEncontrada.DataNascimento = pessoa.DataNascimento;
            pessoaEncontrada.Cpf = pessoa.Cpf;
            _context.Entry(pessoaEncontrada).State = EntityState.Modified;
            _context.SaveChanges();
            return pessoaEncontrada;
        }
    }
}
