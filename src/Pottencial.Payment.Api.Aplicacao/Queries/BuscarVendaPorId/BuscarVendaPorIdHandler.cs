using MediatR;
using Microsoft.Extensions.Logging;
using Pottencial.Payment.Api.Aplicacao.Interfaces;
using Pottencial.Payment.Api.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pottencial.Payment.Api.Aplicacao.Queries.BuscarVendaPorId
{
    public class BuscarVendaPorIdHandler : IRequestHandler<BuscarVendaPorIdQuery, BuscarVendaPorIdResponse>
    {
        public IVendaRepositorio VendaRepositotio { get; set; }
        private readonly ILogger<BuscarVendaPorIdHandler> Logger;

        public BuscarVendaPorIdHandler(IVendaRepositorio vendaRepositotio, ILogger<BuscarVendaPorIdHandler> logger)
        {
            VendaRepositotio = vendaRepositotio;
            Logger = logger;
        }

        public Task<BuscarVendaPorIdResponse> Handle(BuscarVendaPorIdQuery request, CancellationToken cancellationToken)
        {
            Venda venda = VendaRepositotio.BuscarVendaPorId(request.Id);

            return Task.FromResult(new BuscarVendaPorIdResponse()
            {
                Venda = venda
            });
        }
    }
}
