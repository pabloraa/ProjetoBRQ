using System;
using System.Data.SqlClient;

namespace WebServiceApi
{
    class Program
    {
        static void Main(string[] args)
        {
            
            SqlConnection conexao = new SqlConnection(@"data source =DESKTOP-10HCQPV\SQLEXPRESS ; Integrated Security=SSPI ; Initial Catalog=ExemploBD");
            conexao.Open();

            string strQuerySelect = "SELECT * FROM usuarios";

            var ConexaoBD = new ConexaoBD();
        }
    }
}
