using System.ComponentModel.DataAnnotations;
using AdmCC.Domain.OperacaoSemanalReuniaoCC.Enums;

namespace AdmCC.Api.Models.Requests.OperacaoSemanalReuniaoCC
{
    public class AtualizarParametroPontuacaoEducacionalRequest
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;
        public TipoPontuacaoEducacional TipoPontuacaoEducacional { get; set; }
        [Range(1, int.MaxValue)]
        public int Pontos { get; set; }
        public bool Ativo { get; set; }
    }
}
