using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class Cliente
    {
        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Rg { get; set; }

        public string DataNascimento { get; set; }

        public string Profissao { get; set; }

        public string ContaCorrente { get; set; }

        public string Agencia { get; set; }

        public string ContaDestino { get; set; }

        public int IdCliente { get; set; }

        public string Extrato { get; set; }

        public string Saldo { get; set; } 

        public string Valor_Compra { get; set; }

        public string Data_Compra { get; set; }

        public string Descricao_Compra { get; set; } 

        public string Tipo_Transacao { get; set; }

        public string Dono { get; set; }

        //camel case parâmetros de métodos ou funções.
        //Propriedades das classes letras maiúsculas

        public Cliente(string nome, string cpf, string rg, string dataNascimento, string contaCorrente,
            string agencia, string contaDestino,int idCliente, string extrato,string saldo, string valor_compra,string data_compra,
            string descricao, string tipo_transacao, string dono)
        {
            Nome = nome;
            Cpf = cpf;
            Rg = rg;
            DataNascimento = dataNascimento;
            ContaCorrente = contaCorrente;
            Agencia = agencia;
            ContaDestino = contaDestino;
            IdCliente = idCliente;
            Extrato = extrato;
            Saldo = saldo;
            Valor_Compra = valor_compra;
            Data_Compra = data_compra;
            Descricao_Compra = descricao;
            Tipo_Transacao = tipo_transacao;
            Dono = dono;
        }
    }
}