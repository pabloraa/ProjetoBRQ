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
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetAll() 
        {
            return Ok(await _transacaoService.BuscarTransacoes());
        }

        [HttpGet]
        [Route("GetById/{idConta}")]  
        public async Task<IActionResult> GetById(string idConta)
        {
            var transacoes = await _transacaoService.BuscarTransacoesPorIdConta(idConta);

            if (transacoes.Count == 0)
                return NotFound(Mensagens.TransacaoNaoEncontrada);

            return Ok(transacoes);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Transacao transacao)
        {
            if (transacao is null)
            {
                return BadRequest(Mensagens.TransacaoNaoCriada);
            }

            var t = await _transacaoService.Create(transacao);  
            
            return Created("Transacao", t);
        }

        [HttpPut]
        [Route("Atualizar/{id}")]
        public async Task<IActionResult> Atualizar(string id,[FromBody] Transacao transacao)
        {
            var t = await _transacaoService.Atualizar(id, transacao);

            if (t is null)
                return BadRequest(Mensagens.TransacaoNaoEncontrada);
            return Ok("Transacao Atualizada!");
        }
    }
}

