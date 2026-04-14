using AdmCC.Domain.OperacaoSemanalReuniaoCC.Enums;

namespace AdmCC.Api.Models.Responses.OperacaoSemanalReuniaoCC
{
    public class ReuniaoCCResponse
    {
        public Guid Id { get; set; }
        public Guid CicloSemanalId { get; set; }
        public Guid AssociadoOrigemId { get; set; }
        public string? NomeAssociadoOrigem { get; set; }
        public Guid AssociadoDestinoId { get; set; }
        public string? NomeAssociadoDestino { get; set; }
        public TipoReuniaoCC TipoReuniaoCC { get; set; }
        public string? LocalReuniao { get; set; }
        public string? LinkReuniaoOnline { get; set; }
        public DateTime DataAgendada { get; set; }
        public DateTime DataCadastro { get; set; }
        public StatusReuniaoCC StatusReuniaoCC { get; set; }
        public int QuantidadeValidacoes { get; set; }
    }
}
