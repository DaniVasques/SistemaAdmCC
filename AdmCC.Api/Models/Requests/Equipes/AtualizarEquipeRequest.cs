using System.ComponentModel.DataAnnotations;
using AdmCC.Domain.Equipes.Enums;

namespace AdmCC.Api.Models.Requests.Equipes
{
    public class AtualizarEquipeRequest
    {
        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string NomeEquipe { get; set; } = string.Empty;

        [Required]
        public DateTime DataInicioFormacao { get; set; }

        [Required]
        public DateTime DataPrevisaoLancamento { get; set; }

        public DateTime? DataEfetivaLancamento { get; set; }

        [Required]
        public StatusEquipe StatusEquipe { get; set; }

        [Required]
        public DiaReuniaoEquipe DiaReuniaoEquipe { get; set; }

        [Required]
        public TimeOnly HorarioReuniao { get; set; }

        [Required]
        public ModeloReuniaoDeEquipe ModeloReuniaoDeEquipe { get; set; }

        public Guid LocalReuniaoPresencialId { get; set; }

        [StringLength(500)]
        public string LinkReuniaoOnline { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int NumeroComponentesAtivos { get; set; }

        [Range(0, int.MaxValue)]
        public int PontuacaoMensalAtual { get; set; }
    }
}
