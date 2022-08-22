using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;
using WebApiModels.Models.Enums;

namespace WebServiceApi.Interfaces
{
    public interface IContaService
    {
        public Task<Conta> Create(Conta conta);

        public Task<Conta> BuscarContaPorIdCliente(string idCliente);

        public Task<Conta> BuscarContaPorIdConta(string idConta);

        public Task<Conta> BuscarContaPorAgenciaENumero(int agencia, int numeroConta);

        public Task<Resultadoservice> DeletarContaPorId(string id);

        public Task<Conta> AtualizarPorId(string id, Conta conta);

        public Task<List<Conta>> BuscarContasPorPessoaId(string id);

        public Task<List<Conta>> GetAll();

        public Task<Conta> BuscaPorAgenciaConta(int agencia, int numero);

    }
}
