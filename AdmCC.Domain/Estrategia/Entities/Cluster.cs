using System;
using AdmCC.Domain.CadastroBase.Entities;

namespace AdmCC.Domain.Estrategia.Entities
{
    public class Cluster
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Nome { get; set; } = string.Empty;

        public bool Ativo { get; set; } = true;

        public ICollection<AtuacaoEspecifica> AtuacoesEspecificas { get; set; }
            = new List<AtuacaoEspecifica>();

        public ICollection<Associado> Associados { get; set; }
            = new List<Associado>();
    }
}
