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
    public class TransacaoService : ITransacaoService
    {
        private readonly ApiContext _context;
               

        public TransacaoService(ApiContext apiContext)
        {
            _context = apiContext;
           
        }

        public async Task<Transacao> VerificarTransacao(Transacao transacao)
        {
            var verificarTransacao = await _context.Transacoes.
                FirstOrDefaultAsync(conta => conta.Id.Equals(transacao.ContaId));
            return verificarTransacao;
        }

        public async Task<Transacao> VerificarPorId(string id) 
        {
            var verificaTransacao = await _context.Transacoes.
                FirstOrDefaultAsync(conta => conta.Id.Equals(id));
            return verificaTransacao;
        }

        public async Task<List<Transacao>> BuscarTransacoes()
        {
            return await _context.Transacoes.ToListAsync();
        }

        public async Task<List<Transacao>> BuscarTransacoesPorIdConta(string idConta)
        {
            var verificaTransacao = await _context.Transacoes.
                Where(transacao => transacao.ContaId.Equals(idConta)).ToListAsync();
            return verificaTransacao;
        }

        public async Task<ResultadoTransacoes> BuscaTransacoesPorId(string id)
        {
            var buscaTransacoes = await _context.Transacoes.Where(transacao => transacao.Id.Equals(id)).ToListAsync();
            if (buscaTransacoes.Count == 0)
                return ResultadoTransacoes.NaoEncontrado;
            return ResultadoTransacoes.Ok;
        }
        public async Task<Transacao> Create(Transacao transacao)
        {
            if (transacao is null)
                return null;
            transacao.Id = Guid.NewGuid().ToString();
            transacao.DataTransacao = DateTime.Now;
            await _context.Transacoes.AddAsync(transacao);
            await _context.SaveChangesAsync();
            return transacao;
        }

        public async Task<Transacao> Atualizar(string id, Transacao transacao)
        {
            var transasaoEncontrada = await _context.Transacoes.FirstOrDefaultAsync(t => t.Id.Equals(id));
            if (transasaoEncontrada is null)
                return null;
            transasaoEncontrada.Descricao = transacao.Descricao;
            transasaoEncontrada.ValorTransacao = transacao.ValorTransacao;
            transasaoEncontrada.DataTransacao = transacao.DataTransacao;

            _context.Entry(transasaoEncontrada).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return transasaoEncontrada;
        }
    }
}
