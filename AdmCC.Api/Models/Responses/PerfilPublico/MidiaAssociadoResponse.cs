namespace AdmCC.Api.Models.Responses.PerfilPublico
{
    public class MidiaAssociadoResponse
    {
        public Guid Id { get; set; }
        public Guid PerfilAssociadoId { get; set; }
        public string NomeMidia { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public int OrdemExibicao { get; set; }
        public bool Ativa { get; set; }
    }
}
