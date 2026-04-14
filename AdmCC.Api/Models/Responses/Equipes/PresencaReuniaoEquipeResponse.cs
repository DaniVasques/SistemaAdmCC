namespace AdmCC.Api.Models.Responses.Equipes
{
    public class PresencaReuniaoEquipeResponse
    {
        public Guid Id { get; set; }
        public Guid OcorrenciaReuniaoEquipeId { get; set; }
        public Guid AssociadoId { get; set; }
        public string? NomeAssociado { get; set; }
        public bool Presente { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
