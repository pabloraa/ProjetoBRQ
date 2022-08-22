using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NSubstitute;
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

        
        public ContaControllerTest()
        {
            
            var icontaService = Substitute.For<IContaService>();

            _contaController = new ContaController(icontaService);
        }

        [Fact]
        public async Task CreateReturnsBadRequest()
        {
            //configurar 
            
            //executar
            var response = (IStatusCodeActionResult) await _contaController.Create(null);
            var response2 = (IStatusCodeActionResult) response;
            var response3 = (OkObjectResult) response2;
            //validar
            Assert.NotNull(response);
            Assert.Equal(401,response2.StatusCode.GetValueOrDefault());
            //Assert.Equal("",response);
        }
        [Fact]
        public async Task CreateReturnsNotFound()
        {

        }
        [Fact]
        public async Task CreateReturnsCreated()
        {

        }
    }
}
