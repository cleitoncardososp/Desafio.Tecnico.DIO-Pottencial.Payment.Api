using MediatR;
using Microsoft.Extensions.Logging;
using Pottencial.Payment.Api.Aplicacao.Interfaces;
using Pottencial.Payment.Api.Dominio.Entidades;

namespace Pottencial.Payment.Api.Aplicacao.Commands.CadastrarVenda
{
    public class CadastrarVendaHandler : IRequestHandler<CadastrarVendaRequest, CadastrarVendaResponse>
    {
        public IVendaRepositorio VendaRepositorio { get; set; }
        private readonly ILogger<CadastrarVendaHandler> Logger;

        public CadastrarVendaHandler(IVendaRepositorio vendaRepositorio, ILogger<CadastrarVendaHandler> logger)
        {
            VendaRepositorio = vendaRepositorio;
            Logger = logger;
        }

        public Task<CadastrarVendaResponse> Handle(CadastrarVendaRequest request, CancellationToken cancellationToken)
        {
            Vendedor vendedor = new Vendedor(request.Vendedor.Cpf,
                                             request.Vendedor.Nome,
                                             request.Vendedor.Email,
                                             request.Vendedor.Telefone);

            List<Item> listaDeItens = new List<Item>();
            foreach (var registro in request.ListaDeItens)
            {
                Item item = new Item(registro.Nome);
                listaDeItens.Add(item);
            }

            Venda venda = new Venda(vendedor, listaDeItens, request.DataDaVenda);

            VendaRepositorio.CadastrarVenda(venda);

            return Task.FromResult(new CadastrarVendaResponse()
            {
                Venda = venda
            }) ;
        }
    }
}
