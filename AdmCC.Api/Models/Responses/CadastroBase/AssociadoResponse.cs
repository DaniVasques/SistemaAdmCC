using AdmCC.Domain.CadastroBase.Enums;

namespace AdmCC.Api.Models.Responses.CadastroBase
{
    public class AssociadoResponse
    {
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public bool PermitirExibirAniversario { get; set; }
        public string EmailPrincipal { get; set; } = string.Empty;
        public string TelefoneWhatsappPrincipal { get; set; } = string.Empty;
        public DateTime DataIngresso { get; set; }
        public DateTime DataCadastro { get; set; }
        public StatusAssociado StatusAssociado { get; set; }
        public Guid EnderecoId { get; set; }
        public Guid EmpresaId { get; set; }
        public Guid AnuidadeId { get; set; }
        public Guid PadrinhoId { get; set; }
        public Guid EquipeOrigemId { get; set; }
        public Guid EquipeAtualId { get; set; }
        public Guid ClusterId { get; set; }
        public string? ClusterNome { get; set; }
        public Guid AtuacaoEspecificaId { get; set; }
        public string? AtuacaoEspecificaNome { get; set; }
        public string? PadrinhoNome { get; set; }
        public IReadOnlyCollection<string> Grupamentos { get; set; } = Array.Empty<string>();
    }
}
