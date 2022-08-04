using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;
using WebApi.Models;
using WebApiModels.Models.Enums;

namespace WebServiceApi.Interfaces
{
    public interface IContaService
    {
        public Conta BuscarContaPorIdCliente(string idCliente);

        public Conta BuscarContaPorIdConta(string idConta);

        public Conta BuscarContaPorAgenciaENumero(int agencia, int numeroConta);

        public List<Conta> BuscarContasPorPessoaId(string id);
        
        public Resultadoservice DeletarContaPorId(string id);

        public Conta BuscaPorAgenciaConta(int agencia, int numero);

        public Conta AtualizarPorId(string id, Conta conta);

        public Conta Create(Conta conta);
    }
}
