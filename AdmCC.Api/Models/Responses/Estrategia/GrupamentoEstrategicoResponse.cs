namespace AdmCC.Api.Models.Responses.Estrategia
{
    public class GrupamentoEstrategicoResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sigla { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
