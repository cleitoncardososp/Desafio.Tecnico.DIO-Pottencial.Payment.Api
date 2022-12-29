using Pottencial.Payment.Api.Dominio.Entidades;
using Pottencial.Payment.Api.Dominio.Enum;

namespace Pottencial.Payment.Api.Aplicacao.Interfaces
{
    public interface IVendaRepositorio
    {
        void CadastrarVenda(Venda venda);
        Venda BuscarVendaPorId(Guid vendaId);
        void Atualizar(Venda venda);
        List<Venda> ListarTodasAsVendas();
    }
}
