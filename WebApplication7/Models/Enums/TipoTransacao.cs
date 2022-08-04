using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication7.Models.Enums
{
    public enum TipoTransacao
    {
        Saque = 1,
        Deposito = 2,
        Pix = 3,
        Ted = 4,
        Doc = 5,
        DebitoAutomatico = 6
    }
}