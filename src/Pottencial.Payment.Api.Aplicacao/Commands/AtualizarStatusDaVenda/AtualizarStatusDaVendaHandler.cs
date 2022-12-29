using MediatR;
using Microsoft.Extensions.Logging;
using Pottencial.Payment.Api.Aplicacao.Commands.CadastrarVenda;
using Pottencial.Payment.Api.Aplicacao.Interfaces;
using Pottencial.Payment.Api.Dominio.Entidades;
using Pottencial.Payment.Api.Dominio.Enum;
using Pottencial.Payment.Api.Dominio.Excecoes;

namespace Pottencial.Payment.Api.Aplicacao.Commands.AtualizarStatusDaVenda
{
    public class AtualizarStatusDaVendaHandler : IRequestHandler<AtualizarStatusDaVendaRequest, AtualizarStatusDaVendaResponse>
    {
        public IVendaRepositorio VendaRepositorio { get; set; }
        private readonly ILogger<CadastrarVendaHandler> Logger;

        public AtualizarStatusDaVendaHandler(IVendaRepositorio vendaRepositorio, ILogger<CadastrarVendaHandler> logger)
        {
            VendaRepositorio = vendaRepositorio;
            Logger = logger;
        }

        public Task<AtualizarStatusDaVendaResponse> Handle(AtualizarStatusDaVendaRequest request, CancellationToken cancellationToken)
        {
            Venda venda = VendaRepositorio.BuscarVendaPorId(request.Id);

            venda.AtualizarStatus(request.StatusDaVenda);

            VendaRepositorio.Atualizar(venda);

            return Task.FromResult(new AtualizarStatusDaVendaResponse()
            {
                Venda = venda
            });
        }
    }
}
