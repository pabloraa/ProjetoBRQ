using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class ContaController : ApiController
    {
        public static List<Conta> Contas = new List<Conta>();
        
        [HttpPost]
        [Route("api/conta/Create")]
        public IHttpActionResult Create(Conta conta)
        {
            if (conta is null)
            {
                return BadRequest();
            }
            conta.Id = Guid.NewGuid();
            conta.DataAbertura = DateTime.Now;
            conta.DataEncerramento = null;
            conta.Transacoes = new List<Transacao>();
            conta.Ativo = true;
            Contas.Add(conta);
            return Created("Contas", conta);
            //return Ok(Contas);
        }

        [HttpGet]
        [Route("api/conta/GetByIdcliente/{id}")]
        public IHttpActionResult Read(int id)
        {
            var conta = Contas.Where(x => x.IdCliente.Equals(id));

            if(conta is null)
            {
                return NotFound();
            }
            return Ok(conta);
        }

        [HttpGet]
        [Route("api/conta/GetById/{id}")]
        public IHttpActionResult Read(Guid id)
        {
            var conta = Contas.Where(x => x.Id.Equals(id));

            if (conta is null)
            {
                return NotFound();
            }
            return Ok(conta);
        }

        [HttpGet]
        [Route("api/conta/GetByAgencia/{id}")]
        public IHttpActionResult BuscaPorAgencia(int id)
        {
            var conta = Contas.Where(x => x.Agencia.Equals(id));

            if (conta is null)
            {
                return NotFound();
            }
            return Ok(conta);
        }

        [HttpGet]
        [Route("api/conta/GetByConta/{id}")]
        public IHttpActionResult BuscaPorConta(int id)
        {
            var conta = Contas.Where(x => x.NumeroConta.Equals(id));

            if (conta is null)
            {
                return NotFound();
            }
            return Ok(conta);
        }

        [HttpPut]
        [Route("api/conta/Atualizar/{id}")]
        public IHttpActionResult Put(Guid id, [FromBody] Conta conta)
        {
            if(conta is null)
            {
                return BadRequest();
            }

            var contaEncontrada = Contas.Find(x => x.Id.Equals(id));
            if (contaEncontrada is null)
                return NotFound();
            //contaEncontrada.Nome = conta.Nome;
            //contaEncontrada.IdCliente = conta.IdCliente; NUNCA PODE ATUALIZAR IDS.
            //contaEncontrada.NumeroConta = conta.NumeroConta; NUNCA PODE ATUALIZAR NÚMERO DA CONTA.
            contaEncontrada.DataEncerramento = conta.DataEncerramento;
            contaEncontrada.Ativo = conta.Ativo;
            return Ok(contaEncontrada);
        }

        [HttpDelete]
        [Route("api/conta/Deletar/{id}")]
        public IHttpActionResult Deletar(Guid id)
        {
            var contaEncontrada = Contas.Find(x => x.Id.Equals(id));
            if(contaEncontrada is null)
            {
                return NotFound();
            }
            Contas.Remove(contaEncontrada);
            return Ok();
        }

        [HttpGet]
        [Route("api/conta/Transacoes/{id}")]
        public IHttpActionResult BuscaTransacoes(Transacao transacoes)
        {
            if(transacoes is null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}