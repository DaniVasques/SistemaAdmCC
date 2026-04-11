using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.Seguro.Entities
{
    public class ContatoEmergencia
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid SeguroAssociadoId { get; set; }
        public SeguroAssociado SeguroAssociado { get; set; } = null!;

        public string NomeCompleto { get; set; } = string.Empty;
        public string TelefonePrincipal { get; set; } = string.Empty;
        public string? TelefoneSecundario { get; set; }
    }
}
