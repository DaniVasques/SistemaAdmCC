using System;
using AdmCC.Domain.CadastroBase.Entities;

namespace AdmCC.Domain.Estrategia.Entities
{
    public class AtuacaoEspecifica
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Nome { get; set; } = string.Empty;

        public Guid ClusterId { get; set; }
        public Cluster Cluster { get; set; } = null!;

        public bool Ativo { get; set; } = true;

        public ICollection<Associado> Associados { get; set; }
            = new List<Associado>();
    }
}
