namespace AdmCC.Api.Models.Responses.Equipes
{
    public class OcorrenciaReuniaoEquipeResponse
    {
        public Guid Id { get; set; }
        public Guid EquipeId { get; set; }
        public string? NomeEquipe { get; set; }
        public DateTime DataReuniao { get; set; }
        public int NumeroOcorrenciaNoMes { get; set; }
        public bool EhPresencial { get; set; }
        public bool Realizada { get; set; }
        public IReadOnlyCollection<PresencaReuniaoEquipeResponse> Presencas { get; set; } = Array.Empty<PresencaReuniaoEquipeResponse>();
    }
}
