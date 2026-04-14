namespace AdmCC.Api.Models.Responses.Seguro
{
    public class ConsentimentoLgpdSeguroResponse
    {
        public Guid Id { get; set; }
        public Guid SeguroAssociadoId { get; set; }
        public bool Aceito { get; set; }
        public DateTime? DataAceite { get; set; }
        public string TextoConsentimento { get; set; } = string.Empty;
    }
}
