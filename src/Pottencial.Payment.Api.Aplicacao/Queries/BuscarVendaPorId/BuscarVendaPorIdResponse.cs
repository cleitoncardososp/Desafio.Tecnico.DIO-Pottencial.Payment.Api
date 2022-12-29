using MediatR;
using Pottencial.Payment.Api.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pottencial.Payment.Api.Aplicacao.Queries.BuscarVendaPorId
{
    public class BuscarVendaPorIdResponse
    {
        public Venda Venda { get; set; }
    }
}
