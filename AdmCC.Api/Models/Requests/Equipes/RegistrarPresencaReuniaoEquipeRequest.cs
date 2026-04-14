using System.ComponentModel.DataAnnotations;

namespace AdmCC.Api.Models.Requests.Equipes
{
    public class RegistrarPresencaReuniaoEquipeRequest
    {
        public Guid AssociadoId { get; set; }

        public bool Presente { get; set; }

        public DateTime? DataRegistro { get; set; }
    }
}
