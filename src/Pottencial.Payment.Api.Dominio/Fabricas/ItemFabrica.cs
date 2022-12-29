using Pottencial.Payment.Api.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pottencial.Payment.Api.Dominio.Fabricas
{
    public interface IItemFabrica
    {
        Item CriarInstancia(Guid id, string nome);
    }

    public class ItemFabrica : IItemFabrica
    {
        public Item CriarInstancia(Guid id, string nome)
        {
            return new Item(id, nome);
        }
    }
}
