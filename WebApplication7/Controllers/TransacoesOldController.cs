using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApplication7.Models;
//using System.Web.Mvc;

namespace WebApplication7.Controllers
{
    public class TransacoesOldController : ApiController
    {
        private static List<Transacao> transacoes = new List<Transacao>();

        [HttpPost]
        //[Route("api/Transacoes/Create")]
        [Route("api/TransacoesOld/Create")]
        public IHttpActionResult Create(Transacao transacao)
        {
            if (transacao is null)
            {
                return BadRequest();
            }

            transacao.Id = Guid.NewGuid(); //objeto, pesquisar o que é
            transacao.DataTransacao = DateTime.Now;
            transacoes.Add(transacao);
            return Created("transacoes", transacao);
        }

        [HttpGet]//OK
        [Route("api/TransacoesOld/Read/{id}")]
        public IHttpActionResult Read(int id)
        {
            var transacao = transacoes.Where(x => x.IdDonoTransacao.Equals(id));
            if (transacao is null)
            {
                return NotFound();
            }

            return Ok(transacao);
        }

        [HttpPut]
        [Route("api/TransacoesOld/Atualizar/{id}")]
        public IHttpActionResult Put(Guid id, [FromBody] Transacao transacao)
        {
            if (transacao is null)
                return BadRequest();

            var transacaoEncontrado = transacoes.Find(x => x.Id.Equals(id));
            if (transacaoEncontrado is null)
                return NotFound();

            // var elemento = transacoes.First(r => r.Id.Equals(id));

            //transacaoEncontrado.Id = transacao.Id; NUNCA PODE FAZER ISSO
            transacaoEncontrado.IdDonoTransacao = transacao.IdDonoTransacao;
            transacaoEncontrado.DataTransacao = transacao.DataTransacao;
            transacaoEncontrado.ValorTransacao = transacao.ValorTransacao;
            transacaoEncontrado.Tipo = transacao.Tipo;
            transacaoEncontrado.Descricao = transacao.Descricao;
            return Ok(transacaoEncontrado);

            //return Ok();
        }

        [HttpDelete]
        [Route("api/TransacoesOld/Deletar/{id}")]
        public IHttpActionResult Deletar(Guid id)
        {
            var transacao = transacoes.Find(x => x.Id.Equals(id));

            if (transacao is null)
            {
                return NotFound();
            }
            transacoes.Remove(transacao);
            return Ok();
        }
    }
}