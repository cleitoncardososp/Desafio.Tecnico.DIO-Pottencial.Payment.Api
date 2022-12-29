using Pottencial.Payment.Api.Dominio.Entidades;
using Pottencial.Payment.Api.Dominio.Enum;
using Pottencial.Payment.Api.Dominio.Excecoes;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace Pottencial.Payment.Api.Aplicacao.Tests
{
    public class VendaTests
    {
        [Fact]
        public void CadastrarVenda_Sucesso()
        {
            Vendedor vendedor = CriarVendedor();
            List<Item> listaDeItens = CriarListaDeItens();
            DateTime data = CriarDataDaVenda();

            Venda venda = new Venda(vendedor, listaDeItens, data);

            Assert.NotNull(venda);
        }

        [Fact]
        public void CadastrarVenda_VendedorFaltante_Falha()
        {
            List<Item> listaDeItens = CriarListaDeItens();
            DateTime data = CriarDataDaVenda();

            Assert.Throws<VendedorFaltanteException>(() => new Venda(null, listaDeItens, data));
        }

        [Fact]
        public void CadastrarVenda_ListaDeItensFaltante_Falha()
        {
            Vendedor vendedor = CriarVendedor();
            DateTime data = CriarDataDaVenda();

            Assert.Throws<ItensDaVendaFaltantesException>(() => new Venda(vendedor, null, data));
        }

        [Fact]
        public void CadastrarVenda_ItensFaltantes_Falha()
        {
            Vendedor vendedor = CriarVendedor();
            List<Item> listaDeItens = CriarListaDeItens();
            listaDeItens.RemoveRange(0,listaDeItens.Count);
            DateTime data = CriarDataDaVenda();

            Assert.Throws<ItensDaVendaFaltantesException>(() => new Venda(vendedor, listaDeItens, data));
        }


        [Fact]
        public void CadastrarVenda_DataDaVendaFaltante_Falha()
        {
            Vendedor vendedor = CriarVendedor();
            List<Item> listaDeItens = CriarListaDeItens();

            Assert.Throws<DataDaVendaFaltanteException>(() => new Venda(vendedor, listaDeItens, DateTime.MinValue));
        }

        [Fact]
        public void AtualizarStatusDaVenda_AguardandoPagamento_PagamentoAprovado_Sucesso()
        {
            Vendedor vendedor = CriarVendedor();
            List<Item> listaDeItens = CriarListaDeItens();
            DateTime data = CriarDataDaVenda();

            Venda venda = new Venda(vendedor, listaDeItens, data);

            venda.AtualizarStatus(EnumStatusDaVenda.PagamentoAprovado);

            Assert.Equal(EnumStatusDaVenda.PagamentoAprovado, venda.StatusDaVenda);
        }

        [Fact]
        public void AtualizarStatusDaVenda_AguardandoPagamento_Cancelada_Sucesso()
        {
            Vendedor vendedor = CriarVendedor();
            List<Item> listaDeItens = CriarListaDeItens();
            DateTime data = CriarDataDaVenda();

            Venda venda = new Venda(vendedor, listaDeItens, data);

            venda.AtualizarStatus(EnumStatusDaVenda.Cancelada);

            Assert.Equal(EnumStatusDaVenda.Cancelada, venda.StatusDaVenda);
        }

        [Fact]
        public void AtualizarStatusDaVenda_PagamentoAprovado_EnviadoParaTransportadora_Sucesso()
        {
            Vendedor vendedor = CriarVendedor();
            List<Item> listaDeItens = CriarListaDeItens();
            DateTime data = CriarDataDaVenda();

            Venda venda = new Venda(vendedor, listaDeItens, data);

            venda.StatusDaVenda = EnumStatusDaVenda.PagamentoAprovado;

            venda.AtualizarStatus(EnumStatusDaVenda.EnviadoParaTransportadora);

            Assert.Equal(EnumStatusDaVenda.EnviadoParaTransportadora, venda.StatusDaVenda);
        }

        [Fact]
        public void AtualizarStatusDaVenda_PagamentoAprovado_Cancelada_Sucesso()
        {
            Vendedor vendedor = CriarVendedor();
            List<Item> listaDeItens = CriarListaDeItens();
            DateTime data = CriarDataDaVenda();

            Venda venda = new Venda(vendedor, listaDeItens, data);

            venda.StatusDaVenda = EnumStatusDaVenda.PagamentoAprovado;

            venda.AtualizarStatus(EnumStatusDaVenda.Cancelada);

            Assert.Equal(EnumStatusDaVenda.Cancelada, venda.StatusDaVenda);
        }

        [Fact]
        public void AtualizarStatusDaVenda_EnviadoParaTransportadora_Entregue_Sucesso()
        {
            Vendedor vendedor = CriarVendedor();
            List<Item> listaDeItens = CriarListaDeItens();
            DateTime data = CriarDataDaVenda();

            Venda venda = new Venda(vendedor, listaDeItens, data);

            venda.StatusDaVenda = EnumStatusDaVenda.EnviadoParaTransportadora;

            venda.AtualizarStatus(EnumStatusDaVenda.Entregue);

            Assert.Equal(EnumStatusDaVenda.Entregue, venda.StatusDaVenda);
        }

        [Theory]
        [InlineData(EnumStatusDaVenda.AguardandoPagamento, EnumStatusDaVenda.EnviadoParaTransportadora)]
        [InlineData(EnumStatusDaVenda.AguardandoPagamento, EnumStatusDaVenda.Entregue)]
        [InlineData(EnumStatusDaVenda.PagamentoAprovado, EnumStatusDaVenda.AguardandoPagamento)]
        [InlineData(EnumStatusDaVenda.PagamentoAprovado, EnumStatusDaVenda.Entregue)]
        [InlineData(EnumStatusDaVenda.EnviadoParaTransportadora, EnumStatusDaVenda.AguardandoPagamento)]
        [InlineData(EnumStatusDaVenda.EnviadoParaTransportadora, EnumStatusDaVenda.PagamentoAprovado)]
        [InlineData(EnumStatusDaVenda.EnviadoParaTransportadora, EnumStatusDaVenda.Cancelada)]
        [InlineData(EnumStatusDaVenda.Cancelada, EnumStatusDaVenda.AguardandoPagamento)]
        [InlineData(EnumStatusDaVenda.Cancelada, EnumStatusDaVenda.PagamentoAprovado)]
        [InlineData(EnumStatusDaVenda.Cancelada, EnumStatusDaVenda.EnviadoParaTransportadora)]
        [InlineData(EnumStatusDaVenda.Cancelada, EnumStatusDaVenda.Entregue)]
        public void AtualizarStatusDaVenda_Falha(EnumStatusDaVenda statusAtual, EnumStatusDaVenda novoStatus)
        {
            Vendedor vendedor = CriarVendedor();
            List<Item> listaDeItens = CriarListaDeItens();
            DateTime data = CriarDataDaVenda();

            Venda venda = new Venda(vendedor, listaDeItens, data);

            venda.StatusDaVenda = statusAtual;

            Assert.Throws<AtualizacaoDeStatusIncompativelException>(() => venda.AtualizarStatus(novoStatus));
        }

        #region [ Métodos Auxiliares ]

        public Vendedor CriarVendedor()
        {
            return new Vendedor("12345678910", "Joaquim", "j@email.com", "11998761234");
        }

        public List<Item> CriarListaDeItens()
        {
            return new List<Item>()
            {
                new Item("Lápis"),
                new Item("Caneta")
            };
        }

        public DateTime CriarDataDaVenda()
        {
            return new DateTime(2020, 08, 10, new GregorianCalendar());
        }
        #endregion

    }
}
