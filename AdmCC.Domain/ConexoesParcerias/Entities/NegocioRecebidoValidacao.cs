using AdmCC.Domain.ConexoesParcerias.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.ConexoesParcerias.Entities
{
    public class NegocioRecebidoValidacao
    {
        public Guid Id { get; set; }

        public Guid ConexaoEstrategicaId { get; set; }
        public ConexaoEstrategica ConexaoEstrategica { get; set; } = null!;

        public Guid AssociadoReceptorId { get; set; }

        public StatusConexao StatusConexao { get; set; }
        public MotivoNegocioNaoFechado? MotivoNegocioNaoFechado { get; set; }

        public decimal? ValorNegocioFechado { get; set; }

        public DateTime? DataValidacao { get; set; }

        public bool PrazoEstourado { get; set; }
        public DateTime? DataPrazoEstourado { get; set; }
    }
}
