using Microsoft.EntityFrameworkCore;
using Pottencial.Payment.Api.Infraestrutura.DTOs;

namespace Pottencial.Payment.Api.Infraestrutura.Context
{
    public class VendasContext : DbContext
    {
        public VendasContext(DbContextOptions<VendasContext> options) : base(options)
        {

        }

        public DbSet<VendaDTO> Vendas { get; set; }
        public DbSet<VendedorDTO> Vendedores { get; set; }
        public DbSet<ItemDTO> Itens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VendedorDTO>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<VendaDTO>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<ItemDTO>()
                .HasKey(x => x.Id);
        }
    }
}
