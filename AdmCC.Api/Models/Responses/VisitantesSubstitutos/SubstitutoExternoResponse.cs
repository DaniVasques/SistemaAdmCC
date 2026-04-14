using AdmCC.Domain.VisitantesSubstitutos.Enums;

namespace AdmCC.Api.Models.Responses.VisitantesSubstitutos
{
    public class SubstitutoExternoResponse
    {
        public Guid Id { get; set; }
        public Guid OcorrenciaReuniaoEquipeId { get; set; }
        public string? NomeEquipe { get; set; }
        public Guid AssociadoTitularId { get; set; }
        public string? NomeAssociadoTitular { get; set; }
        public TipoPessoa TipoPessoa { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public string TelefonePrincipal { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Cpf { get; set; }
        public string? NomeEmpresa { get; set; }
        public StatusValidacaoPresenca StatusValidacaoPresenca { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataValidacao { get; set; }
    }
}
