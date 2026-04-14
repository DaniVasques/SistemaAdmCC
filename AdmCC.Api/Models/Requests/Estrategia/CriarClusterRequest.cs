using System.ComponentModel.DataAnnotations;

namespace AdmCC.Api.Models.Requests.Estrategia
{
    public class CriarClusterRequest
    {
        [Required]
        [StringLength(120, MinimumLength = 2)]
        public string Nome { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
    }
}
