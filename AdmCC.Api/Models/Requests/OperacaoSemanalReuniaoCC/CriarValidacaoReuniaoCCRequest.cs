using System.ComponentModel.DataAnnotations;

namespace AdmCC.Api.Models.Requests.OperacaoSemanalReuniaoCC
{
    public class CriarValidacaoReuniaoCCRequest
    {
        public Guid AssociadoId { get; set; }
        public DateTime? DataValidacao { get; set; }
        public bool NaoEncontrouProspect { get; set; }
        [Range(0, int.MaxValue)]
        public int PontosGerados { get; set; } = 5;
    }
}
