using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Controllers;
using WebServiceApi.Interfaces;

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
    }
}
