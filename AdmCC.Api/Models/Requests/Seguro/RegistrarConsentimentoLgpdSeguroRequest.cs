using System.ComponentModel.DataAnnotations;

namespace AdmCC.Api.Models.Requests.Seguro
{
    public class RegistrarConsentimentoLgpdSeguroRequest
    {
        [Required]
        public bool Aceito { get; set; }

        public DateTime? DataAceite { get; set; }

        [Required]
        public string TextoConsentimento { get; set; } = string.Empty;
    }
}
