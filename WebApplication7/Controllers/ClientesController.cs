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
    public class ClientesController : ApiController
    {

        public static List<Cliente> listaCliente = new List<Cliente>();

        [HttpGet]
        [Route(("api/clientes"))]
        public IHttpActionResult Get()
        {
            return Ok(listaCliente); // retorna uma lista em formato jason.
        }

        // GET api/clientes/5
        [HttpGet]
        [Route(("api/clientes/BuscarPorId/{Id}"))]
        public IHttpActionResult Get(int id)
        {
           var cliente = listaCliente.Find(x => x.IdCliente.Equals(id));
            if (cliente is null)
                return NotFound();

           return Ok(cliente);
        }

        // POST api/clientes
        // adicionar clientes
        [HttpPost]
        [Route("api/clientes/Adicionar")]
        public IHttpActionResult Adicionar([FromBody] Cliente cliente)
        {
            if(cliente is null)
                return NotFound();
                
            listaCliente.Add(cliente);
            return Created<Cliente>("lista Cliente",cliente); 
        }

        [HttpPut]
        [Route("api/clientes/AtualizarCliente/{id}")]
        // PUT api/clientes/5
        public IHttpActionResult Put(int id, [FromBody] Cliente cliente)
        {
            if (cliente is null)
                return BadRequest();

            var clienteEncontrado = listaCliente.Find(x => x.IdCliente.Equals(id));
            if (clienteEncontrado is null)
                return NotFound();

            var elemento = listaCliente.First(r => r.IdCliente.Equals(id));

            elemento.Agencia = cliente.Agencia;
            elemento.ContaCorrente = cliente.ContaCorrente;
            elemento.Cpf = cliente.Cpf;
            elemento.Rg = cliente.Rg;
            elemento.Profissao = cliente.Profissao;
            elemento.Nome = cliente.Nome;
            elemento.DataNascimento = cliente.DataNascimento;

            return Ok(elemento);
        }

        // DELETE api/clientes/5
        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/clientes/excluir/{id}")]
        public IHttpActionResult Excluir(int id)
       {
            var cli = listaCliente.Where(x => x.IdCliente.Equals(id)).ToList();

            foreach (var cliente in cli)
            {
                listaCliente.Remove(cliente);
            }
            return Ok();
       }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/clientes/Excluir_Nome/{nome}")]
        public IHttpActionResult Excluir_Nome(string Nome)
        {
            var cli = listaCliente.Where(x => x.Nome.Equals(Nome)).ToList();

            foreach (var cliente in cli)
            {
                listaCliente.Remove(cliente);
            }
            return Ok<string>("Ok, foram removidos: " + cli.Count() + " clientes");
        }

        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/clientes/Valor_Compra/{valor}")]
        public IHttpActionResult Valor_Compra(float valor)
        {
            var cli = listaCliente.Where(x => x.Nome.Equals(valor)).ToList();
            foreach(var cliente in cli)
            {
                listaCliente.Add(cliente);
            }
            return Ok();
        }
    }
}
