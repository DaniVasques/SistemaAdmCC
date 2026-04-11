using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.Seguro.Entities
{
    public class BeneficiarioSeguro
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid SeguroAssociadoId { get; set; }
        public SeguroAssociado SeguroAssociado { get; set; } = null!;

        public string NomeCompleto { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string GrauParentesco { get; set; } = string.Empty;
        public decimal Percentual { get; set; }
        public string Telefone { get; set; } = string.Empty;
    }
}
