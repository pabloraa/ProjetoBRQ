using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

    public class PessoaController : ControllerBase
    {
        private IContaService _contaService;
        private ITransacaoService _transacaoService;
        private IPessoaService _pessoaService;

        public PessoaController( IPessoaService pessoaService, IContaService contaService, ITransacaoService transacaoService)
        {
            _pessoaService = pessoaService;
            _contaService = contaService;
            _transacaoService = transacaoService;
        }


        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Pessoa pessoa)
        {
            Pessoa p = await _pessoaService.Create(pessoa);
            return Created("Pessoa", p);
        } 

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _pessoaService.GetAll());
        }

        [HttpGet]
        [Route("GetByPessoaId/{id}")]
        public async Task<IActionResult> BuscaPorId(string id)
        {
            var pessoa = await _pessoaService.BuscarPorId(id);

            if(pessoa is null)
            {
                return NotFound(Mensagens.ClienteNaoEncontrado);
            }

            return Ok(pessoa);
}

        [HttpDelete]
        [Route("DeletarPorId/{id}")]
        public async Task<IActionResult> DeletarPorId(string id)
        {

           Resultadoservice resultado = await _pessoaService.DeletarPorId(id);

            if(resultado == Resultadoservice.NaoEncontrado)
            {
                return NotFound(Mensagens.ClienteNaoEncontrado);
            }

            if(resultado == Resultadoservice.NaoPodeExcluir)
            {
                return Ok(Mensagens.NaoPodeExcluir);
            }

            return Ok(Mensagens.ExcluidoComSucesso);
        }

        [HttpPut]
        [Route("AtualizarPorId/{id}")]
        public async Task<IActionResult> AtualizarPessoa(string id,[FromBody] Pessoa pessoa)
        {
            var p = await _pessoaService.AtualizarPessoa(id, pessoa);
            if (p is null)
                return NotFound(Mensagens.PessoaNaoEncontrada);
            return Ok(Mensagens.PessoaAtualizada);
        }
    }
}
