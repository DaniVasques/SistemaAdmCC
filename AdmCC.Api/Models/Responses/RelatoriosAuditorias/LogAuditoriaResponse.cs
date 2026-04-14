namespace AdmCC.Api.Models.Responses.RelatoriosAuditorias
{
    public class LogAuditoriaResponse
    {
        public Guid Id { get; set; }
        public string Entidade { get; set; } = string.Empty;
        public string Acao { get; set; } = string.Empty;
        public Guid EntidadeId { get; set; }
        public Guid UsuarioResponsavelId { get; set; }
        public DateTime DataHora { get; set; }
        public string? DadosAnterioresJson { get; set; }
        public string? DadosNovosJson { get; set; }
    }
}
