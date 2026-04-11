using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities
{
    public class ParametroPontuacaoEducacional
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Nome { get; set; } = string.Empty;
        public AdmCC.Domain.OperacaoSemanalReuniaoCC.Enums.TipoPontuacaoEducacional TipoPontuacaoEducacional { get; set; }
        public int Pontos { get; set; }

        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    }
}
