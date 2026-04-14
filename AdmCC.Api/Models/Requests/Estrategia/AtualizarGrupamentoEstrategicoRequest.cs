using System.ComponentModel.DataAnnotations;

namespace AdmCC.Api.Models.Requests.Estrategia
{
    public class AtualizarGrupamentoEstrategicoRequest
    {
        [Required]
        [StringLength(120, MinimumLength = 2)]
        public string Nome { get; set; } = string.Empty;
        [Required]
        [StringLength(4, MinimumLength = 1)]
        public string Sigla { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
    }
}
