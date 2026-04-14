namespace AdmCC.Api.Models.Responses.ConexoesParcerias
{
    public class ParceriaAssociadoResponse
    {
        public Guid Id { get; set; }
        public Guid AssociadoOrigemId { get; set; }
        public Guid AssociadoDestinoId { get; set; }
        public DateTime DataParceria { get; set; }
        public bool Ativa { get; set; }
    }
}
