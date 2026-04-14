namespace AdmCC.Api.Models.Responses.Estrategia
{
    public class ClusterResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public bool Ativo { get; set; }
    }
}
