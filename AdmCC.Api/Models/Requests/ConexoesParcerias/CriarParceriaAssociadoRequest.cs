using System.ComponentModel.DataAnnotations;

namespace AdmCC.Api.Models.Requests.ConexoesParcerias
{
    public class CriarParceriaAssociadoRequest
    {
        public Guid AssociadoOrigemId { get; set; }
        public Guid AssociadoDestinoId { get; set; }

        public DateTime? DataParceria { get; set; }

        [Required]
        public bool Ativa { get; set; } = true;
    }
}
