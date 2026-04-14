using System.ComponentModel.DataAnnotations;

namespace AdmCC.Api.Models.Requests.OperacaoSemanalReuniaoCC
{
    public class CriarRegistroEducacionalRequest
    {
        public Guid AssociadoId { get; set; }
        public Guid ParametroPontuacaoEducacionalId { get; set; }
        [Required]
        [StringLength(150)]
        public string Titulo { get; set; } = string.Empty;
        [StringLength(100)]
        public string? CodigoExterno { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public bool Validado { get; set; }
    }
}
