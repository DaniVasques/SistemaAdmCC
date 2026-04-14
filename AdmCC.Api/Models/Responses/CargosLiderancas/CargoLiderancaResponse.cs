using AdmCC.Domain.CargosLiderancas.Enums;

namespace AdmCC.Api.Models.Responses.CargosLiderancas
{
    public class CargoLiderancaResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public ClassificacaoCargo ClassificacaoCargo { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
