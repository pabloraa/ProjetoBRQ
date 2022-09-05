using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.DataBaseConection;
using WebApi.Models;
using WebApiModels.Models.Enums;
using WebServiceApi.Interfaces;
using WebServiceApi.Services;
using Xunit;

namespace Testes
{
    public class PessoaServiceTest
    {
        private readonly IPessoaService _pessoaService;
        private readonly IContaService _contaService;
        private readonly ITransacaoService _transacaoService;
        private readonly ApiContext _apiContext;
       
        public PessoaServiceTest()
        {
            _transacaoService = Substitute.For<ITransacaoService>();
            _contaService = Substitute.For<IContaService>();

            _apiContext = Substitute.For<ApiContext>();

            _pessoaService = new PessoaService(_apiContext,_contaService,_transacaoService);
        }

        [Fact]
        public async Task GetAll()
        {
            //configurar
            _apiContext.Pessoas.ToListAsync().Returns(new List<Pessoa>());
            //executar 
            var response = await _pessoaService.GetAll();

            //validar
            Assert.Empty(response);
            Assert.NotNull(response);
            Assert.IsType<List<Pessoa>>(response);
        } 
        
        [Fact]
        public async Task BuscaPorId()
        {
            //configurar
            string id = "";
            var pessoaEncontrada = InstanciarPessoa();
            _apiContext.Pessoas.FirstOrDefaultAsync(x => x.Id.Equals(id)).Returns(pessoaEncontrada);

            //executar
            var response = await _pessoaService.BuscaPorId(id);

            //validar

            Assert.NotNull(response);
        }
        
        [Fact]
        public async Task DeletarPorIdOk() //duvida
        {
            //configurar
            string id = "";
            Pessoa p = new Pessoa();
            var pessoaEncontrada = InstanciarPessoa();
            _apiContext.Pessoas.Remove(p);

            //executar
            var response = await _pessoaService.DeletarPorId(id);

            //validar
            Assert.Equal(Resultadoservice.Ok,response);
                        
        }

        [Fact]
        public async Task DeletarPorIdNaoEncontrado()
        {
            //configurar
            string id = "";
            var pessoaEncontrada = InstanciarPessoa();
            _apiContext.Pessoas.FirstOrDefaultAsync(x => x.Id.Equals(id)).Returns(pessoaEncontrada);
            //executar
            var response = await _pessoaService.DeletarPorId(id);

            //validar
            Assert.Equal( Resultadoservice.NaoEncontrado, response);
        }

        [Fact]
        public async Task DeletarPorIdNaoPodeExcluir()
        {
            //configurar
            string id = "";
            var pessoaEncontrada = InstanciarPessoa();
            _apiContext.Pessoas.FirstOrDefault(x => x.Id.Equals(id)).Returns(pessoaEncontrada);
            //executar
            var response = await _pessoaService.DeletarPorId(id);

            //validar
            Assert.Equal(Resultadoservice.NaoPodeExcluir, response);
        }

        [Fact]
        public async Task BuscarPorIdNull()
        {
            //configurar
            string id = "";
            var pessoaEncontrada = InstanciarPessoa();
            _apiContext.Pessoas.FirstOrDefaultAsync(x => x.Id.Equals(id)).Returns(pessoaEncontrada);
            //executar
            var response = await _pessoaService.BuscarPorId(id);
            //validar
            Assert.NotNull(response);
            Assert.Equal(pessoaEncontrada,response);
        }

        [Fact]
        public async Task BuscarPorIdP()
        {
            //configurar
            string id = "";
            var pessoaEncontrada = InstanciarPessoa();
            List<Conta> c = new List<Conta>();
            _contaService.BuscarContasPorPessoaId(id).Returns(c);
            //executar
            var response = await _contaService.BuscarContasPorPessoaId(id);

            //validar
            Assert.NotNull(response);
            Assert.Equal(c,response);
        }

        [Fact]
        public async Task ResultadoCriarPessoa()
        {
            //configurar
            Pessoa p = new Pessoa();
            var pessoaCriada = InstanciarPessoa();
            
            //executar
            var response = await _pessoaService.ResultadoCriarPessoa(p);

            //validar
            Assert.NotNull(response);
            Assert.Equal(pessoaCriada,response);
        }

        [Fact]
        public async Task AtualizarPessoaNull() //incompleto
        {
            //configurar
            string id = "";
            var pessoaEncontrada = InstanciarPessoa();
            Pessoa p = new Pessoa();
            _apiContext.Pessoas.FirstOrDefaultAsync(p => p.Id.Equals(id)).Returns(p);
            //executar
            var response = await _pessoaService.AtualizarPessoa(id,p);

            //validar
            Assert.NotNull(response);
            Assert.Equal(pessoaEncontrada,response);
        }
        public Pessoa InstanciarPessoa()
        {
            Pessoa p = new Pessoa();
            p.Nome = "Zé mingau";
            p.DataNascimento = DateTime.Now;
            p.Cpf = "123.456.789-01";

            return p;
        }
    }
}
