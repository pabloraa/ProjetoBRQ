using System;
using WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.DataBaseConection;
using WebServiceApi.Services;
using WebServiceApi.Interfaces;
using WebApiModels.Models.Enums;
using Recursos;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class TransacoesController : ControllerBase //somente backend
    {

        private ApiContext _context;
        private ITransacaoService _transacaoService;

        public TransacoesController(ApiContext context, ITransacaoService transacaoService)
        {
            _context = context;
            _transacaoService = transacaoService;
        }

        [HttpGet]
        [Route("GetAll")]  
        public IActionResult GetAll() 
        {
            return Ok(_transacaoService.BuscarTransacoes());
        }

        [HttpGet]
        [Route("GetById/{idConta}")]  
        public IActionResult GetById(string idConta)
        {
            var transacoes = _transacaoService.BuscarTransacoesPorIdConta(idConta);

            if (transacoes.Count == 0)
                return NotFound(Mensagens.TransacaoNaoEncontrada);

            return Ok(transacoes);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(Transacao transacao)
        {
            if (transacao is null)
            {
                return BadRequest(Mensagens.TransacaoNaoCriada);
            }

            var t = _transacaoService.Create(transacao);  
            
            return Created("Transacao", t);
        }
    }
}

