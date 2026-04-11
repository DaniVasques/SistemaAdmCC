using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.CargosLiderancas.Entities
{
    public class EquipeCargoAtivo
    {
        public Guid Id { get; set; }

        public Guid EquipeId { get; set; }
        public Guid CargoLiderancaId { get; set; }

        public bool Ativo { get; set; } = true;
        public DateTime DataCadastro { get; set; }
    }
}
