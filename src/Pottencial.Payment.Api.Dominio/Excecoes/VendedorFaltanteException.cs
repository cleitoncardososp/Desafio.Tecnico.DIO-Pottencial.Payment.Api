using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pottencial.Payment.Api.Dominio.Excecoes
{
    public class VendedorFaltanteException : Exception
    {
        public VendedorFaltanteException(string message) : base(message)
        {

        }
    }
}
