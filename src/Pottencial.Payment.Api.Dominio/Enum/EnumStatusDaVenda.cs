using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Pottencial.Payment.Api.Dominio.Enum
{
    public enum EnumStatusDaVenda
    {
        [Display(Name = "Aguardando Pagamento")]
        AguardandoPagamento = 1,
        [Display(Name = "Pagamento Aprovado")]
        PagamentoAprovado = 2,
        [Display(Name = "Enviado Para Transportadora")]
        EnviadoParaTransportadora = 3,
        [Display(Name = "Entregue")]
        Entregue = 4,
        [Display(Name = "Cancelada")]
        Cancelada = 5
    }
}
