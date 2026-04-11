using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.Equipes.Entities
{
    public class OcorrenciaReuniaoEquipe
    {
        public Guid Id { get; set; }

        public Guid EquipeId { get; set; }
        public Equipe Equipe { get; set; } = null!;

        public DateTime DataReuniao { get; set; }
        public int NumeroOcorrenciaNoMes { get; set; }

        public bool EhPresencial { get; set; }

        public bool Realizada { get; set; }

        public ICollection<PresencaReuniaoEquipe> Presencas { get; set; }
            = new List<PresencaReuniaoEquipe>();
    }
}

