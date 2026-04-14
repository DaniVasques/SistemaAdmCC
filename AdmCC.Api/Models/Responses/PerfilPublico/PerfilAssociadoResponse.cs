namespace AdmCC.Api.Models.Responses.PerfilPublico
{
    public class PerfilAssociadoResponse
    {
        public Guid Id { get; set; }
        public Guid AssociadoId { get; set; }
        public string FotoProfissionalUrl { get; set; } = string.Empty;
        public string DescricaoProfissional { get; set; } = string.Empty;
        public string? NomeEmpresaExibicao { get; set; }
        public string? LogomarcaEmpresaUrl { get; set; }
        public string? Site { get; set; }
        public string? EmailPublico { get; set; }
        public bool PerfilPublicado { get; set; }
        public IReadOnlyCollection<MidiaAssociadoResponse> Midias { get; set; } = Array.Empty<MidiaAssociadoResponse>();
    }
}
