using System.ComponentModel.DataAnnotations;

namespace AdmCC.Api.Models.Requests.PerfilPublico
{
    public class AtualizarMidiaAssociadoRequest
    {
        [Required]
        [StringLength(80, MinimumLength = 2)]
        public string NomeMidia { get; set; } = string.Empty;
        [Required]
        [StringLength(500, MinimumLength = 5)]
        public string Url { get; set; } = string.Empty;
        [Range(0, int.MaxValue)]
        public int OrdemExibicao { get; set; }
        public bool Ativa { get; set; } = true;
    }
}
