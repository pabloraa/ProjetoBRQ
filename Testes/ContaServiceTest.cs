using NSubstitute;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DataBaseConection;
using WebApi.Models;
using WebServiceApi.Interfaces;
using WebServiceApi.Services;
using Xunit;

namespace Testes
{
    public class ContaServiceTest
    {
        private readonly IContaService _contaService;
        private readonly ITransacaoService _transacaoService;
        private readonly IPessoaService _pessoaService;
        private readonly ApiContext _apiContext;

        public ContaServiceTest()
        {
            _transacaoService = Substitute.For<ITransacaoService>();
            _pessoaService = Substitute.For<IPessoaService>();
            _apiContext = Substitute.For<ApiContext>();

            _contaService = new ContaService(_apiContext,_transacaoService);
        }

        [Fact]
        public async Task GetAll()
        {
            //configurar
            List<Conta> conta = new List<Conta>();
            _apiContext.Contas.ToListAsync().Returns(conta);
            //executar
            var response = await _contaService.GetAll();
            //validar
            Assert.NotNull(response);
            Assert.Empty(response);
            Assert.IsType<List<Conta>>(response);
        }

        [Fact]
        public async Task Create()
        {
            //configurar
            var c = InstanciarConta();
            await _apiContext.Contas.AddAsync(c);
            //executar
            var response = await _contaService.Create(c);
            //validar

            Assert.NotNull(response);
            Assert.Equal(c,response);
        }

        [Fact]
        public async Task BuscarContasPorPessoaId()
        {
            //configurar
            string id = "";
            var contaEncontrada = InstanciarConta();
            await _apiContext.Contas.Where(x => x.PessoaId.Equals(id)).ToListAsync();
            //executar
            var response = await _contaService.BuscarContasPorPessoaId(id);
            //validar

            Assert.NotNull(response);
            Assert.Empty(response);
        }

        public Conta InstanciarConta()
        {
            Conta c = new Conta();
            c.Agencia = 1234;
            c.NumeroConta = 1234;
            c.DataAbertura = DateTime.Now;
            c.DataEncerramento = DateTime.Now;
            c.Ativo = true;

            return c;
        }
    }
}
