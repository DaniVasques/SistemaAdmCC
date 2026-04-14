namespace AdmCC.Api.Models.Responses.OperacaoSemanalReuniaoCC
{
    public class CicloSemanalResponse
    {
        public Guid Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataEncerramento { get; set; }
        public int MesReferencia { get; set; }
        public int AnoReferencia { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public int QuantidadeReunioes { get; set; }
    }
}
