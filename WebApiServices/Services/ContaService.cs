using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private void GerarNovasCredenciaisDeConta(Conta conta)
        {
            conta.Id = Guid.NewGuid().ToString();
            conta.DataAbertura = DateTime.Now;
            conta.DataEncerramento = null;
            conta.Transacoes = new List<Transacao>();
            conta.Ativo = true;
        }

        public Conta Create(Conta conta)
        {
            GerarNovasCredenciaisDeConta(conta);
            _context.Contas.Add(conta);
            _context.SaveChanges();
            return conta;
        }

        public List<Conta> BuscarContasPorPessoaId(string id)
        {
            var contas = _context.Contas.Where(x => x.PessoaId.Equals(id)).ToList();
            return contas;
        }


        public Conta BuscarContaPorIdCliente(string idCliente)
        {
            var contaEncontrada = _context.Contas.FirstOrDefault(x => x.PessoaId.Equals(idCliente));
            if (contaEncontrada is null)
            {
                return null;
            }
            contaEncontrada.Transacoes = _transacaoService.BuscarTransacoesPorIdConta(contaEncontrada.Id);
            return contaEncontrada;
        }

        public Conta BuscarContaPorIdConta(string idConta)
        {
            var contaEncontrada = _context.Contas.FirstOrDefault(x => x.Id.Equals(idConta));
            if (contaEncontrada is null)
            {
                return null;
            }
            contaEncontrada.Transacoes = _transacaoService.BuscarTransacoesPorIdConta(contaEncontrada.Id);
            return contaEncontrada;
        }

        public Conta BuscarContaPorAgenciaENumero(int agencia, int numeroConta)
        {
            var contaEncontrada = _context.Contas.
                FirstOrDefault(c => c.Agencia == agencia && c.NumeroConta == numeroConta);

            if (contaEncontrada is null)
            {
                return null;
            }
            contaEncontrada.Transacoes = _transacaoService.BuscarTransacoesPorIdConta(contaEncontrada.Id);
            return contaEncontrada;
        }

        public Resultadoservice DeletarContaPorId(string id)
        {
            var contaEncontrada = _context.Contas.FirstOrDefault(p => p.Id.Equals(id));
            if (contaEncontrada is null)
            {
                return Resultadoservice.NaoEncontrado;
            }

            _context.Contas.Remove(contaEncontrada);
            _context.SaveChanges();

            return Resultadoservice.Ok;
        }

        public Conta AtualizarPorId(string id, Conta conta)
        {
            var contaEncontrada = _context.Contas.FirstOrDefault(c => c.Id.Equals(id));

            if (contaEncontrada is null)
                return null;

            contaEncontrada.DataEncerramento = conta.DataEncerramento;
            contaEncontrada.Ativo = conta.Ativo;
            _context.Entry(contaEncontrada).State = EntityState.Modified;
            _context.SaveChanges();
            return contaEncontrada;
        }

        public Resultadoservice BuscaPorConta(int agencia, int conta)
        {
            var contaEncontrada = _context.Contas.FirstOrDefault(c => c.NumeroConta.Equals(conta));

            if (contaEncontrada is null)
                return Resultadoservice.NaoEncontrado;

            return Resultadoservice.Ok;
        }

        public Resultadoservice BuscarPorIdConta(string id)
        {
            var contaEncontrada = _context.Contas.FirstOrDefault();

            if (contaEncontrada is null)
                return Resultadoservice.NaoEncontrado;

            return Resultadoservice.Ok;
        }

        public Conta BuscaPorAgenciaConta(int agencia, int numero) // não faça alterações aqui...
        {
            var conta = _context.Contas.
                FirstOrDefault(c => c.Agencia == agencia && c.NumeroConta == numero);
            return conta;
        }
    }
}
