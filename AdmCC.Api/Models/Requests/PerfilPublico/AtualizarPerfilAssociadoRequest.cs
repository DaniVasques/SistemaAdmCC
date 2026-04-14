using System.ComponentModel.DataAnnotations;

namespace AdmCC.Api.Models.Requests.PerfilPublico
{
    public class AtualizarPerfilAssociadoRequest
    {
        public Guid AssociadoId { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 5)]
        public string FotoProfissionalUrl { get; set; } = string.Empty;
        [Required]
        [StringLength(2000, MinimumLength = 10)]
        public string DescricaoProfissional { get; set; } = string.Empty;
        [StringLength(150)]
        public string? NomeEmpresaExibicao { get; set; }
        [StringLength(500)]
        public string? LogomarcaEmpresaUrl { get; set; }
        [StringLength(500)]
        public string? Site { get; set; }
        [EmailAddress]
        [StringLength(150)]
        public string? EmailPublico { get; set; }
        public bool PerfilPublicado { get; set; }
        public IReadOnlyCollection<AtualizarMidiaAssociadoRequest> Midias { get; set; } = Array.Empty<AtualizarMidiaAssociadoRequest>();
    }
}
