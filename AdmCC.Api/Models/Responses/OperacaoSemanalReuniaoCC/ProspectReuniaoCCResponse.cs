namespace AdmCC.Api.Models.Responses.OperacaoSemanalReuniaoCC
{
    public class ProspectReuniaoCCResponse
    {
        public Guid Id { get; set; }
        public Guid ValidacaoReuniaoCCId { get; set; }
        public string NomeProspect { get; set; } = string.Empty;
        public string NomeEmpresa { get; set; } = string.Empty;
        public bool CompartilhouApenasContato { get; set; }
    }
}
