using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.CargosLiderancas.Entities
{
    public class DiretoriaEquipeVinculo
    {
        public Guid Id { get; set; }

        public Guid EquipeId { get; set; }
        public Guid UsuarioId { get; set; }

        public string TipoVinculo { get; set; } = string.Empty;
        // DiretorTerritorio ou DiretorEquipe

        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        public bool Indeterminado { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
