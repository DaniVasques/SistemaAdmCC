namespace AdmCC.Api.Models.Responses.Seguro
{
    public class ContatoEmergenciaResponse
    {
        public Guid Id { get; set; }
        public Guid SeguroAssociadoId { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public string TelefonePrincipal { get; set; } = string.Empty;
        public string? TelefoneSecundario { get; set; }
    }
}
