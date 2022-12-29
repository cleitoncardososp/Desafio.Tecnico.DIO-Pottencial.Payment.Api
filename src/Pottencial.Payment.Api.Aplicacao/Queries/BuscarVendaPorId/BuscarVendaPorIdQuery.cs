using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pottencial.Payment.Api.Aplicacao.Queries.BuscarVendaPorId
{
    public class BuscarVendaPorIdQuery : IRequest<BuscarVendaPorIdResponse>
    {
        public Guid Id { get; set; }

    }
}
