using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.RelatoriosAuditorias.Entities
{
    public class NotificacaoInterna
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UsuarioDestinoId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Mensagem { get; set; } = string.Empty;

        public bool Lida { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime? DataLeitura { get; set; }
    }
}
