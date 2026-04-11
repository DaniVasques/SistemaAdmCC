using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.PerfilPublico.Entities
{
    public class MidiaAssociado
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid PerfilAssociadoId { get; set; }
        public PerfilAssociado PerfilAssociado { get; set; } = null!;

        public string NomeMidia { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;

        public int OrdemExibicao { get; set; }
        public bool Ativa { get; set; } = true;
    }
}
