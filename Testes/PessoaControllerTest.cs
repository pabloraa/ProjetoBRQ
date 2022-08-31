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
using WebServiceApi.Interfaces;
using Xunit;

namespace Testes
{
    public class PessoaControllerTest
    {
        private readonly PessoaController _pessoaController;
        private readonly IPessoaService _pessoaService;

        public PessoaControllerTest()
        {
            _pessoaService = Substitute.For<IPessoaService>();

            _pessoaController = new PessoaController(_pessoaService);
        }


        [Fact]
        public async Task Create()
        {

        }

        [Fact]
        public async Task GetAllOk()
        {
         
            //executar
            var response = (OkObjectResult)await _pessoaController.GetAll();

            //validar

            Assert.NotNull(response);
            Assert.Equal(200,response.StatusCode.GetValueOrDefault());
            
        }

        [Fact]
        public async Task GetByPessoaIdOk()
        {
            //configurar
            string id = "";
            var pessoaEncontrada = InstanciarUmaPessoa();
            _pessoaService.BuscaPorId(id).Returns(pessoaEncontrada);

            //executar
            var response = (OkObjectResult)await _pessoaController.BuscaPorId(id);

            //validar

            Assert.NotNull(response);
            Assert.Equal(200,response.StatusCode.GetValueOrDefault());
            Assert.Equal(pessoaEncontrada,response.Value);
        }

        [Fact]
        public async Task GetByPessoaIdNotFound()
        {
            //configurar
            string id = "";
            _pessoaService.BuscaPorId(id).ReturnsNull();

            //executar
            var response = (NotFoundObjectResult)await _pessoaController.BuscaPorId(id);

            //validar
            Assert.NotNull(response);
            Assert.Equal(404,response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.ClienteNaoEncontrado,response.Value);

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