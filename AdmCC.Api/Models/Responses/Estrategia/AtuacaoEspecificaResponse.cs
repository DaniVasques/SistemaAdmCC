namespace AdmCC.Api.Models.Responses.Estrategia
{
    public class AtuacaoEspecificaResponse
    {
        public Guid Id { get; set; }
        public Guid ClusterId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
