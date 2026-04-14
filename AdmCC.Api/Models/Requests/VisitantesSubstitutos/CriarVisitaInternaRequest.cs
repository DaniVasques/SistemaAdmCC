namespace AdmCC.Api.Models.Requests.VisitantesSubstitutos
{
    public class CriarVisitaInternaRequest
    {
        public Guid OcorrenciaReuniaoEquipeId { get; set; }
        public Guid AssociadoVisitanteId { get; set; }
    }
}
