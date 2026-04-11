using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.ConexoesParcerias.Entities
{
    public class ParceriaAssociado
    {
        public Guid Id { get; set; }

        public Guid AssociadoOrigemId { get; set; }
        public Guid AssociadoDestinoId { get; set; }

        public DateTime DataParceria { get; set; }

        public bool Ativa { get; set; } = true;
    }
}
