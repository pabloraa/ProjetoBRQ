using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Recursos;
using System.Net;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.DataBaseConection;
using WebApi.Models;
using WebApiModels.Models.Enums;
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
            conta.Id = "cc210d81-3b38-41da-93e6-eadf4ef71047";

            return conta;
        }

        [Fact]
        public async Task GetByClienteIdRetornaNotFound()
        {
            //configurar
            string id = "";
            _contaService.BuscarContaPorIdCliente(id)
                .ReturnsNull();

            //executar
            var response = (NotFoundObjectResult)await _contaController.BuscarPorId(id);

            //validar
            Assert.NotNull(response);
            Assert.Equal(404,response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.ClienteNaoEncontrado,response.Value);
        }

        [Fact]
        public async Task GetByClienteIdRetornaOk()
        {
            //configurar

            var contaEsperada = InstanciarUmaConta();

            string id = "";
            _contaService.BuscarContaPorIdCliente(id).Returns(contaEsperada);

            //executar
            var response = (OkObjectResult)await _contaController.BuscarPorId(id);

            //validar
            Assert.NotNull(response);
            Assert.Equal(200, response.StatusCode.GetValueOrDefault());
            Assert.Equal(contaEsperada,response.Value);
        }

        [Fact]
        public async Task GetByIdContaNotFound()  //pablo
        {
            //configurar
           
            string idConta = "";
            
            _contaService.BuscarContaPorIdConta(idConta).ReturnsNull();

            //executar
            var response = (NotFoundObjectResult)await _contaController.Read(idConta);

            //validar

            Assert.NotNull(response);
            Assert.Equal(404, response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.ClienteNaoEncontrado,response.Value);
        }

        [Fact]
        public async Task GetByIdContaOk()//pablo
        {
            //configurar
            var contaEncontrada = InstanciarUmaConta();
            string idConta = "";

            _contaService.BuscarContaPorIdConta(idConta).Returns(contaEncontrada);

            //executar

            var response = (OkObjectResult)await _contaController.Read(idConta);

            //validar

            Assert.NotNull(response);
            Assert.Equal(200, response.StatusCode.GetValueOrDefault());
            Assert.Equal(contaEncontrada,response.Value);
        }

        [Fact]
        public async Task GetByContaNotFound()  //Pablo
        {
            //configurar         
            int agencia = 1452;
            int numeroConta = 1234;
            _contaService.BuscarContaPorAgenciaENumero(agencia,numeroConta).ReturnsNull();

            //executar
            var response = (NotFoundObjectResult)await _contaController.BuscaPorConta(agencia,numeroConta);

            //validar

            Assert.NotNull(response);
            Assert.Equal(404, response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.ContaNaoEncontrada,response.Value);
        }

        [Fact]
        public async Task GetByContaOk() //pablo, está com erro
        {
            //configurar
            var contaEncontrada = InstanciarUmaConta();
            int agencia = 1452;
            int numeroConta = 1234;
            _contaService.BuscarContaPorAgenciaENumero(agencia, numeroConta).Returns(contaEncontrada);

            //executar
            var response = (OkObjectResult)await _contaController.BuscaPorConta(agencia,numeroConta);

            //validar
            Assert.NotNull(response);
            Assert.Equal(200, response.StatusCode.GetValueOrDefault());
            Assert.Equal(contaEncontrada,response.Value);
        }

        [Fact]
        public async Task AtualizarNotFound() //pablo
        {
            //configurar
            string id = "";
            var contaEncontrada = InstanciarUmaConta();

            _contaService.AtualizarPorId(id,null).ReturnsNull();

            //executar
            var response = (BadRequestObjectResult)await _contaController.Put(id,null);

            //validar

            Assert.NotNull(response);
            Assert.Equal(400,response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.ContaNaoInformada,response.Value);
        }

        [Fact]
        public async Task AtualizarOk()
        {
            //configurar
            var contaEncontrada = InstanciarUmaConta();
            string id = "";
            _contaService.AtualizarPorId(id, contaEncontrada).Returns(contaEncontrada);

            //executar
            var response = (OkObjectResult)await _contaController.Put(id, contaEncontrada);

            //validar
            Assert.NotNull(response);
            Assert.Equal(200,response.StatusCode.GetValueOrDefault());
            Assert.Equal(contaEncontrada,response.Value);
        }

        [Fact]
        public async Task DeletarNotFound()
        {
            //configurar
            string id = "";
            _contaService.DeletarContaPorId(id).Returns(Resultadoservice.NaoEncontrado);

            //executar
            var response = (NotFoundObjectResult)await _contaController.DeletarPorIdConta(id);

            //validar
            Assert.NotNull(response);
            Assert.Equal(404,response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.ContaNaoEncontrada,response.Value);
        }

        [Fact]
        public async Task DeletarOk()
        {
            //configurar
            var contaEncontrada = InstanciarUmaConta();
            string id = "";
            
            _contaService.DeletarContaPorId(id).Returns(Resultadoservice.Encontrado);

            //executar
            var response = (OkObjectResult)await _contaController.DeletarPorIdConta(id);

            //validar
            Assert.NotNull(response);
            Assert.Equal(200, response.StatusCode.GetValueOrDefault());
            Assert.Equal(Mensagens.ContaRemovida, response.Value);
        } 
    }
}
