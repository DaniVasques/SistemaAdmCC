using System.ComponentModel.DataAnnotations;
using AdmCC.Domain.CargosLiderancas.Enums;

namespace AdmCC.Api.Models.Requests.CargosLiderancas
{
    public class CriarCargoLiderancaRequest
    {
        [Required]
        [StringLength(120, MinimumLength = 2)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public ClassificacaoCargo ClassificacaoCargo { get; set; }

        public bool Ativo { get; set; } = true;
    }
}
