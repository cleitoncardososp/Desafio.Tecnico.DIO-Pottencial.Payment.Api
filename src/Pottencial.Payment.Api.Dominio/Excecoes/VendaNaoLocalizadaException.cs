using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pottencial.Payment.Api.Dominio.Excecoes
{
    public class VendaNaoLocalizadaException : Exception
    {
        public VendaNaoLocalizadaException(string message) : base(message)
        {
        }
    }
}
