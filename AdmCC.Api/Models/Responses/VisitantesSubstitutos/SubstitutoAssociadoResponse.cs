using AdmCC.Domain.VisitantesSubstitutos.Enums;

namespace AdmCC.Api.Models.Responses.VisitantesSubstitutos
{
    public class SubstitutoAssociadoResponse
    {
        public Guid Id { get; set; }
        public Guid OcorrenciaReuniaoEquipeId { get; set; }
        public string? NomeEquipe { get; set; }
        public Guid AssociadoTitularId { get; set; }
        public string? NomeAssociadoTitular { get; set; }
        public Guid AssociadoSubstitutoId { get; set; }
        public string? NomeAssociadoSubstituto { get; set; }
        public StatusValidacaoPresenca StatusValidacaoPresenca { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataValidacao { get; set; }
    }
}
