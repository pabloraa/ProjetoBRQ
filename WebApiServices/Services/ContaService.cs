using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.DataBaseConection;
using WebApi.Models;
using WebApiModels.Models.Enums;
using WebServiceApi.Interfaces;

namespace WebServiceApi.Services
{
    public class ContaService : IContaService
    {

        private readonly ApiContext _context;
        private readonly ITransacaoService _transacaoService;

        public ContaService(ApiContext apiContext, ITransacaoService transacaoService)
        {
            _context = apiContext;
            _transacaoService = transacaoService;
        }

        public async Task<List<Conta>> GetAll()
        {
            return await _context.Contas.ToListAsync();
        }

        private void GerarNovasCredenciaisDeConta(Conta conta)
        {
            conta.Id = Guid.NewGuid().ToString();
            conta.DataAbertura = DateTime.Now;
            conta.DataEncerramento = null;
            conta.Transacoes = new List<Transacao>();
            conta.Ativo = true;
        }

        public async Task<Conta> Create(Conta conta) //assíncrono
        {
            GerarNovasCredenciaisDeConta(conta);
            await _context.Contas.AddAsync(conta);
            await _context.SaveChangesAsync();
            return conta;
        }

        public async Task<List<Conta>> BuscarContasPorPessoaId(string id)
        {
            var contas = await _context.Contas.Where(x => x.PessoaId.Equals(id)).ToListAsync();
            return contas;
        }

        public async Task<Conta> BuscarContaPorIdCliente(string idCliente)
        {
            var contaEncontrada = await _context.Contas.FirstOrDefaultAsync(x => x.PessoaId.Equals(idCliente));
            if (contaEncontrada is null)
            {
                return null;
            }
            contaEncontrada.Transacoes = await _transacaoService.BuscarTransacoesPorIdConta(contaEncontrada.Id);
            return contaEncontrada;
        }

        public async Task<Conta> BuscarContaPorIdConta(string idConta)
        {
            var contaEncontrada = await _context.Contas.FirstOrDefaultAsync(x => x.Id.Equals(idConta));
            if (contaEncontrada is null)
            {
                return null;
            }
            contaEncontrada.Transacoes = await _transacaoService.BuscarTransacoesPorIdConta(contaEncontrada.Id);
            return contaEncontrada;
        }

        public async Task<Conta> BuscarContaPorAgenciaENumero(int agencia, int numeroConta)
        {
            var contaEncontrada = await _context.Contas.
                FirstOrDefaultAsync(c => c.Agencia == agencia && c.NumeroConta == numeroConta);

            if (contaEncontrada is null)
            {
                return null;
            }
            contaEncontrada.Transacoes = await _transacaoService.BuscarTransacoesPorIdConta(contaEncontrada.Id);
            return contaEncontrada;
        }

        public async Task<Resultadoservice> DeletarContaPorId(string id)
        {
            var contaEncontrada = await _context.Contas.FirstOrDefaultAsync(p => p.Id.Equals(id));
            if (contaEncontrada is null)
            {
                return Resultadoservice.NaoEncontrado;
            }

            _context.Contas.Remove(contaEncontrada);
            _context.SaveChanges();

            return Resultadoservice.Ok;
        }

        public async Task<Conta> AtualizarPorId(string id, Conta conta)
        {
            var contaEncontrada = await _context.Contas.FirstOrDefaultAsync(c => c.Id.Equals(id));

            if (contaEncontrada is null)
                return null;

            contaEncontrada.DataEncerramento = conta.DataEncerramento;
            contaEncontrada.Ativo = conta.Ativo;
            _context.Entry(contaEncontrada).State = EntityState.Modified;
            _context.SaveChanges();
            return contaEncontrada;
        }

        public async Task<Resultadoservice> BuscaPorConta(int agencia, int conta)
        {
            var contaEncontrada = await _context.Contas.FirstOrDefaultAsync(c => c.NumeroConta.Equals(conta));

            if (contaEncontrada is null)
                return Resultadoservice.NaoEncontrado;

            return Resultadoservice.Ok;
        }

        public async Task<Resultadoservice> BuscarPorIdConta(string id)
        {
            //Escrever código
            var contaEncontrada = await _context.Contas.FirstOrDefaultAsync(c => c.Id.Equals(id));

            if (contaEncontrada is null)
                return Resultadoservice.NaoEncontrado;

            return Resultadoservice.Ok;
        }

        public async Task<Conta> BuscaPorAgenciaConta(int agencia, int numero) // não faça alterações aqui...
        {
            var conta = await _context.Contas.
                FirstOrDefaultAsync(c => c.Agencia == agencia && c.NumeroConta == numero);
            return conta;
        }
    }
}
