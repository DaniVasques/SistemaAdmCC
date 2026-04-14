using AdmCC.Domain.ConexoesParcerias.Enums;

namespace AdmCC.Api.Models.Responses.ConexoesParcerias
{
    public class NegocioRecebidoValidacaoResponse
    {
        public Guid Id { get; set; }
        public Guid ConexaoEstrategicaId { get; set; }
        public Guid AssociadoReceptorId { get; set; }
        public StatusConexao StatusConexao { get; set; }
        public MotivoNegocioNaoFechado? MotivoNegocioNaoFechado { get; set; }
        public decimal? ValorNegocioFechado { get; set; }
        public DateTime? DataValidacao { get; set; }
        public bool PrazoEstourado { get; set; }
        public DateTime? DataPrazoEstourado { get; set; }
    }
}
