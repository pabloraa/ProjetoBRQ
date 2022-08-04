using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ViewBag.Title = "Home Page";
            var pessoa = new Pessoa
            {
               PessoaId = 1,
               Nome = "xxxxxxx",
               Tipo = "xxxxxxx",
               Data_Nascimento = "xxxxxxxx",
               Profissao = "xxxxxxxxxxxxx",
               Conta_Corrente = "xxxx",
               Agencia = "xxxx",
               Conta_destino = "xxxx",
               Id_Cliente = "xxxx",
               Cpf = "xxx.xxx.xxx-xx",
               Rg = "xx xxx xxx",
               Extrato = "xxxxx",
               Saldo = "xxxx",
            };

            //var pessoa2 = new Pessoa();
            //pessoa2.PessoaId = 3;
            //pessoa2.Nome = "Pablo";
            //pessoa2.Tipo = "Engenheiro";

            //ViewData["PessoaId"] = pessoa.PessoaId;
           // ViewData["Nome"] = pessoa.Nome;
            //ViewData["Tipo"] = pessoa.Tipo;


            return View(pessoa);
        }
        public ActionResult Contato()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Lista(Pessoa pessoa)
        {
            ViewData["PessoaId"] = pessoa.PessoaId;
            ViewData["Nome"] = pessoa.Nome;
            ViewData["Tipo"] = pessoa.Tipo;
            ViewData["Data_Nascimento"] = pessoa.Data_Nascimento;
            ViewData["Profissão"] = pessoa.Profissao;
            ViewData["Conta_Corrente"] = pessoa.Conta_Corrente;
            ViewData["Agencia"] = pessoa.Agencia;
            ViewData["Conta_destino"] = pessoa.Conta_destino;
            ViewData["Id_Cliente"] = pessoa.Id_Cliente;
            ViewData["Cpf"] = pessoa.Cpf;
            ViewData["Rg"] = pessoa.Rg;
            ViewData["Extrato"] = pessoa.Extrato;
            ViewData["Saldo"] = pessoa.Saldo;

            return View();
        }
    }
}
