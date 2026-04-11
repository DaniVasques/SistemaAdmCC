using AdmCC.Domain.CadastroBase.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.CadastroBase.Entities
{
    public class HistoricoAssociado
    {
        public Guid Id { get; set; }

        public Guid AssociadoId { get; set; }
        public Associado Associado { get; set; } = null!;

        public StatusAssociado StatusAnterior { get; set; }
        public StatusAssociado StatusNovo { get; set; }

        public DateTime DataAlteracao { get; set; }
        public string? Motivo { get; set; }
        public Guid UsuarioResponsavelId { get; set; }
    }
}
