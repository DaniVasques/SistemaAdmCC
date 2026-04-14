using AdmCC.Domain.ConexoesParcerias.Enums;

namespace AdmCC.Api.Models.Responses.ConexoesParcerias
{
    public class ConexaoEstrategicaResponse
    {
        public Guid Id { get; set; }
        public Guid AssociadoOrigemId { get; set; }
        public Guid AssociadoDestinoId { get; set; }
        public string NomeContatoOuEmpresa { get; set; } = string.Empty;
        public string TelefoneContato { get; set; } = string.Empty;
        public string? Complemento { get; set; }
        public TipoDeConexao TipoDeConexao { get; set; }
        public StatusConexao StatusConexao { get; set; }
        public DateTime DataEnvio { get; set; }
        public bool Excluida { get; set; }
        public NegocioRecebidoValidacaoResponse? ValidacaoRecebimento { get; set; }
    }
}
