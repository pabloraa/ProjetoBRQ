using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Mvc;
using WebApplication7.Models;


namespace WebApplication7.Controllers
{
    public class TransacoesController : ApiController //somente backend
    {
        private List<Conta> contas = ContaController.Contas;
        

        [HttpPost]
        [Route("api/Transacoes/Create")]
        public IHttpActionResult Create(Transacao transacao)
        {
            if(transacao is null)
            {
                return BadRequest();
            }

            var conta = contas.Find(x => x.Id.Equals(transacao.IdDonoTransacao));
            if (conta is null)
            {
                return NotFound();
            }

            transacao.Id = Guid.NewGuid(); //objeto, pesquisar o que é
            transacao.DataTransacao = DateTime.Now;
            
            conta.Transacoes.Add(transacao);
            return Created(conta.Id.ToString(), transacao);
        }
    }
}

/* Verificar se a conta existe, se a conta existir adicionar a transação na conta.
             * se a conta não existir, retornar notFound();
             * Comparar a transacao.IdDonoTransacao com conta.Id.
             * 
             * 
             * 
             * 
             * */
