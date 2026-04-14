using System.ComponentModel.DataAnnotations;

namespace AdmCC.Api.Models.Requests.OperacaoSemanalReuniaoCC
{
    public class CriarCicloSemanalRequest
    {
        [Required]
        public DateTime DataInicio { get; set; }
        [Required]
        public DateTime DataEncerramento { get; set; }
        [Range(1, 12)]
        public int MesReferencia { get; set; }
        [Range(2000, 9999)]
        public int AnoReferencia { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
