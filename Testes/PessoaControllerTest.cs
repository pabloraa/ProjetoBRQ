using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Recursos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Models;
using WebApiModels.Models.Enums;
using WebServiceApi.Interfaces;
using Xunit;

namespace Testes
{
    public class PessoaControllerTest
    {
        private readonly PessoaController _pessoaController;
        private readonly IPessoaService _pessoaService;
        private readonly IContaService _contaService;
        private readonly ITransacaoService _transacaoService;
        

        public PessoaControllerTest()
        {
            _pessoaService = Substitute.For<IPessoaService>();
            _contaService = Substitute.For<IContaService>();
            _transacaoService = Substitute.For<ITransacaoService>();
            _pessoaController = new PessoaController(_pessoaService,_contaService,_transacaoService);
        }

        [Fact]
        public async Task CreatePessoaOk()
        {
            //configurar
            var pessoa = InstanciarUmaPessoa();
            var esperado = InstanciarUmaPessoa();
            _pessoaService.Create(pessoa).Returns(esperado);
            //executar
            var response = (CreatedResult)await _pessoaController.Create(pessoa);

            //validar
            Assert.NotNull(response);
            Assert.Equal(201,response.StatusCode.GetValueOrDefault());
            Assert.Equal(esperado,response.Value);

        }

        [Fact]
        public async Task GetAllOk()
        {

            //configurar


            //executar
            var response = (OkObjectResult)await _pessoaController.GetAll();

            //validar

            Assert.NotNull(response);
            Assert.Equal(200, response.StatusCode.GetValueOrDefault());

        }

        [Fact]
        public async Task GetByPessoaIdOk()
        {
            //configurar
            string id = "";
            var pessoaEncontrada = InstanciarUmaPessoa();
            _pessoaService.BuscarPorId(id).Returns(pessoaEncontrada);

            //executar
            var response = (OkObjectResult)await _pessoaController.BuscaPorId(id);

            //validar

            Assert.NotNull(response);
            Assert.Equal(200, response.StatusCode.GetValueOrDefault());
            Assert.Equal(pessoaEncontrada, response.Value);
        }

        [Fact]
        public async Task GetByPessoaIdNotFound()
        {
            //configurar
            string id = "";
            _pessoaService.BuscarPorId(id).ReturnsNull();

            //executar
            var response = (NotFoundObjectResult)await _pessoaController.BuscaPorId(id);

            //validar
            Assert.NotNull(response);
            Assert.Equal(404, response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.ClienteNaoEncontrado, response.Value);

        }

        [Fact]
        public async Task DeletarPorIdNotFound() //erro
        {
            //configurar
            string id = "";
            _pessoaService.DeletarPorId(id).Returns(Resultadoservice.NaoEncontrado);

            //executar
            var response = (NotFoundObjectResult)await _pessoaController.DeletarPorId(id);

            //validar

            Assert.NotNull(response);
            Assert.Equal(404,response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.ClienteNaoEncontrado,response.Value);
        }

        [Fact]
        public async Task DeletarPorIdOk()
        {
            //configurar
            string id = "";
            var pessoaEncontrada = InstanciarUmaPessoa();
            _pessoaService.DeletarPorId(id).Returns(Resultadoservice.NaoPodeExcluir);
            //executar
            var response = (OkObjectResult)await _pessoaController.DeletarPorId(id);

            //validar
            Assert.NotNull(response);
            Assert.Equal(200,response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.NaoPodeExcluir,response.Value);
        }

        [Fact]
        public async Task AtualizarPessoaOk()
        {
            //configurar
            string id = "";
            var pessoaEncontrada = InstanciarUmaPessoa();
            _pessoaService.AtualizarPessoa(id, pessoaEncontrada).Returns(pessoaEncontrada);

            //executar
            var response = (OkObjectResult)await _pessoaController.AtualizarPessoa(id, pessoaEncontrada);
            //validar

            Assert.NotNull(response);
            Assert.Equal(200,response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.PessoaAtualizada, response.Value);
        }


        [Fact]
        public async Task AtualizarPessoaNotFound()
        {
            //configurar
            string id = "";
            var pessoaEncontrada = InstanciarUmaPessoa();
            _pessoaService.AtualizarPessoa(id, pessoaEncontrada).ReturnsNull();
            //executar
            var response = (NotFoundObjectResult)await _pessoaController.AtualizarPessoa(id, pessoaEncontrada);

            //validar
            Assert.NotNull(response);
            Assert.Equal(404,response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.PessoaNaoEncontrada,response.Value);
        }
        public Pessoa InstanciarUmaPessoa()
        {
            Pessoa pessoa = new Pessoa();
            pessoa.Id = "c9af33fd-a954-4988-94e9-b2832bcc2358";
            pessoa.Cpf = "xxx";
            pessoa.Nome = "yyy";

            return pessoa;
        }
    }
}