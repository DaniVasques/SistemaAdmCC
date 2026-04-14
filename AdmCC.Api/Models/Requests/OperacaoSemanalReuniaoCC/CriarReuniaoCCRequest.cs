using System.ComponentModel.DataAnnotations;
using AdmCC.Domain.OperacaoSemanalReuniaoCC.Enums;

namespace AdmCC.Api.Models.Requests.OperacaoSemanalReuniaoCC
{
    public class CriarReuniaoCCRequest
    {
        public Guid CicloSemanalId { get; set; }
        public Guid AssociadoOrigemId { get; set; }
        public Guid AssociadoDestinoId { get; set; }
        [Required]
        public TipoReuniaoCC TipoReuniaoCC { get; set; }
        [StringLength(200)]
        public string? LocalReuniao { get; set; }
        [StringLength(500)]
        public string? LinkReuniaoOnline { get; set; }
        [Required]
        public DateTime DataAgendada { get; set; }
        public StatusReuniaoCC StatusReuniaoCC { get; set; } = StatusReuniaoCC.Pendente;
    }
}
