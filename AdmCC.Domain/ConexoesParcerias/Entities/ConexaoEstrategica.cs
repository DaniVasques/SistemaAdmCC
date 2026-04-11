using AdmCC.Domain.ConexoesParcerias.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.ConexoesParcerias.Entities
{
    public class ConexaoEstrategica
    {
        public Guid Id { get; set; }

        public Guid AssociadoOrigemId { get; set; }
        public Guid AssociadoDestinoId { get; set; }

        public string NomeContatoOuEmpresa { get; set; } = string.Empty;
        public string TelefoneContato { get; set; } = string.Empty;
        public string? Complemento { get; set; }

        public TipoDeConexao TipoDeConexao { get; set; }
        public StatusConexao StatusConexao { get; set; } = StatusConexao.Nova;

        public DateTime DataEnvio { get; set; }
        public bool Excluida { get; set; } = false;

        public NegocioRecebidoValidacao? ValidacaoRecebimento { get; set; }
    }
}
