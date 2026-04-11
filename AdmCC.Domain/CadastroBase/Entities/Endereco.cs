using System;
using System.Collections.Generic;
using System.Text;

namespace AdmCC.Domain.CadastroBase.Entities
{
    public class Endereco
    {
        public Guid Id { get; set; }

        public string Cep { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string? Complemento { get; set; } 
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;

        public bool PermitirExibicaoNaRede { get; set; }
    }
}
