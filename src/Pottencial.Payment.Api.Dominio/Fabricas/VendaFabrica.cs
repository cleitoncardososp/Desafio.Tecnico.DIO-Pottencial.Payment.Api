using Pottencial.Payment.Api.Dominio.Entidades;
using Pottencial.Payment.Api.Dominio.Enum;

namespace Pottencial.Payment.Api.Dominio.Fabricas
{
    public interface IVendaFabrica
    {
        Venda CriarInstancia(Guid VendaId, Vendedor VendedorId, List<Item> Itens, DateTime DataDaVenda, EnumStatusDaVenda StatusDaVenda);
    }

    public class VendaFabrica : IVendaFabrica
    {
        public Venda CriarInstancia(Guid VendaId, Vendedor VendedorId, List<Item> Itens, DateTime DataDaVenda, EnumStatusDaVenda StatusDaVenda)
        {
            return new Venda(VendaId, VendedorId, Itens, DataDaVenda, StatusDaVenda);
        }
    }
}
