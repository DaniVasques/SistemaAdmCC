using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.RelatoriosAuditorias.Entities
{
    public class LogAuditoria
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Entidade { get; set; } = string.Empty;
        public string Acao { get; set; } = string.Empty;
        public Guid EntidadeId { get; set; }

        public Guid UsuarioResponsavelId { get; set; }
        public DateTime DataHora { get; set; } = DateTime.UtcNow;

        public string? DadosAnterioresJson { get; set; }
        public string? DadosNovosJson { get; set; }
    }
}
