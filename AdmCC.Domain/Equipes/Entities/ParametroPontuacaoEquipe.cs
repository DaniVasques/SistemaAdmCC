using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.Equipes.Entities
{
    public class ParametroPontuacaoEquipe
    {
        public Guid Id { get; set; }

        public int QuantidadeMinimaAssociados { get; set; }
        public int? QuantidadeMaximaAssociados { get; set; }

        public int Pontos { get; set; }

        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; }

    }
}
