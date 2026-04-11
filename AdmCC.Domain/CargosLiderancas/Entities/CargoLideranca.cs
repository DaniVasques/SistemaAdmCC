using AdmCC.Domain.CargosLiderancas.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.CargosLiderancas.Entities
{
    public class CargoLideranca
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public ClassificacaoCargo ClassificacaoCargo { get; set; }

        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; }

        public ICollection<AssociadoCargoLideranca> AssociadosCargos { get; set; }
            = new List<AssociadoCargoLideranca>();

        public ICollection<EquipeCargoAtivo> EquipesCargosAtivos { get; set; }
            = new List<EquipeCargoAtivo>();

        public ICollection<DesignacaoLiderancaEquipe> Designacoes { get; set; }
            = new List<DesignacaoLiderancaEquipe>();
    }
}
