using Pottencial.Payment.Api.Dominio.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Pottencial.Payment.Api.Infraestrutura.DTOs
{
    [Table("Venda")]
    public class VendaDTO
    {
        [Key]
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required]
        public Guid VendedorId { get; set; }

        [Required]
        public DateTime DataDaVenda { get; set; }

        [Required]
        public EnumStatusDaVenda StatusDaVenda { get; set; }

        [ForeignKey("VendedorId")]
        public VendedorDTO Vendedor { get; set; }

        public List<ItemDTO> Itens { get; set; }
    }
}
