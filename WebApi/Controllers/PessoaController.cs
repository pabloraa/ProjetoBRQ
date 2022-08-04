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

        private ApiContext _context;
        private IPessoaService _pessoaService;
        private IContaService _contaService;
        private ITransacaoService _transacaoService;

        public PessoaController(ApiContext context, IPessoaService pessoaService, IContaService contaService, ITransacaoService transacaoService)
        {
            _context = context;
            _pessoaService = pessoaService;
            _contaService = contaService;
            _transacaoService = transacaoService;
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(Pessoa pessoa)
        {
            Pessoa p = _pessoaService.Create(pessoa);
            return Created("Pessoa", p);
        } 

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Pessoas);
        }

        [HttpGet]
        [Route("GetByPessoaId/{id}")]
        public IActionResult BuscaPorId(string id)
        {
            var pessoa = _pessoaService.BuscarPorId(id);

            if(pessoa is null)
            {
                return NotFound(Mensagens.ClienteNaoEncontrado);
            }

            return Ok(pessoa);
}

        [HttpDelete]
        [Route("DeletarPorId/{id}")]
        public IActionResult DeletarPorId(string id)
        {

           Resultadoservice resultado = _pessoaService.DeletarPorId(id);

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
    }
}
