using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using WebApi.DataBaseConection;

namespace WebServiceApi
{
    public class ConexaoBD : IDisposable
    {
        private ApiContext _context;
        private readonly SqlConnection conexao;

        public ConexaoBD()
        {
            conexao = new SqlConnection(@"data source =DESKTOP-10HCQPV\SQLEXPRESS ; Integrated Security=SSPI ; Initial Catalog=ExemploBD");
            conexao.Open();
        }

        public void executaComando(string strQuery)
        {
            var cmdComando = new SqlCommand
            {
                CommandText = strQuery,
                CommandType = CommandType.Text,
                Connection = conexao
            };
            cmdComando.ExecuteNonQuery();
        }

        public SqlDataReader executaComandoComRetorno(string strQuery)
        {
            var cmdComandoSelect = new SqlCommand(strQuery, conexao);
            return cmdComandoSelect.ExecuteReader();
        }

        public void Dispose()
        {
            if (conexao.State == System.Data.ConnectionState.Open)
            {
                conexao.Close();
            }
        }
    }
}
