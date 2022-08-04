using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.DataBaseConection;
using WebApi.Models;
using WebServiceApi.Interfaces;

namespace WebServiceApi.Services
{
    public class ContaService : IContaService
    {

        private ApiContext _context;

        public void GerarNovasCredenciaisDeConta(Conta conta)
        {
            conta.Id = Guid.NewGuid();
            conta.DataAbertura = DateTime.Now;
            conta.DataEncerramento = null;
            conta.Transacoes = new List<Transacao>();
            conta.Ativo = true;
        }

        public Conta BuscarContaPorIdCliente(Guid idCliente)
        {
            var contaEncontrada = _context.Contas.FirstOrDefault(x => x.IdCliente.Equals(idCliente));
            if (contaEncontrada is null)
            {
                return null;
            }
            contaEncontrada.Transacoes = BuscarTransacoesPorIdConta(contaEncontrada.Id);
            return contaEncontrada;
        }

        public List<Transacao> BuscarTransacoesPorIdConta(Guid idConta)
        {
            return _context.Transacoes.Where(t => t.IdConta.Equals(idConta)).ToList();
        }

        public Conta BuscarContaPorIdConta(Guid idConta)
        {
            var contaEncontrada = _context.Contas.FirstOrDefault(x => x.Id.Equals(idConta));
            if (contaEncontrada is null)
            {
                return null;
            }
            contaEncontrada.Transacoes = BuscarTransacoesPorIdConta(contaEncontrada.Id);
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
            contaEncontrada.Transacoes = BuscarTransacoesPorIdConta(contaEncontrada.Id);
            return contaEncontrada;
        }
    }
}
