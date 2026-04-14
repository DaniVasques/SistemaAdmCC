using AdmCC.Domain.OperacaoSemanalReuniaoCC.Enums;

namespace AdmCC.Api.Models.Responses.OperacaoSemanalReuniaoCC
{
    public class RegistroEducacionalResponse
    {
        public Guid Id { get; set; }
        public Guid AssociadoId { get; set; }
        public string? NomeAssociado { get; set; }
        public Guid ParametroPontuacaoEducacionalId { get; set; }
        public string? NomeParametro { get; set; }
        public TipoPontuacaoEducacional TipoPontuacaoEducacional { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? CodigoExterno { get; set; }
        public int Pontos { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public bool Validado { get; set; }
        public DateTime? DataValidacao { get; set; }
    }
}
