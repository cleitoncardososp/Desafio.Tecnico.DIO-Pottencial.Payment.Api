using MediatR;
using Pottencial.Payment.Api.Dominio.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pottencial.Payment.Api.Aplicacao.Commands.AtualizarStatusDaVenda
{
    public class AtualizarStatusDaVendaRequest : IRequest<AtualizarStatusDaVendaResponse>
    {
        public Guid Id { get; set; }
        public EnumStatusDaVenda StatusDaVenda { get; set; }
    }
}
