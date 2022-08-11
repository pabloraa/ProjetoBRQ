using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;
using WebApiModels.Models.Enums;

namespace WebServiceApi.Interfaces
{
    public interface IPessoaService
    {
        //public Pessoa Create(Pessoa pessoa);

        //public Pessoa BuscaPorId(string id);

        //public Resultadoservice DeletarPorId(string id);

        //public Pessoa BuscarPorId(string id);

        //public Pessoa ResultadoCriarPessoa(Pessoa pessoa);

        public Task<Pessoa> Create(Pessoa pessoa);

        public Task<Pessoa> BuscaPorId(string id);

        public Task<Resultadoservice> DeletarPorId(string id);

        public Task<Pessoa> BuscarPorId(string id);

        public Task<Pessoa> ResultadoCriarPessoa(Pessoa pessoa);

        public Task<List<Pessoa>> GetAll();
    }
}
