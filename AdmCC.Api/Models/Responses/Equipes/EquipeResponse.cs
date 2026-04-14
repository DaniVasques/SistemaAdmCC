using AdmCC.Domain.Equipes.Enums;

namespace AdmCC.Api.Models.Responses.Equipes
{
    public class EquipeResponse
    {
        public Guid Id { get; set; }
        public string NomeEquipe { get; set; } = string.Empty;
        public DateTime DataInicioFormacao { get; set; }
        public DateTime DataPrevisaoLancamento { get; set; }
        public DateTime? DataEfetivaLancamento { get; set; }
        public StatusEquipe StatusEquipe { get; set; }
        public DiaReuniaoEquipe DiaReuniaoEquipe { get; set; }
        public TimeOnly HorarioReuniao { get; set; }
        public ModeloReuniaoDeEquipe ModeloReuniaoDeEquipe { get; set; }
        public Guid LocalReuniaoPresencialId { get; set; }
        public string LinkReuniaoOnline { get; set; } = string.Empty;
        public int NumeroComponentesAtivos { get; set; }
        public int PontuacaoMensalAtual { get; set; }
        public int QuantidadeOcorrencias { get; set; }
    }
}
