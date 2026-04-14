using AdmCC.Domain.VisitantesSubstitutos.Enums;

namespace AdmCC.Api.Models.Responses.VisitantesSubstitutos
{
    public class VisitaInternaResponse
    {
        public Guid Id { get; set; }
        public Guid OcorrenciaReuniaoEquipeId { get; set; }
        public string? NomeEquipe { get; set; }
        public Guid AssociadoVisitanteId { get; set; }
        public string? NomeAssociadoVisitante { get; set; }
        public StatusValidacaoPresenca StatusValidacaoPresenca { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataValidacao { get; set; }
    }
}
