using AdmCC.Domain.OperacaoSemanalReuniaoCC.Enums;

namespace AdmCC.Api.Models.Responses.OperacaoSemanalReuniaoCC
{
    public class ParametroPontuacaoEducacionalResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public TipoPontuacaoEducacional TipoPontuacaoEducacional { get; set; }
        public int Pontos { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
