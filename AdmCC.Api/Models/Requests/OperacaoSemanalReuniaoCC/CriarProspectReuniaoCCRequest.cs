using System.ComponentModel.DataAnnotations;

namespace AdmCC.Api.Models.Requests.OperacaoSemanalReuniaoCC
{
    public class CriarProspectReuniaoCCRequest
    {
        public Guid ValidacaoReuniaoCCId { get; set; }
        [Required]
        [StringLength(150)]
        public string NomeProspect { get; set; } = string.Empty;
        [Required]
        [StringLength(150)]
        public string NomeEmpresa { get; set; } = string.Empty;
        public bool CompartilhouApenasContato { get; set; }
    }
}
