using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Pottencial.Payment.Api.Infraestrutura.DTOs
{
    [Table("Vendedor")]
    public class VendedorDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Cpf { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Telefone { get; set; }

        public VendaDTO Venda { get; set; }
    }
}
