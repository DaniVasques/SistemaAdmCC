namespace AdmCC.Api.Models.Responses.Seguro
{
    public class BeneficiarioSeguroResponse
    {
        public Guid Id { get; set; }
        public Guid SeguroAssociadoId { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string GrauParentesco { get; set; } = string.Empty;
        public decimal Percentual { get; set; }
        public string Telefone { get; set; } = string.Empty;
    }
}
