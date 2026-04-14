using AdmCC.Domain.Seguro.Enums;

namespace AdmCC.Api.Models.Responses.Seguro
{
    public class SeguroAssociadoResponse
    {
        public Guid Id { get; set; }
        public Guid AssociadoId { get; set; }
        public EstadoCivil EstadoCivil { get; set; }
        public string Profissao { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; }
        public IReadOnlyCollection<BeneficiarioSeguroResponse> Beneficiarios { get; set; } = Array.Empty<BeneficiarioSeguroResponse>();
        public ContatoEmergenciaResponse? ContatoEmergencia { get; set; }
        public ConsentimentoLgpdSeguroResponse? ConsentimentoLgpd { get; set; }
        public IReadOnlyCollection<SolicitacaoAlteracaoBeneficiarioResponse> SolicitacoesAlteracaoBeneficiario { get; set; } = Array.Empty<SolicitacaoAlteracaoBeneficiarioResponse>();
    }
}
