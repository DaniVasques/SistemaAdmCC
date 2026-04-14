using System.ComponentModel.DataAnnotations;

namespace AdmCC.Api.Models.Requests.Seguro
{
    public class AtualizarContatoEmergenciaRequest
    {
        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string TelefonePrincipal { get; set; } = string.Empty;

        [StringLength(20)]
        public string? TelefoneSecundario { get; set; }
    }
}
