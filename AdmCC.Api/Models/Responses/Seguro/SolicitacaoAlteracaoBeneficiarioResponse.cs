using AdmCC.Domain.Seguro.Enums;

namespace AdmCC.Api.Models.Responses.Seguro
{
    public class SolicitacaoAlteracaoBeneficiarioResponse
    {
        public Guid Id { get; set; }
        public Guid SeguroAssociadoId { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public StatusSolicitacaoBeneficiario StatusSolicitacaoBeneficiario { get; set; }
        public string? ObservacaoSolicitante { get; set; }
        public string? ObservacaoAnalise { get; set; }
        public DateTime? DataConclusao { get; set; }
    }
}
