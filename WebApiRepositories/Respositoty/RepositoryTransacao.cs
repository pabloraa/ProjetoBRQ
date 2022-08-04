using System;
using System.Collections.Generic;
using System.Text;
using WebApi.DataBaseConection;
using WebRepositories.Interfaces;

namespace WebServiceApi.Respositoty
{
    public class RepositoryTransacao : IRepositoryTransacao
    {
        private ApiContext _context;
    }
}
