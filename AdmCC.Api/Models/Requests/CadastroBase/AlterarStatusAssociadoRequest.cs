using System.ComponentModel.DataAnnotations;
using AdmCC.Domain.CadastroBase.Enums;

namespace AdmCC.Api.Models.Requests.CadastroBase
{
    public class AlterarStatusAssociadoRequest
    {
        [Required]
        public StatusAssociado NovoStatus { get; set; }

        [StringLength(500)]
        public string? Motivo { get; set; }

        public Guid UsuarioResponsavelId { get; set; }
    }
}
