using System.ComponentModel.DataAnnotations;

namespace AdmCC.Api.Models.Requests.Seguro
{
    public class CriarSolicitacaoAlteracaoBeneficiarioRequest
    {
        [StringLength(1000)]
        public string? ObservacaoSolicitante { get; set; }
    }
}
