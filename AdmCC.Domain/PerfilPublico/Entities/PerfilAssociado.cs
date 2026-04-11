using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.PerfilPublico.Entities
{
    public class PerfilAssociado
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid AssociadoId { get; set; }
        public AdmCC.Domain.CadastroBase.Entities.Associado Associado { get; set; } = null!;

        public string FotoProfissionalUrl { get; set; } = string.Empty;
        public string DescricaoProfissional { get; set; } = string.Empty;
        public string? NomeEmpresaExibicao { get; set; }
        public string? LogomarcaEmpresaUrl { get; set; }
        public string? Site { get; set; }
        public string? EmailPublico { get; set; }

        public bool PerfilPublicado { get; set; }

        public ICollection<MidiaAssociado> Midias { get; set; }
            = new List<MidiaAssociado>();
    }
}
