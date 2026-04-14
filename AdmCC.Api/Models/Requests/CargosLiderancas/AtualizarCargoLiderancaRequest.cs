using System.ComponentModel.DataAnnotations;
using AdmCC.Domain.CargosLiderancas.Enums;

namespace AdmCC.Api.Models.Requests.CargosLiderancas
{
    public class AtualizarCargoLiderancaRequest
    {
        [Required]
        [StringLength(120, MinimumLength = 2)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public ClassificacaoCargo ClassificacaoCargo { get; set; }

        [Required]
        public bool Ativo { get; set; }
    }
}
