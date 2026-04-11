using System;
namespace AdmCC.Domain.Estrategia.Entities
{
    public class GrupamentoEstrategico
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public string Sigla { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public ICollection<AssociadoGrupamento> AssociadosGrupamentos { get; set; }
            = new List<AssociadoGrupamento>();
    }
}
