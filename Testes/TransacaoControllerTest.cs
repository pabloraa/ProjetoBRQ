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
    public class TransacaoControllerTest
    {
        private readonly TransacoesController _transacoesController;
        private readonly ITransacaoService _transacaoService;

        public TransacaoControllerTest()
        { 
            _transacaoService = Substitute.For<ITransacaoService>();
            _transacoesController = new TransacoesController(_transacaoService);
        }

        [Fact]
        public async Task GetAllOk()
        {
            //configurar
            //executar
            var response = (OkObjectResult)await _transacoesController.GetAll();

            //validar
            Assert.NotNull(response);
            Assert.Equal(200,response.StatusCode.GetValueOrDefault());
        }

        [Fact]
        public async Task GetByIdNotFound()
        {
            //configurar
            string id = "";
            List<Transacao> transacao = new List<Transacao>();
            _transacaoService.BuscarTransacoesPorIdConta(id).Returns(transacao);

            //executar
            var response = (NotFoundObjectResult)await _transacoesController.GetById(id);

            //validar
            Assert.NotNull(response);
            Assert.Equal(404,response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.TransacaoNaoEncontrada, response.Value);
        }

        [Fact]
        public async Task GetByIdOk()
        {
            //configurar
            string id = "";
            List<Transacao> transacao = new List<Transacao>();
            transacao.Add(new Transacao());

            var transacoesEsperadas = transacao;
            _transacaoService.BuscarTransacoesPorIdConta(id).Returns(transacao);

            //executar
            var response = (OkObjectResult)await _transacoesController.GetById(id);
            //validar
            Assert.NotNull(response);
            Assert.Equal(200,response.StatusCode.GetValueOrDefault());
            Assert.Equal(transacoesEsperadas,response.Value);
        }

        [Fact]
        public async Task Create()
        {
            //configurar
            var transacao = InstanciarTransacao();
            var esperado = InstanciarTransacao();
            _transacaoService.Create(transacao).Returns(esperado);

            //executar
            var response = (CreatedResult)await _transacoesController.Create(transacao);
            
            //validar
            Assert.NotNull(response);
            Assert.Equal(201,response.StatusCode.GetValueOrDefault());
            Assert.Equal(esperado,response.Value);
        }

        [Fact]
        public  async Task CreateBadResquest()
        {
            //configurar
            var transacao = InstanciarTransacao();
            _transacaoService.Create(transacao).ReturnsNull();

            //executar
            var response = (BadRequestObjectResult)await _transacoesController.Create(null);

            //validar
            Assert.NotNull(response);
            Assert.Equal(400,response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.TransacaoNaoCriada, response.Value);
        }

        [Fact]
        public async Task AtualizarTransacaoOk()
        {
            //configurar
            var transacaoEncontrada = InstanciarTransacao();
            string id = "";
            _transacaoService.Atualizar(id, transacaoEncontrada).Returns(transacaoEncontrada);

            //executar
            var response = (OkObjectResult)await _transacoesController.Atualizar(id, transacaoEncontrada);

            //validar
            Assert.NotNull(response);
            Assert.Equal(200,response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.TransacaoAtualizada,response.Value);
        }

        [Fact]
        public async Task AtualizarTransacaoBadRequest()
        {
            //configurar
            var transacaoEncontrada = InstanciarTransacao();
            string id = "";
            _transacaoService.Atualizar(id, transacaoEncontrada).ReturnsNull();

            //executar
            var response = (BadRequestObjectResult)await _transacoesController.Atualizar(id, transacaoEncontrada);

            //validar

            Assert.NotNull(response);
            Assert.Equal(400,response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.TransacaoNaoEncontrada,response.Value);
        }

        public Transacao InstanciarTransacao()
        {
            Transacao transacao = new Transacao();
            transacao.Id = "ce023a9f-cf41-4165-9e43-cb7af29fbf33";
            transacao.Descricao = "Sacolao";
            transacao.ValorTransacao = 20.50;
            transacao.DataTransacao = DateTime.Now;
            transacao.ContaId = "9e71ba5d-4fb1-4bec-8015-cfb6117a5dfc"; 

            return transacao;
        }
    }
}
