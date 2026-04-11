using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.Seguro.Entities
{
    public class SolicitacaoAlteracaoBeneficiario
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid SeguroAssociadoId { get; set; }
        public SeguroAssociado SeguroAssociado { get; set; } = null!;

        public DateTime DataSolicitacao { get; set; } = DateTime.UtcNow;
        public AdmCC.Domain.Seguro.Enums.StatusSolicitacaoBeneficiario StatusSolicitacaoBeneficiario { get; set; }
            = AdmCC.Domain.Seguro.Enums.StatusSolicitacaoBeneficiario.Solicitada;

        public string? ObservacaoSolicitante { get; set; }
        public string? ObservacaoAnalise { get; set; }
        public DateTime? DataConclusao { get; set; }
    }
}
