using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities
{
    public class CicloSemanal
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime DataInicio { get; set; }
        public DateTime DataEncerramento { get; set; }

        public int MesReferencia { get; set; }
        public int AnoReferencia { get; set; }

        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        public ICollection<ReuniaoCC> ReunioesCC { get; set; }
            = new List<ReuniaoCC>();
    }
}
