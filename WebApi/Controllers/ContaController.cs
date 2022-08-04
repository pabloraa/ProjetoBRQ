using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Recursos;
using WebApi.DataBaseConection;
using WebApi.Models;
using WebApiModels.Models.Enums;
using WebServiceApi.Interfaces;
using WebServiceApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaController : ControllerBase
    {
        private ApiContext _context;
        private IContaService _contaService;

        public ContaController(ApiContext context, IContaService contaService)
        {
            _context = context;
            _contaService = contaService;
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(Conta conta)
        {
            if (conta is null)
            {
                return BadRequest(Mensagens.ContaNaoInformada);
            }
            if(conta.PessoaId.Equals(Guid.Empty))
            {
                return NotFound(Mensagens.FaltaCliente);
            }

            var c =  _contaService.Create(conta);
            return Created("Contas", c);
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_context.Contas);
        }

        [HttpGet]
        [Route("GetByIdCliente/{idCliente}")]
        public IActionResult BuscarPorId(string idCliente)
        {
            var conta = _contaService.BuscarContaPorIdCliente(idCliente);

            if (conta is null)
            {
                return NotFound(Mensagens.ClienteNaoEncontrado);
            }
            return Ok(conta);
        }

        [HttpGet]
        [Route("GetById/{idConta}")]
        public IActionResult Read(string idConta)
        {
            var conta = _contaService.BuscarContaPorIdConta(idConta);
            
            if (conta is null)
            {
                return NotFound(Mensagens.ClienteNaoEncontrado);
            }

            return Ok(conta);
        }

        [HttpGet]
        [Route("GetByConta")]
        public IActionResult BuscaPorConta(int agencia, int numeroConta)
        {
            var conta = _contaService.BuscarContaPorAgenciaENumero(agencia,numeroConta);

            if (conta is null)
            {
                return NotFound(Mensagens.ContaNaoEncontrada);
            }

            return Ok(conta);
        }

        [HttpPut]
        [Route("Atualizar/{idConta}")]
        public IActionResult Put(string idConta, [FromBody] Conta conta)
        {
            if (conta is null)
            {
                return BadRequest(Mensagens.ContaNaoInformada);
            }

            var contaEncontrada = _contaService.AtualizarPorId(idConta,conta);

            return Ok(contaEncontrada); 
        }
        
        [HttpDelete]
        [Route("DeletarPorIdConta/{id}")]
        public IActionResult DeletarPorIdConta(string id)
        {
            Resultadoservice resultado = _contaService.DeletarContaPorId(id);

            if(resultado == Resultadoservice.NaoEncontrado)
            {
                return NotFound(Mensagens.ContaNaoEncontrada);
            }

            return Ok(Mensagens.ContaRemovida);
        }

        //[HttpGet]
        //[Route("BuscaPeriodoTransacoes")]
        //public IActionResult BuscaPeriodoTransacoes(string id, DateTime dataInicial, DateTime dataFinal)
        //{
        //    var contaEncontrada = _contaService.BuscarContaPorIdCliente(id);

        //    if (contaEncontrada is null)
        //    {
        //        return NotFound();
        //    }
            
        //    return Ok();
        //}
    }
}