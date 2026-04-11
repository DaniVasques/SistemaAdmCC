using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.OperacaoSemanalReuniaoCC.Entities
{
    public class ValidacaoReuniaoCC
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ReuniaoCCId { get; set; }
        public ReuniaoCC ReuniaoCC { get; set; } = null!;

        public Guid AssociadoId { get; set; }
        public AdmCC.Domain.CadastroBase.Entities.Associado Associado { get; set; } = null!;

        public DateTime DataValidacao { get; set; } = DateTime.UtcNow;
        public bool NaoEncontrouProspect { get; set; }
        public int PontosGerados { get; set; } = 5;

        public ICollection<ProspectReuniaoCC> Prospects { get; set; }
            = new List<ProspectReuniaoCC>();
    }
}
