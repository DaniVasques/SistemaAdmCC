using System.ComponentModel.DataAnnotations;
using AdmCC.Domain.CadastroBase.Enums;

namespace AdmCC.Api.Models.Requests.CadastroBase
{
    public class AtualizarAssociadoRequest
    {
        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required]
        [StringLength(14, MinimumLength = 11)]
        public string Cpf { get; set; } = string.Empty;

        [Required]
        public DateTime DataNascimento { get; set; }

        public bool PermitirExibirAniversario { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string EmailPrincipal { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(20)]
        public string TelefoneWhatsappPrincipal { get; set; } = string.Empty;

        [Required]
        public DateTime DataIngresso { get; set; }

        [Required]
        public StatusAssociado StatusAssociado { get; set; }

        public Guid EnderecoId { get; set; }
        public Guid EmpresaId { get; set; }
        public Guid AnuidadeId { get; set; }
        public Guid PadrinhoId { get; set; }
        public Guid EquipeOrigemId { get; set; }
        public Guid EquipeAtualId { get; set; }
        public Guid ClusterId { get; set; }
        public Guid AtuacaoEspecificaId { get; set; }
    }
}
