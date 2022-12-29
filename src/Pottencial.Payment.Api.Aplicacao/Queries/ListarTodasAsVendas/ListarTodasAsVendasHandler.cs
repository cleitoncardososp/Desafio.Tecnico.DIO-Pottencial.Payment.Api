using MediatR;
using Microsoft.Extensions.Logging;
using Pottencial.Payment.Api.Aplicacao.Interfaces;
using Pottencial.Payment.Api.Dominio.Entidades;

namespace Pottencial.Payment.Api.Aplicacao.Queries.ListarTodasAsVendas
{
    public class ListarTodasAsVendasHandler : IRequestHandler<ListarTodasAsVendasQuery, ListarTodasAsVendasResponse>
    {
        public IVendaRepositorio VendaRepositorio { get; set; }
        private readonly ILogger<ListarTodasAsVendasHandler> Logger;

        public ListarTodasAsVendasHandler(IVendaRepositorio vendaRepositorio, ILogger<ListarTodasAsVendasHandler> logger)
        {
            VendaRepositorio = vendaRepositorio;
            Logger = logger;
        }

        public Task<ListarTodasAsVendasResponse> Handle(ListarTodasAsVendasQuery request, CancellationToken cancellationToken)
        {
            List<Venda> listaDeVendas = VendaRepositorio.ListarTodasAsVendas();

            return Task.FromResult(new ListarTodasAsVendasResponse()
            {
                Vendas = listaDeVendas
            });
        }
    }
}
