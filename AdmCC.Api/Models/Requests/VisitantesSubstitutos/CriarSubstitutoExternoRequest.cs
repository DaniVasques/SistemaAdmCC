using AdmCC.Domain.VisitantesSubstitutos.Enums;

namespace AdmCC.Api.Models.Requests.VisitantesSubstitutos
{
    public class CriarSubstitutoExternoRequest
    {
        public Guid OcorrenciaReuniaoEquipeId { get; set; }
        public Guid AssociadoTitularId { get; set; }
        public TipoPessoa TipoPessoa { get; set; } = TipoPessoa.Pessoa;
        public string NomeCompleto { get; set; } = string.Empty;
        public string TelefonePrincipal { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Cpf { get; set; }
        public string? NomeEmpresa { get; set; }
    }
}
