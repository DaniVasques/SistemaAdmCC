namespace AdmCC.Api.Models.Responses.OperacaoSemanalReuniaoCC
{
    public class ValidacaoReuniaoCCResponse
    {
        public Guid Id { get; set; }
        public Guid ReuniaoCCId { get; set; }
        public Guid AssociadoId { get; set; }
        public string? NomeAssociado { get; set; }
        public DateTime DataValidacao { get; set; }
        public bool NaoEncontrouProspect { get; set; }
        public int PontosGerados { get; set; }
        public IReadOnlyCollection<ProspectReuniaoCCResponse> Prospects { get; set; } = Array.Empty<ProspectReuniaoCCResponse>();
    }
}
