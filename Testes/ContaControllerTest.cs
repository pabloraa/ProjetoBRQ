using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NSubstitute;
using Recursos;
using System.Net;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.DataBaseConection;
using WebApi.Models;
using WebServiceApi.Interfaces;
using Xunit;

namespace Testes
{
    public class ContaControllerTest
    {
        private readonly ContaController _contaController;
        private readonly IContaService _contaService;
        
        public ContaControllerTest()
        {
            
            _contaService = Substitute.For<IContaService>();

            _contaController = new ContaController(_contaService);
        }

        //Ok
        [Fact]
        public async Task CreateReturnsBadRequest()
        {
            //configurar 
            
            //executar
            var response = (BadRequestObjectResult)await _contaController.Create(null);
            
            //validar

            Assert.NotNull(response);
            Assert.Equal(400,response.StatusCode.GetValueOrDefault());
            
        }

        //ok
        [Fact]
        public async Task CreateReturnsNotFound()
        {
            //configurar
            var conta = InstanciarUmaConta();
            conta.PessoaId = "";
            
            //exectuar

            var response = (NotFoundObjectResult)await _contaController.Create(conta);
            //validar

            Assert.NotNull(response);
            Assert.Equal(404, response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.FaltaCliente,response.Value);
        }
        //ok
        [Fact]
        public async Task CreateReturnsCreated()
        {

            //configurar
            var conta = InstanciarUmaConta();
            var contaEsperada = InstanciarUmaConta();
            _contaService.Create(conta).Returns(contaEsperada);//novo

            //executar

            var response = (CreatedResult)await _contaController.Create(conta);

            //validar
            Assert.NotNull(response);
            Assert.Equal(201, response.StatusCode.GetValueOrDefault());
            Assert.Equal(contaEsperada, response.Value);
        }

        public Conta InstanciarUmaConta()
        {
            Conta conta = new Conta();
            conta.PessoaId = "c9af33fd-a954-4988-94e9-b2832bcc2358";
            conta.Agencia = 1452;
            conta.NumeroConta = 1234;
            conta.Ativo = true;

            return conta;
        }
    }
}
