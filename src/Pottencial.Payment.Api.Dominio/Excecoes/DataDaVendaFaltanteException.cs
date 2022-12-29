using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pottencial.Payment.Api.Dominio.Excecoes
{
    public class DataDaVendaFaltanteException : Exception
    {
        public DataDaVendaFaltanteException(string message) : base(message)
        {

        }
    }
}
