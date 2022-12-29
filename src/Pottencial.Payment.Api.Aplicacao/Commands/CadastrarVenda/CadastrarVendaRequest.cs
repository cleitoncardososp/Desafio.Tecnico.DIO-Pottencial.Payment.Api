using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Pottencial.Payment.Api.Aplicacao.Commands.CadastrarVenda
{
    public class CadastrarVendaRequest : IRequest<CadastrarVendaResponse>
    {
        public DateTime DataDaVenda { get; set; }
        
        [Required(ErrorMessage = "Vendedor é obrigatório!")]
        public VendedorDTO Vendedor { get; set; }

        [Required(ErrorMessage = "Lista de Itens é obrigatório!")]
        public List<ItemDTO> ListaDeItens { get; set; }


        public class VendedorDTO
        {
            [Required(ErrorMessage = "CPF é obrigatório!")]
            public string Cpf { get; set; }

            [Required(ErrorMessage = "Nome é obrigatório!")]
            public string Nome { get; set; }

            [Required(ErrorMessage = "E-mail é obrigatório!")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Telefone é obrigatório!")]
            public string Telefone { get; set; }
        }

        public class ItemDTO
        {
            [Required(ErrorMessage = "Item da venda é obrigatório!")]
            public string Nome { get; set; }
        }
    }
}
