using Pottencial.Payment.Api.Dominio.Enum;
using Pottencial.Payment.Api.Dominio.Excecoes;

namespace Pottencial.Payment.Api.Dominio.Entidades
{
    public class Venda
    {
        public Guid Id { get; set; }
        public Vendedor Vendedor { get; }
        public List<Item> Itens { get; set; }
        public DateTime DataDaVenda { get; set; }
        public EnumStatusDaVenda StatusDaVenda { get; set; }

        public Venda(Vendedor vendedor, List<Item> itens, DateTime dataDaVenda)
        {
            Id = Guid.NewGuid();

            Vendedor = vendedor ?? throw new VendedorFaltanteException("Necessário informar o vendedor!");
            if(itens == null || itens.Count == 0)
            {
                throw new ItensDaVendaFaltantesException("Necessário informar itens da venda!");
            }
            else
            {
                Itens = itens;
            }

            if (dataDaVenda == DateTime.MinValue)
            {
                throw new DataDaVendaFaltanteException("Necessário informar a data");
            }
            else
            {
                DataDaVenda = dataDaVenda;
            }

            StatusDaVenda = EnumStatusDaVenda.AguardandoPagamento;
        }

        public Venda(Guid id, Vendedor vendedor, List<Item> itens, DateTime dataDaVenda, EnumStatusDaVenda statusDaVenda)
        {
            Id = id;
            Vendedor = vendedor;
            Itens = itens;
            DataDaVenda = dataDaVenda;
            StatusDaVenda = statusDaVenda;
        }

        public void AtualizarStatus(EnumStatusDaVenda novoStatus)
        {
            if ((StatusDaVenda == EnumStatusDaVenda.AguardandoPagamento) &&
                (novoStatus == EnumStatusDaVenda.PagamentoAprovado ||
                 novoStatus == EnumStatusDaVenda.Cancelada))
            {
                StatusDaVenda = novoStatus;
            }
            else if ((StatusDaVenda == EnumStatusDaVenda.PagamentoAprovado) &&
                (novoStatus == EnumStatusDaVenda.EnviadoParaTransportadora ||
                 novoStatus == EnumStatusDaVenda.Cancelada))
            {
                StatusDaVenda = novoStatus;
            }
            else if ((StatusDaVenda == EnumStatusDaVenda.EnviadoParaTransportadora) &&
                 (novoStatus == EnumStatusDaVenda.Entregue))
            {
                StatusDaVenda = novoStatus;
            }
            else
            {
                throw new AtualizacaoDeStatusIncompativelException(
                    "Atualização de Status Incompativel," +
                    $"impossível alterar de {StatusDaVenda} para {novoStatus}");
            }
        }
    }
}
