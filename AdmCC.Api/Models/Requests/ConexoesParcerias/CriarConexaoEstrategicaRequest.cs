using System.ComponentModel.DataAnnotations;
using AdmCC.Domain.ConexoesParcerias.Enums;

namespace AdmCC.Api.Models.Requests.ConexoesParcerias
{
    public class CriarConexaoEstrategicaRequest
    {
        public Guid AssociadoOrigemId { get; set; }
        public Guid AssociadoDestinoId { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string NomeContatoOuEmpresa { get; set; } = string.Empty;

        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string TelefoneContato { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Complemento { get; set; }

        [Required]
        public TipoDeConexao TipoDeConexao { get; set; }

        public AdmCC.Domain.ConexoesParcerias.Enums.StatusConexao StatusConexao { get; set; } =
            AdmCC.Domain.ConexoesParcerias.Enums.StatusConexao.Nova;
    }
}
