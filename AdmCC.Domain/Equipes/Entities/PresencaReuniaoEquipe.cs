using AdmCC.Domain.CadastroBase.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.Equipes.Entities
{
    public class PresencaReuniaoEquipe
    {
        public Guid Id { get; set; }

        public Guid OcorrenciaReuniaoEquipeId { get; set; }
        public OcorrenciaReuniaoEquipe OcorrenciaReuniaoEquipe { get; set; } = null!;

        public Guid AssociadoId { get; set; }
        public Associado Associado { get; set; } = null!;

        public bool Presente { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}
