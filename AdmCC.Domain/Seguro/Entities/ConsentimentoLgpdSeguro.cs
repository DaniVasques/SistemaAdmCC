using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.Seguro.Entities
{
    public class ConsentimentoLgpdSeguro
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid SeguroAssociadoId { get; set; }
        public SeguroAssociado SeguroAssociado { get; set; } = null!;

        public bool Aceito { get; set; }
        public DateTime? DataAceite { get; set; }
        public string TextoConsentimento { get; set; } = string.Empty;
    }
}
