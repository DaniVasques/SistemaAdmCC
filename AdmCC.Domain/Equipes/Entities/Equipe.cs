using AdmCC.Domain.CadastroBase.Entities;
using AdmCC.Domain.Equipes.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.Equipes.Entities
{
    public class Equipe
    {
        public Guid Id { get; set; }

        public string NomeEquipe { get; set; } = string.Empty;

        public DateTime DataInicioFormacao { get; set; }
        public DateTime DataPrevisaoLancamento { get; set; }
        public DateTime? DataEfetivaLancamento { get; set; }

        public StatusEquipe StatusEquipe { get; set; } = StatusEquipe.EmFormacao;

        public DiaReuniaoEquipe DiaReuniaoEquipe { get; set; }
        public TimeOnly HorarioReuniao { get; set; }

        public ModeloReuniaoDeEquipe ModeloReuniaoDeEquipe { get; set; }

        public Guid LocalReuniaoPresencialId { get; set; }
        public Endereco LocalReuniaoPresencial { get; set; } = null!;

        public string LinkReuniaoOnline { get; set; } = string.Empty;

        public int NumeroComponentesAtivos { get; set; }
        public int PontuacaoMensalAtual { get; set; }

        public ICollection<OcorrenciaReuniaoEquipe> OcorrenciasReuniao { get; set; }
            = new List<OcorrenciaReuniaoEquipe>();
    }
}

