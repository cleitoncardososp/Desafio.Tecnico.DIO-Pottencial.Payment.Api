using Microsoft.EntityFrameworkCore;
using Pottencial.Payment.Api.Aplicacao.Interfaces;
using Pottencial.Payment.Api.Dominio.Entidades;
using Pottencial.Payment.Api.Dominio.Enum;
using Pottencial.Payment.Api.Dominio.Excecoes;
using Pottencial.Payment.Api.Dominio.Fabricas;
using Pottencial.Payment.Api.Infraestrutura.Context;
using Pottencial.Payment.Api.Infraestrutura.DTOs;

namespace Pottencial.Payment.Api.Infraestrutura.Repositorios
{
    public class VendaRepositorio : IVendaRepositorio
    {
        public VendasContext _context { get; set; }
        public IVendaFabrica VendaFabrica { get; set; }
        public IItemFabrica ItemFabrica { get; set; }
        public IVendedorFabrica VendedorFabrica { get; set; }

        public VendaRepositorio(VendasContext context, IVendaFabrica vendaFabrica, IItemFabrica itemFabrica, IVendedorFabrica vendedorFabrica)
        {
            _context = context;
            VendaFabrica = vendaFabrica;
            ItemFabrica = itemFabrica;
            VendedorFabrica = vendedorFabrica;
        }

        public Venda BuscarVendaPorId(Guid vendaId)
        {
            VendaDTO vendaDto = _context.Vendas.Where(v => v.Id == vendaId)
                                                .Include(v => v.Vendedor)
                                                .Include(v => v.Itens)
                                                .FirstOrDefault();

            if (vendaDto == null)
                throw new VendaNaoLocalizadaException($"Venda não localizada - ID: {vendaId}");

            Vendedor vendedor = VendedorFabrica.CriarInstancia(vendaDto.Vendedor.Id,
                                                               vendaDto.Vendedor.Cpf,
                                                               vendaDto.Vendedor.Nome,
                                                               vendaDto.Vendedor.Email,
                                                               vendaDto.Vendedor.Telefone);

            List<Item> listaDeItens = new List<Item>();
            foreach (var registro in vendaDto.Itens)
            {
                Item item = ItemFabrica.CriarInstancia(registro.Id, registro.Nome);
                listaDeItens.Add(item);
            }

            Venda venda = VendaFabrica.CriarInstancia(vendaDto.Id, vendedor, listaDeItens, vendaDto.DataDaVenda, vendaDto.StatusDaVenda);

            return venda;
        }

        public void CadastrarVenda(Venda venda)
        {
            VendedorDTO vendedorDto = new VendedorDTO()
            {
                Id = venda.Vendedor.Id,
                Cpf = venda.Vendedor.Cpf,
                Nome = venda.Vendedor.Nome,
                Email = venda.Vendedor.Email,
                Telefone = venda.Vendedor.Telefone
            };

            _context.Vendedores.Add(vendedorDto);
            _context.SaveChanges();


            List<ItemDTO> listaDeItensDto = new List<ItemDTO>();

            VendaDTO vendaDto = new VendaDTO()
            {
                Id = venda.Id,
                VendedorId = vendedorDto.Id,
                DataDaVenda = venda.DataDaVenda,
                StatusDaVenda = venda.StatusDaVenda,
                Itens = listaDeItensDto
            };

            _context.Vendas.Add(vendaDto);
            _context.SaveChanges();

            foreach (var registro in venda.Itens)
            {
                ItemDTO item = new ItemDTO()
                {
                    Id = registro.Id,
                    Nome = registro.Nome
                };

                listaDeItensDto.Add(item);
            }

            _context.Itens.AddRange(listaDeItensDto);
            _context.SaveChanges();

        }

        public void Atualizar(Venda venda)
        {
            VendaDTO vendaToUpdate = _context.Vendas.Where(v => v.Id == venda.Id)
                                                .Include(v => v.Vendedor)
                                                .Include(v => v.Itens)
                                                .FirstOrDefault();

            if (vendaToUpdate == null)
                throw new VendaNaoLocalizadaException($"Venda não localizada - ID: {venda.Id}");

            vendaToUpdate.StatusDaVenda = venda.StatusDaVenda;

            _context.Vendas.Update(vendaToUpdate);
            _context.SaveChanges();
        }

        public List<Venda> ListarTodasAsVendas()
        {
            List<VendaDTO> listaDeVendasDto = _context.Vendas
                                                      .Include(v => v.Vendedor)
                                                      .Include(v => v.Itens)
                                                      .ToList();

            List<Venda> listaDeVendas = new List<Venda>();


            for (int i = 0; i < listaDeVendasDto.Count; i++)
            {
                
                Vendedor vendedor = VendedorFabrica.CriarInstancia(listaDeVendasDto[i].Vendedor.Id,
                                                                   listaDeVendasDto[i].Vendedor.Cpf,
                                                                   listaDeVendasDto[i].Vendedor.Nome,
                                                                   listaDeVendasDto[i].Vendedor.Email,
                                                                   listaDeVendasDto[i].Vendedor.Telefone);

                List<Item> listaDeItens = new List<Item>();

                for (int item = 0; item < listaDeVendasDto[i].Itens.Count; item++)
                {
                    Item itemDaVenda = ItemFabrica.CriarInstancia(listaDeVendasDto[i].Itens[item].Id, listaDeVendasDto[i].Itens[item].Nome);

                    listaDeItens.Add(itemDaVenda);
                }

                Venda venda = VendaFabrica.CriarInstancia(listaDeVendasDto[i].Id, vendedor, listaDeItens, listaDeVendasDto[i].DataDaVenda,listaDeVendasDto[i].StatusDaVenda);

                listaDeVendas.Add(venda);
            }

            return listaDeVendas;
        }
    }
}
