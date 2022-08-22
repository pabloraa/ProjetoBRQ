using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApi.DataBaseConection;
using WebApi.Models;
using WebApiModels.Models.Enums;

namespace WebServiceApi.Interfaces
{
    public interface ITransacaoService
    {
        public Task<List<Transacao>> BuscarTransacoesPorIdConta(string idConta);

        public Task<Transacao> VerificarTransacao(Transacao transacao);

        public Task<ResultadoTransacoes> BuscaTransacoesPorId(string id);

        public Task<Transacao> Create(Transacao transacao);

        public Task<List<Transacao>> BuscarTransacoes();

        public Task<Transacao> VerificarPorId(string id);

        public Task<Transacao> Atualizar(string id, Transacao transacao);
    }
}
