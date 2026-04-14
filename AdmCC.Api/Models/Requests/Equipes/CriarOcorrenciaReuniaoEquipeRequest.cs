using System.ComponentModel.DataAnnotations;

namespace AdmCC.Api.Models.Requests.Equipes
{
    public class CriarOcorrenciaReuniaoEquipeRequest
    {
        [Required]
        public DateTime DataReuniao { get; set; }

        [Range(1, int.MaxValue)]
        public int NumeroOcorrenciaNoMes { get; set; }

        public bool EhPresencial { get; set; }

        public bool Realizada { get; set; }
    }
}
