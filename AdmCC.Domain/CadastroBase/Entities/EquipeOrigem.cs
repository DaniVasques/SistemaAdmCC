using AdmCC.Domain.CadastroBase.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.CadastroBase.Entities
{
    public class EquipeOrigem
    {
        public Guid Id { get; set; }

        public OrigemEquipeEnum TipoOrigem { get; set; }
        public Guid EquipeOrigemId { get; set; }
    }
}
