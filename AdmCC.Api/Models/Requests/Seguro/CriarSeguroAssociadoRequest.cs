using System.ComponentModel.DataAnnotations;
using AdmCC.Domain.Seguro.Enums;

namespace AdmCC.Api.Models.Requests.Seguro
{
    public class CriarSeguroAssociadoRequest
    {
        public Guid AssociadoId { get; set; }

        [Required]
        public EstadoCivil EstadoCivil { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Profissao { get; set; } = string.Empty;

        [MinLength(1)]
        public IReadOnlyCollection<CriarBeneficiarioSeguroRequest> Beneficiarios { get; set; } = Array.Empty<CriarBeneficiarioSeguroRequest>();

        public CriarContatoEmergenciaRequest? ContatoEmergencia { get; set; }

        public RegistrarConsentimentoLgpdSeguroRequest? ConsentimentoLgpd { get; set; }
    }
}
