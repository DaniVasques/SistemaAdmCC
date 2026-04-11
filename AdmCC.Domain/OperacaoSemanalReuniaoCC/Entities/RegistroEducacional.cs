using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities
{
    public class RegistroEducacional
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid AssociadoId { get; set; }
        public AdmCC.Domain.CadastroBase.Entities.Associado Associado { get; set; } = null!;

        public Guid ParametroPontuacaoEducacionalId { get; set; }
        public ParametroPontuacaoEducacional ParametroPontuacaoEducacional { get; set; } = null!;

        public AdmCC.Domain.OperacaoSemanalReuniaoCC.Enums.TipoPontuacaoEducacional TipoPontuacaoEducacional { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? CodigoExterno { get; set; }
        public int Pontos { get; set; }

        public DateTime DataOcorrencia { get; set; }
        public bool Validado { get; set; }
        public DateTime? DataValidacao { get; set; }
    }
}
