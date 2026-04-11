using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities
{
    public class ProspectReuniaoCC
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ValidacaoReuniaoCCId { get; set; }
        public ValidacaoReuniaoCC ValidacaoReuniaoCC { get; set; } = null!;

        public string NomeProspect { get; set; } = string.Empty;
        public string NomeEmpresa { get; set; } = string.Empty;
        public bool CompartilhouApenasContato { get; set; }
    }
}
