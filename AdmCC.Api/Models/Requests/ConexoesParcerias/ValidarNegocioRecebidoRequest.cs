using System.ComponentModel.DataAnnotations;
using AdmCC.Domain.ConexoesParcerias.Enums;

namespace AdmCC.Api.Models.Requests.ConexoesParcerias
{
    public class ValidarNegocioRecebidoRequest
    {
        public Guid AssociadoReceptorId { get; set; }

        [Required]
        public StatusConexao StatusConexao { get; set; }

        public MotivoNegocioNaoFechado? MotivoNegocioNaoFechado { get; set; }

        public decimal? ValorNegocioFechado { get; set; }

        public DateTime? DataValidacao { get; set; }

        public bool PrazoEstourado { get; set; }

        public DateTime? DataPrazoEstourado { get; set; }
    }
}
