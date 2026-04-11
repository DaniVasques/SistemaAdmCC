using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities
{
    public class ReuniaoCC
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid CicloSemanalId { get; set; }
        public CicloSemanal CicloSemanal { get; set; } = null!;

        public Guid AssociadoOrigemId { get; set; }
        public AdmCC.Domain.CadastroBase.Entities.Associado AssociadoOrigem { get; set; } = null!;

        public Guid AssociadoDestinoId { get; set; }
        public AdmCC.Domain.CadastroBase.Entities.Associado AssociadoDestino { get; set; } = null!;

        public AdmCC.Domain.OperacaoSemanalReuniaoCC.Enums.TipoReuniaoCC TipoReuniaoCC { get; set; }
        public string? LocalReuniao { get; set; }
        public string? LinkReuniaoOnline { get; set; }

        public DateTime DataAgendada { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        public AdmCC.Domain.OperacaoSemanalReuniaoCC.Enums.StatusReuniaoCC StatusReuniaoCC { get; set; }
            = AdmCC.Domain.OperacaoSemanalReuniaoCC.Enums.StatusReuniaoCC.Pendente;

        public ICollection<ValidacaoReuniaoCC> Validacoes { get; set; }
            = new List<ValidacaoReuniaoCC>();
    }
}
