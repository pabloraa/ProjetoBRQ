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
        
        private IContaService _contaService;

        public ContaController(IContaService contaService)
        {
            
            _contaService = contaService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Conta conta) //assíncrona
        {
            if (conta is null)
            {
                return BadRequest(Mensagens.ContaNaoInformada);
            }
            if(string.IsNullOrEmpty(conta.PessoaId))
            {
                return NotFound(Mensagens.FaltaCliente);
            }

            var c = await _contaService.Create(conta);
            return Created("Contas", c);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _contaService.GetAll());
        }

        [HttpGet]
        [Route("GetByIdCliente/{idCliente}")]
        public async Task<IActionResult> BuscarPorId(string idCliente)
        {
            var conta = await _contaService.BuscarContaPorIdCliente(idCliente);

            if (conta is null)
            {
                return NotFound(Mensagens.ClienteNaoEncontrado);
            }
            return Ok(conta);
        }

        [HttpGet]
        [Route("GetById/{idConta}")]
        public async Task<IActionResult> Read(string idConta)
        {
            var conta = await _contaService.BuscarContaPorIdConta(idConta);
            
            if (conta is null)
            {
                return NotFound(Mensagens.ClienteNaoEncontrado);
            }

            return Ok(conta);
        }

        [HttpGet]
        [Route("GetByConta")]
        public async Task<IActionResult> BuscaPorConta(int agencia, int numeroConta)
        {
            var conta = await _contaService.BuscarContaPorAgenciaENumero(agencia,numeroConta);

            if (conta is null)
            {
                return NotFound(Mensagens.ContaNaoEncontrada);
            }

            return Ok(conta);
        }

        [HttpPut]
        [Route("Atualizar/{idConta}")]
        public async Task<IActionResult> Put(string idConta, [FromBody] Conta conta)
        {
            if (conta is null)
            {
                return BadRequest(Mensagens.ContaNaoInformada);
            }

            var contaEncontrada = await _contaService.AtualizarPorId(idConta,conta);

            return Ok(contaEncontrada); 
        }
        
        [HttpDelete]
        [Route("DeletarPorIdConta/{id}")]
        public async Task<IActionResult> DeletarPorIdConta(string id)
        {
            Resultadoservice resultado = await _contaService.DeletarContaPorId(id);

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