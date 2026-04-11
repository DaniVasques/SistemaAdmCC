using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.Seguro.Entities
{
    public class SeguroAssociado
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid AssociadoId { get; set; }
        public AdmCC.Domain.CadastroBase.Entities.Associado Associado { get; set; } = null!;

        public AdmCC.Domain.Seguro.Enums.EstadoCivil EstadoCivil { get; set; }
        public string Profissao { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        public ICollection<BeneficiarioSeguro> Beneficiarios { get; set; }
            = new List<BeneficiarioSeguro>();

        public ContatoEmergencia? ContatoEmergencia { get; set; }
        public ConsentimentoLgpdSeguro? ConsentimentoLgpd { get; set; }

        public ICollection<SolicitacaoAlteracaoBeneficiario> SolicitacoesAlteracaoBeneficiario { get; set; }
            = new List<SolicitacaoAlteracaoBeneficiario>();
    }
}
