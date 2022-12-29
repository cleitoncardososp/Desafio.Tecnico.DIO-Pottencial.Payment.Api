using Pottencial.Payment.Api.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pottencial.Payment.Api.Dominio.Fabricas
{
    public interface IVendedorFabrica
    {
        Vendedor CriarInstancia(Guid id, string cpf, string nome, string email, string telefone);
    }

    public class VendedorFabrica : IVendedorFabrica
    {
        public Vendedor CriarInstancia(Guid id, string cpf, string nome, string email, string telefone)
        {
            return new Vendedor(id, cpf, nome, email, telefone);
        }
    }
}
