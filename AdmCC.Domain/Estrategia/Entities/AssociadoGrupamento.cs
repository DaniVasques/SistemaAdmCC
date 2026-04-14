using System;
using AdmCC.Domain.CadastroBase.Entities;

namespace AdmCC.Domain.Estrategia.Entities
{
    // Vinculo preparado para consolidacao estrategica do associado.
    // A gestao dedicada deste cadastro fica para evolucao futura da API.
    public class AssociadoGrupamento
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AssociadoId { get; set; }
        public Associado Associado { get; set; } = null!;
        public Guid GrupamentoEstrategicoId { get; set; }
        public GrupamentoEstrategico GrupamentoEstrategico { get; set; } = null!;
    }
}
