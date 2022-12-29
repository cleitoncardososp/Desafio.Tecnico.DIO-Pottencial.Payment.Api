using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pottencial.Payment.Api.Infraestrutura.DTOs
{
    [Table("Item")]
    public class ItemDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public Guid VendaId { get; set; }

        [ForeignKey("VendaId")]
        public VendaDTO Venda { get; set; }

        //public int  VendaId { get; set; }
        //[ForeignKey("VendaId")]
        //public VendaDTO Venda { get; set; }
    }
}
