using System.ComponentModel.DataAnnotations;

namespace AdmCC.Api.Models.Requests.Seguro
{
    public class CriarBeneficiarioSeguroRequest
    {
        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string NomeCompleto { get; set; } = string.Empty;

        [Required]
        [StringLength(14, MinimumLength = 11)]
        public string Cpf { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string GrauParentesco { get; set; } = string.Empty;

        [Range(0.01, 100)]
        public decimal Percentual { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string Telefone { get; set; } = string.Empty;
    }
}
