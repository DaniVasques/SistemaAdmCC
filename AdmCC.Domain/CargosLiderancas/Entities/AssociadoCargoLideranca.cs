using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.CargosLiderancas.Entities
{
    public class AssociadoCargoLideranca
    {
        public Guid Id { get; set; }

        public Guid AssociadoId { get; set; }
        public Guid CargoLiderancaId { get; set; }

        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        public bool Ativo { get; set; } = true;
    }
}
