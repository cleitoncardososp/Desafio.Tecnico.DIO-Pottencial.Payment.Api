using Pottencial.Payment.Api.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pottencial.Payment.Api.Aplicacao.Queries.ListarTodasAsVendas
{
    public class ListarTodasAsVendasResponse
    {
        public List<Venda> Vendas { get; set; }
    }
}
