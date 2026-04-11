using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.VisitantesSubstitutos.Entities
{
    public class VisitaInterna
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid OcorrenciaReuniaoEquipeId { get; set; }
        public AdmCC.Domain.Equipes.Entities.OcorrenciaReuniaoEquipe OcorrenciaReuniaoEquipe { get; set; } = null!;

        public Guid AssociadoVisitanteId { get; set; }
        public AdmCC.Domain.CadastroBase.Entities.Associado AssociadoVisitante { get; set; } = null!;

        public AdmCC.Domain.VisitantesSubstitutos.Enums.StatusValidacaoPresenca StatusValidacaoPresenca { get; set; }
            = AdmCC.Domain.VisitantesSubstitutos.Enums.StatusValidacaoPresenca.Pendente;

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public DateTime? DataValidacao { get; set; }
    }
}
