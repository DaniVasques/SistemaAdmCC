namespace AdmCC.Api.Models.Requests.VisitantesSubstitutos
{
    public class CriarSubstitutoAssociadoRequest
    {
        public Guid OcorrenciaReuniaoEquipeId { get; set; }
        public Guid AssociadoTitularId { get; set; }
        public Guid AssociadoSubstitutoId { get; set; }
    }
}
